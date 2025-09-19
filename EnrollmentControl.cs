using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DPUruNet;
using Newtonsoft.Json;

namespace UareUSampleCSharp
{
    public partial class EnrollmentControl : Form
    {
        /// <summary>
    /// Holds the main form with many functions common to all of SDK actions.
    /// </summary>
        public Form_Main _sender;

        private DPCtlUruNet.EnrollmentControl _enrollmentControl;
        
        public EnrollmentControl()
        {
            InitializeComponent();
            dedo1.BringToFront();
            dedo3.SendToBack();
            dedo0.Visible = false;
            dedo1.Visible = false;
            dedo2.Visible = false;
            dedo3.Visible = false;
            dedo4.Visible = false;
        }

        /// <summary>
        /// Initialize the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private async void EnrollmentControl_Load(object sender, EventArgs e)
        {
            if (_enrollmentControl != null)
            {
                _enrollmentControl.Reader = _sender.CurrentReader;
            }
            else
            {
                _enrollmentControl = new DPCtlUruNet.EnrollmentControl(_sender.CurrentReader, Constants.CapturePriority.DP_PRIORITY_COOPERATIVE);
                _enrollmentControl.BackColor = System.Drawing.SystemColors.Window;
                _enrollmentControl.Location = new System.Drawing.Point(3, 3);
                _enrollmentControl.Name = "ctlEnrollmentControl";
                _enrollmentControl.Size = new System.Drawing.Size(482, 346);
                _enrollmentControl.TabIndex = 0;
                _enrollmentControl.OnCancel += new DPCtlUruNet.EnrollmentControl.CancelEnrollment(this.enrollment_OnCancel);
                _enrollmentControl.OnCaptured += new DPCtlUruNet.EnrollmentControl.FingerprintCaptured(this.enrollment_OnCaptured);
                _enrollmentControl.OnDelete += new DPCtlUruNet.EnrollmentControl.DeleteEnrollment(this.enrollment_OnDelete);
                _enrollmentControl.OnEnroll += new DPCtlUruNet.EnrollmentControl.FinishEnrollment(this.enrollment_OnEnroll);
                _enrollmentControl.OnStartEnroll += new DPCtlUruNet.EnrollmentControl.StartEnrollment(this.enrollment_OnStartEnroll);
            }

            this.Controls.Add(_enrollmentControl);
            // Llamar al método de carga de empleados
            await LoadEmployeesAsync();
         

        }

        #region Enrollment Control Events
        private async void enrollment_OnCancel(DPCtlUruNet.EnrollmentControl enrollmentControl, Constants.ResultCode result, int fingerPosition)
        {
            if (enrollmentControl.Reader != null)
            {
               // SendMessage("OnCancel:  " + enrollmentControl.Reader.Description.Name + ", finger " + fingerPosition);
            }
            else
            {
               // SendMessage("OnCancel:  No Reader Connected, finger " + fingerPosition);
            }

            if (comboBox1.SelectedItem is ComboBoxItem selectedItem)
            {
                string nombreCompleto = selectedItem.Text;
                int idEmpleado = selectedItem.Value;
                await LoadFmdsFromServerAsync(idEmpleado);

            }

            btnCancel.Enabled = false;
            label4.Visible = false;
            groupBox1.Visible = true;
        }

        private void enrollment_OnCaptured(DPCtlUruNet.EnrollmentControl enrollmentControl, CaptureResult captureResult, int fingerPosition)
        {
            if (comboBox1.SelectedItem is ComboBoxItem selectedEmployee)
            {
                if (enrollmentControl.Reader != null)
                {
                    SendMessage("Capturada: Huella digital capturada");
                }
                else
                {
                    SendMessage("OnCaptured:  No Reader Connected, finger " + fingerPosition);
                }

                if (captureResult.ResultCode != Constants.ResultCode.DP_SUCCESS)
                {
                    if (_sender.CurrentReader != null)
                    {
                        _sender.CurrentReader.Dispose();
                        _sender.CurrentReader = null;
                    }

                    // Disconnect reader from enrollment control
                    _enrollmentControl.Reader = null;

                    MessageBox.Show("Error:  " + captureResult.ResultCode);
                    btnCancel.Enabled = false;
                }
                else
                {
                    if (captureResult.Data != null)
                    {
                        foreach (Fid.Fiv fiv in captureResult.Data.Views)
                        {
                            pbFingerprint.Image = _sender.CreateBitmap(fiv.RawImage, fiv.Width, fiv.Height);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Seleccione un empleado antes de enviar la huella.");
            }
 
        }
        private void enrollment_OnDelete(DPCtlUruNet.EnrollmentControl enrollmentControl, Constants.ResultCode result, int fingerPosition)
        {
            if (enrollmentControl.Reader != null)
            {
                SendMessage("OnDelete:  " + enrollmentControl.Reader.Description.Name + ", finger " + fingerPosition);
            }
            else
            {
                SendMessage("OnDelete:  No Reader Connected, finger " + fingerPosition);
            }

            _sender.Fmds.Remove(fingerPosition);

            if (_sender.Fmds.Count == 0)
            {
                _sender.btnIdentificationControl.Enabled = false;
            }
        }
        private async void enrollment_OnEnroll(DPCtlUruNet.EnrollmentControl enrollmentControl, DataResult<Fmd> result, int fingerPosition)
        {
            if (comboBox1.SelectedItem is ComboBoxItem selectedEmployee)
            {
                if (enrollmentControl.Reader != null)
                {
                   /// SendMessage("OnEnroll:  " + enrollmentControl.Reader.Description.Name + ", finger " + fingerPosition);
                }
                else
                {
                    SendMessage("OnEnroll:  No Reader Connected, finger " + fingerPosition);
                }

                if (result != null && result.Data != null)
                {
                   // _sender.Fmds.Add(fingerPosition, result.Data);
                    await SendFingerprintImageAsync(result.Data, fingerPosition);
                }

                btnCancel.Enabled = false;
                label4.Visible = false;
                _sender.btnIdentificationControl.Enabled = true;

                if (comboBox1.SelectedItem is ComboBoxItem selectedItem)
                {
                    string nombreCompleto = selectedItem.Text;
                    int idEmpleado = selectedItem.Value;
                    await LoadFmdsFromServerAsync(idEmpleado);

                }
            }
            else
            {
                MessageBox.Show("Seleccione un empleado antes de enviar la huella.");
            }
        }
        private void enrollment_OnStartEnroll(DPCtlUruNet.EnrollmentControl enrollmentControl, Constants.ResultCode result, int fingerPosition)
        {
            dedo0.Visible = false;
            dedo1.Visible = false;
            dedo2.Visible = false;
            dedo3.Visible = false;
            dedo4.Visible = false;
            if (comboBox1.SelectedItem is ComboBoxItem selectedEmployee)
            {
                if (enrollmentControl.Reader != null)
                {
                    SendMessage("Registra tu huella siguiendo las instrucciones");
                }
                else
                {
                    SendMessage("OnStartEnroll:  No Reader Connected, finger " + fingerPosition);
                }
                var a = _sender.Fmds;
                btnCancel.Enabled = true;
                label4.Visible = true;
                groupBox1.Visible = false;
             }
         else
           {
                MessageBox.Show("Seleccione un empleado antes de registrar las huella.");
                _enrollmentControl.Cancel();
            }
    }
        #endregion

        private async Task LoadEmployeesAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // URL del servidor Laravel para obtener los datos de empleados
                    string url = "https://thelastadmin.com/getEmployees";

                    // Realizar la solicitud POST
                    HttpResponseMessage response = await client.GetAsync(url); // No envía cuerpo en esta solicitud

                    // Verificar el resultado de la solicitud
                    if (response.IsSuccessStatusCode)
                    {
                        // Leer la respuesta como una cadena
                        string jsonResponse = await response.Content.ReadAsStringAsync();

                        // Deserializar la respuesta JSON en una lista de objetos de empleado
                        var employees = JsonConvert.DeserializeObject<List<Employee>>(jsonResponse);

                        // Llenar el ComboBox con los nombres de los empleados
                        comboBox1.Items.Clear();
                        foreach (var employee in employees)
                        {
                          
                            comboBox1.Items.Add(new ComboBoxItem
                            {
                                Text = $"{employee.name} {employee.last_name}",
                                Value = employee.pk_employee
                            });
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Error al cargar empleados: {response.StatusCode} - {response.ReasonPhrase}");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error en la solicitud HTTP: {ex.Message}");
                }
            }
        }

        public class ComboBoxItem
        {
            public string Text { get; set; }
            public int Value { get; set; }  // PkEmployee
            public override string ToString() => Text;  // Para que muestre el nombre en pantalla
        }

        public class Employee
        {
            public int pk_employee { get; set; }
            public string name { get; set; }
            public string last_name { get; set; }
            public string img { get; set; }
            public int finger { get; set; }
        }

        /// <summary>
        /// Cancel enrollment when window is closed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            result = MessageBox.Show("¿Estás segura de que deseas cancelar esta inscripción?", "¿Estas seguro?", buttons, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                _enrollmentControl.Cancel();
            }
        }

        private async Task SendFingerprintImageAsync(Fmd fingerprintData, int fingerPosition)
        {

            if (comboBox1.SelectedItem is ComboBoxItem selectedEmployee)
            {
                int pkEmployee = selectedEmployee.Value;
                // Serializar los datos de la huella digital en formato XML
                string fingerprintXml = Fmd.SerializeXml(fingerprintData);
                // Convertir el XML a Base64
                string fingerprintBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(fingerprintXml));

                // Serializar los datos de la huella digital y el posicionamiento del dedo en formato JSON
                var payload = new
                {
                    pkEmployee     = pkEmployee,
                    fingerPosition = fingerPosition,
                    fingerprintData = fingerprintBase64 // Convertir el XML de la huella a base64 para enviarlo
                };

                // Convertir los datos a JSON
                string jsonPayload = JsonConvert.SerializeObject(payload);

                // Crear el cliente HTTP y definir la URL de la API en Laravel
                using (HttpClient client = new HttpClient())
                {
                    // Reemplaza con tu URL de API de Laravel
                    string url = "https://thelastadmin.com/savefinger";

                    // Configurar el contenido de la solicitud
                    HttpContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                    try
                    {
                        // Enviar la solicitud POST
                        HttpResponseMessage response = await client.PostAsync(url, content);

                        // Verificar el resultado de la solicitud
                        if (response.IsSuccessStatusCode)
                        {
                            SendMessage("Huella enviada y guardada correctamente en el servidor.");
                        }
                        else
                        {
                            SendMessage($"Error al enviar la huella: {response.StatusCode} - {response.ReasonPhrase}");
                        }
                    }
                    catch (Exception ex)
                    {
                        // Manejar posibles excepciones
                        SendMessage($"Error en la solicitud HTTP: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Seleccione un empleado antes de enviar la huella.");
            }
        }

        private async Task LoadFmdsFromServerAsync(int pkEployee)
        {
            dedo0.Visible = false;
            dedo1.Visible = false;
            dedo2.Visible = false;
            dedo3.Visible = false;
            dedo4.Visible = false;
            try
            {
                _sender.Fmds.Clear();
                // Solicita todas las huellas dactilares guardadas desde Laravel
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://thelastadmin.com/");
                    var response = await client.GetAsync("obtenerhuellas?pkEployee=" + pkEployee);

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = await response.Content.ReadAsStringAsync();
                        var employees = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Employee>>(jsonResponse);

                        // Itera sobre cada empleado y su huella para comparar
                        foreach (var employee in employees)
                        {
                            try
                            {
                                if(employee.finger == 0)
                                {
                                    dedo0.Visible = true;
                                }

                                if (employee.finger == 1)
                                {
                                    dedo1.Visible = true;
                                }

                                if (employee.finger == 2)
                                {
                                    dedo2.Visible = true;
                                }

                                if (employee.finger == 3)
                                {
                                    dedo3.Visible = true;
                                }

                                if (employee.finger == 4)
                                {
                                    dedo4.Visible = true;
                                }

                            }
                            catch (FormatException formatEx)
                            {
                                MessageBox.Show($"Error al procesar la huella Base64 de Empleado ID: {formatEx.Message}");
                                continue; // Pasa al siguiente empleado si ocurre un error con el formato Base64
                            }
                            catch (Exception innerEx)
                            {
                                // Si ocurre un error al procesar la huella, lo reporta
                                MessageBox.Show($"Error al procesar huella para Empleado ID: {innerEx.Message}");
                            }
                        }

                    }
                    else
                    {
                        MessageBox.Show("Error al obtener las huellas guardadas: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        /// <summary>
        /// Close window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        /// <summary>
    /// Cancel enrollment when window is closed.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <remarks></remarks>
        private void frmEnrollment_Closed(object sender, EventArgs e)
        {
            _enrollmentControl.Cancel();
        }

        private void SendMessage(string message)
        {
            txtMessage.Text += message + "\r\n\r\n";
            txtMessage.SelectionStart = txtMessage.TextLength;
            txtMessage.ScrollToCaret();
        }

        private async void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox1.SelectedItem is ComboBoxItem selectedItem)
            {
                string nombreCompleto = selectedItem.Text;
                int idEmpleado = selectedItem.Value;
                await LoadFmdsFromServerAsync(idEmpleado);
           
            }
            // Obtiene el elemento seleccionado actual
        }

        private void dedo1_Click(object sender, EventArgs e)
        {
            // Mostrar el MessageBox con botones Sí y No
            DialogResult result = MessageBox.Show(
                "¿Estás seguro de que deseas eliminar la huella?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            // Verificar la respuesta del usuario
            if (result == DialogResult.Yes)
            {

                  if (comboBox1.SelectedItem is ComboBoxItem selectedEmployee)
                   {

                    if (comboBox1.SelectedItem is ComboBoxItem selectedItem)
                    {
                        int idEmpleado = selectedItem.Value;
                          EliminarHuella(idEmpleado, 1);
                     }
                   
                  }
            }
           
        }

        private async void EliminarHuella(int idEmpleado, int fingerPosition)
        {
            // Serializar los datos de la huella digital y el posicionamiento del dedo en formato JSON
            var payload = new
            {
                pkEmployee = idEmpleado,
                fingerPosition = fingerPosition
            };

            // Convertir los datos a JSON
            string jsonPayload = JsonConvert.SerializeObject(payload);

            // Crear el cliente HTTP y definir la URL de la API en Laravel
            using (HttpClient client = new HttpClient())
            {
                // Reemplaza con tu URL de API de Laravel
                string url = "https://thelastadmin.com/deletefinger";

                // Configurar el contenido de la solicitud
                HttpContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                try
                {
                    // Enviar la solicitud POST
                    HttpResponseMessage response = await client.PostAsync(url, content);

                    // Verificar el resultado de la solicitud
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("La huella ha sido eliminada correctamente.", "Eliminación exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await LoadFmdsFromServerAsync(idEmpleado);
                    }
                    else
                    {
                        MessageBox.Show("La huella no se ha podido eliminar.", "Eliminación exitosa", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    // Manejar posibles excepciones
                     MessageBox.Show($"Error en la solicitud HTTP: {ex.Message}", "Eliminación exitosa", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        
          
        }

        private void dedo2_Click(object sender, EventArgs e)
        {
            // Mostrar el MessageBox con botones Sí y No
            DialogResult result = MessageBox.Show(
                "¿Estás seguro de que deseas eliminar la huella?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            // Verificar la respuesta del usuario
            if (result == DialogResult.Yes)
            {

                if (comboBox1.SelectedItem is ComboBoxItem selectedEmployee)
                {

                    if (comboBox1.SelectedItem is ComboBoxItem selectedItem)
                    {
                        int idEmpleado = selectedItem.Value;
                        EliminarHuella(idEmpleado, 2);
                    }

                }
            }

        }

        private void dedo0_Click(object sender, EventArgs e)
        {
            // Mostrar el MessageBox con botones Sí y No
            DialogResult result = MessageBox.Show(
                "¿Estás seguro de que deseas eliminar la huella?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            // Verificar la respuesta del usuario
            if (result == DialogResult.Yes)
            {

                if (comboBox1.SelectedItem is ComboBoxItem selectedEmployee)
                {

                    if (comboBox1.SelectedItem is ComboBoxItem selectedItem)
                    {
                        int idEmpleado = selectedItem.Value;
                        EliminarHuella(idEmpleado, 0);
                    }

                }
            }

        }

        private void dedo3_Click(object sender, EventArgs e)
        {
            // Mostrar el MessageBox con botones Sí y No
            DialogResult result = MessageBox.Show(
                "¿Estás seguro de que deseas eliminar la huella?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            // Verificar la respuesta del usuario
            if (result == DialogResult.Yes)
            {

                if (comboBox1.SelectedItem is ComboBoxItem selectedEmployee)
                {

                    if (comboBox1.SelectedItem is ComboBoxItem selectedItem)
                    {
                        int idEmpleado = selectedItem.Value;
                        EliminarHuella(idEmpleado, 3);
                    }

                }
            }
        }

        private void dedo4_Click(object sender, EventArgs e)
        {
            // Mostrar el MessageBox con botones Sí y No
            DialogResult result = MessageBox.Show(
                "¿Estás seguro de que deseas eliminar la huella?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            // Verificar la respuesta del usuario
            if (result == DialogResult.Yes)
            {

                if (comboBox1.SelectedItem is ComboBoxItem selectedEmployee)
                {

                    if (comboBox1.SelectedItem is ComboBoxItem selectedItem)
                    {
                        int idEmpleado = selectedItem.Value;
                        EliminarHuella(idEmpleado, 4);
                    }

                }
            }
        }
    }
}
