using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DPUruNet;
using Newtonsoft.Json;

namespace UareUSampleCSharp
{
    public partial class Verification : Form
    {
        /// <summary>
        /// Holds the main form with many functions common to all of SDK actions.
        /// </summary>
        public Form_Main _sender;

        private const int PROBABILITY_ONE = 0x7fffffff;
        private Fmd firstFinger;
        private Fmd secondFinger;
        private int count;
  
        public Verification()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initialize the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Verification_Load(object sender, System.EventArgs e)
        {
            txtVerify.Text = string.Empty;
            firstFinger = null;
            secondFinger = null;
            count = 0;

            SendMessage(Action.SendMessage, "Por favor captura tu huella.");

            if (!_sender.OpenReader())
            {
                this.Close();
            }

            if (!_sender.StartCaptureAsync(this.OnCaptured))
            {
                this.Close();
            }
        }

        
        /// <summary>
        /// Handler for when a fingerprint is captured.
        /// </summary>
        /// <param name="captureResult">contains info and data on the fingerprint capture</param>
        private async void OnCaptured(CaptureResult captureResult)
        {
            try
            {
                // Verifica la calidad de la captura
                if (!_sender.CheckCaptureResult(captureResult)) return;

                SendMessage(Action.SendMessage, "Buscando Huella...");

                DataResult<Fmd> resultConversion = FeatureExtraction.CreateFmdFromFid(captureResult.Data, Constants.Formats.Fmd.ANSI);
                if (resultConversion.ResultCode != Constants.ResultCode.DP_SUCCESS)
                {
                    _sender.Reset = true;
                    throw new Exception(resultConversion.ResultCode.ToString());
                }

                // Solicita todas las huellas dactilares guardadas desde Laravel
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://thelastadmin.com/");
                    var response = await client.GetAsync("verificar-huella");

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = await response.Content.ReadAsStringAsync();
                        var employees = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Employee>>(jsonResponse);

                        // Itera sobre cada empleado y su huella para comparar
                        foreach (var employee in employees)
                        {
                            try
                            {
                                // Convierte la huella almacenada desde Base64 a byte[]
                                byte[] storedFingerprintBytes = Convert.FromBase64String(employee.img);

                                // Verifica que la huella no esté vacía
                                if (storedFingerprintBytes.Length == 0)
                                {
                                    SendMessage(Action.SendMessage, $"La huella de Empleado ID {employee.pk_finger} está vacía.");
                                    continue; // Salta al siguiente empleado si la huella está vacía
                                }

                                // Deserializa el byte[] a un objeto Fmd si los datos están en formato XML
                                string storedFingerprintXml = Encoding.UTF8.GetString(storedFingerprintBytes);

                                // Deserializa los datos de huella en formato XML a un objeto Fmd
                                Fmd storedFmd = Fmd.DeserializeXml(storedFingerprintXml);

                                // Compara la huella capturada con la huella almacenada
                                CompareResult compareResult = Comparison.Compare(resultConversion.Data, 0, storedFmd, 0);

                                // Verifica el resultado de la comparación
                                if (compareResult.ResultCode == Constants.ResultCode.DP_SUCCESS &&
                                    compareResult.Score < (PROBABILITY_ONE / 100000))
                                {
                                    // Coincidencia encontrada
                                    SendMessage(Action.SendMessage, $"Bienvenido: {employee.name} {employee.last_name}");

                                    await SendAsistant(employee.fk_employee);

                                    var cancellationTokenSource = new CancellationTokenSource();
                                    var cancellationToken = cancellationTokenSource.Token;
                                    await Task.Delay(2000).ContinueWith((t) =>
                                     {
                                         SendMessage(Action.CleanMessage, $"Bienvenido: {employee.name} {employee.last_name}");
                                         SendMessage(Action.SendMessage, "Por favor captura tu huella.");
                                         //Do stuff
                                     }, cancellationToken);
                                   
                                    return; 
                                }
                            }
                            catch (FormatException formatEx)
                            {
                                SendMessage(Action.SendMessage, $"Error al procesar la huella: {formatEx.Message}");
                                continue; // Pasa al siguiente empleado si ocurre un error con el formato Base64
                            }
                            catch (Exception innerEx)
                            {
                                // Si ocurre un error al procesar la huella, lo reporta
                                SendMessage(Action.SendMessage, $"Error al procesar huella: {innerEx.Message}");
                            }
                        }

                        // Si no se encuentra ninguna coincidencia
                        SendMessage(Action.SendMessage, "No se encontró ninguna coincidencia de huella.");
                    }
                    else
                    {
                        SendMessage(Action.SendMessage, $"Error al obtener las huellas guardadas: {response.StatusCode}");
                    }
                }
  
            }
            catch (Exception ex)
            {
                SendMessage(Action.SendMessage, "Error: " + ex.Message);
            }
        }

        private async Task SendAsistant(int fk_employee)
        {
                // Serializar los datos de la huella digital y el posicionamiento del dedo en formato JSON
                var payload = new
                {
                    pkEmployee = fk_employee
                };

                // Convertir los datos a JSON
                string jsonPayload = JsonConvert.SerializeObject(payload);

                // Crear el cliente HTTP y definir la URL de la API en Laravel
                using (HttpClient client = new HttpClient())
                {
                    // Reemplaza con tu URL de API de Laravel
                    string url = "https://thelastadmin.com/saveAsistant";

                    // Configurar el contenido de la solicitud
                    HttpContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                    try
                    {
                        // Enviar la solicitud POST
                        HttpResponseMessage response = await client.PostAsync(url, content);

                        // Verificar el resultado de la solicitud
                        if (response.IsSuccessStatusCode)
                        {
                        //  SendMessage("Huella enviada y guardada correctamente en el servidor.");
                    }
                    else
                        {
                         MessageBox.Show($"Error al registrar la asistencia favor de volver a checkar: {response.StatusCode} - {response.ReasonPhrase}");
                    }
                }
                    catch (Exception ex)
                    {
                    // Manejar posibles excepciones
                    MessageBox.Show($"Error al registrar la asistencia favor de volver a checkar: {ex}");
                }
            }
            
        }

        // Clase para deserializar el JSON de Laravel
        public class Employee
        {
            public int pk_finger { get; set; }
            public int fk_employee { get; set; }
            public string name { get; set; }
            public string last_name { get; set; }
            public string img { get; set; }
        }

        /// <summary>
        /// Close window.
        /// </summary>
        private void btnBack_Click(System.Object sender, System.EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Close window.
        /// </summary>
        private void Verification_Closed(object sender, System.EventArgs e)
        {
            _sender.CancelCaptureAndCloseReader(this.OnCaptured);
        }

        #region SendMessage
        private enum Action
        {
            SendMessage,
            CleanMessage
        }
        private delegate void SendMessageCallback(Action action, string payload);
        private void SendMessage(Action action, string payload)
        {
            try
            {
                if (this.txtVerify.InvokeRequired)
                {
                    SendMessageCallback d = new SendMessageCallback(SendMessage);
                    this.Invoke(d, new object[] { action, payload });
                }
                else
                {
                    switch (action)
                    {
                        case Action.SendMessage:
                            txtVerify.Text += payload + "\r\n";
                            txtVerify.SelectionStart = txtVerify.TextLength;
                            txtVerify.ScrollToCaret();
                            break;
                        default:
                             txtVerify.Clear();
                            break;
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        #endregion
    }
}