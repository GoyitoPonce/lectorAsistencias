using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace UareUSampleCSharp
{
    public partial class SelecType : Form
    {
        // C# 7.3: instanciación explícita
        private readonly Dictionary<int, ButtonMeta> _buttonMetaById = new Dictionary<int, ButtonMeta>();
        private FlowLayoutPanel _buttonsPanel;
        private int _currentEmployeeId;

        public int CurrentEmployeeId
        {
            get { return _currentEmployeeId; }
        }

        public void SetCurrentEmployee(int pkEmployee)
        {
            _currentEmployeeId = pkEmployee;
        }

        public SelecType()
        {
            InitializeComponent();
            _buttonsPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(12),
                WrapContents = true
            };
            Controls.Add(_buttonsPanel);
        }

        private async void SelecType_Load(object sender, EventArgs e)
        {
            await LoadButtonsAsync();
        }

        private async Task LoadButtonsAsync()
        {
            const string baseUrl = "https://thelastadmin.com/getTypeCheck/";
            string apiUrl = $"{baseUrl}{_currentEmployeeId}";

            HttpClient http = new HttpClient();
            HttpResponseMessage resp = null;

            try
            {
                resp = await http.GetAsync(apiUrl);
                resp.EnsureSuccessStatusCode();

                string json = await resp.Content.ReadAsStringAsync();

                ApiResponse data = null;
                try
                {
                    var settings = new JsonSerializerSettings
                    {
                        MissingMemberHandling = MissingMemberHandling.Ignore,
                        NullValueHandling = NullValueHandling.Include
                    };
                    data = JsonConvert.DeserializeObject<ApiResponse>(json, settings);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al parsear la respuesta: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (data == null || data.Buttons == null || data.Buttons.Count == 0) return;

                _buttonsPanel.Controls.Clear();
                _buttonMetaById.Clear();

                // ¿El Action vino como "Entrada"?
                bool isEntrada = string.Equals(data.Action, "Entrada", StringComparison.OrdinalIgnoreCase);
                bool isDescanso = string.Equals(data.Action, "Inicio Descanso", StringComparison.OrdinalIgnoreCase);
                bool isDescansoR = string.Equals(data.Action, "Fin Descanso", StringComparison.OrdinalIgnoreCase);
                bool isSalida = string.Equals(data.Action, "Salida", StringComparison.OrdinalIgnoreCase);

                foreach (var item in data.Buttons)
                {
                    var meta = new ButtonMeta
                    {
                        Id = item.PkButton,
                        Name = item.Name,
                        CheckingInterval = item.CheckingInterval,
                        CloseMaximum = item.CloseMaximum,
                        ColorHex = item.Color
                    };
                    _buttonMetaById[item.PkButton] = meta;

                    var btn = new Button();
                    btn.Text = item.Name ?? ("Botón " + item.PkButton);
                    btn.AutoSize = false;
                    btn.Width = 180; // un poco más ancho por textos largos
                    btn.Height = 48;
                    btn.Margin = new Padding(8);
                    btn.Name = "btn_" + item.PkButton;
                    btn.Tag = meta;

                    if (!string.IsNullOrWhiteSpace(item.Color))
                    {
                        try { btn.BackColor = ColorTranslator.FromHtml(item.Color); } catch { }
                    }

                    // Requisito: si Action == "Entrada", deshabilitar el botón pk_button == 1
                    if (isEntrada)
                    {
                        if (item.PkButton == 1)
                        {
                            btn.Enabled = false;
                            btn.BackColor = ColorTranslator.FromHtml("#D6D6D2");
                        }

                        if (item.PkButton == 2)
                        {
                            btn.Enabled = true;
                            btn.BackColor = ColorTranslator.FromHtml("#D6D6D2");
                        }

                        if (item.PkButton == 3)
                        {
                            btn.Enabled = false;
                            btn.BackColor = ColorTranslator.FromHtml(item.Color);
                        }

                        if (item.PkButton == 4)
                        {
                            btn.Enabled = true;
                            btn.BackColor = ColorTranslator.FromHtml("#D6D6D2");
                        }
                    }

                    if (isDescanso)
                    {
                        if(item.PkButton == 1)
                        {
                            btn.Enabled = false;
                            btn.BackColor = ColorTranslator.FromHtml("#D6D6D2");
                        }

                        if (item.PkButton == 2)
                        {
                            btn.Enabled = false;
                            btn.BackColor = ColorTranslator.FromHtml("#D6D6D2");
                        }

                        if (item.PkButton == 3)
                        {
                            btn.Enabled = true;
                            btn.BackColor = ColorTranslator.FromHtml(item.Color);
                        }

                        if (item.PkButton == 4)
                        {
                            btn.Enabled = false;
                            btn.BackColor = ColorTranslator.FromHtml("#D6D6D2");
                        }

                    }

                    if (isDescansoR)
                    {
                        if (item.PkButton == 1)
                        {
                            btn.Enabled = false;
                            btn.BackColor = ColorTranslator.FromHtml("#D6D6D2");
                        }

                        if (item.PkButton == 2)
                        {
                            btn.Enabled = true;
                            btn.BackColor = ColorTranslator.FromHtml("#D6D6D2");
                        }

                        if (item.PkButton == 3)
                        {
                            btn.Enabled = false;
                            btn.BackColor = ColorTranslator.FromHtml(item.Color);
                        }

                        if (item.PkButton == 4)
                        {
                            btn.Enabled = true;
                            btn.BackColor = ColorTranslator.FromHtml("#D6D6D2");
                        }

                    }

                    if (isSalida)
                    {
                        if (item.PkButton == 1)
                        {
                            btn.Enabled = false;
                            btn.BackColor = ColorTranslator.FromHtml("#D6D6D2");
                        }

                        if (item.PkButton == 2)
                        {
                            btn.Enabled = false;
                            btn.BackColor = ColorTranslator.FromHtml("#D6D6D2");
                        }

                        if (item.PkButton == 3)
                        {
                            btn.Enabled = false;
                            btn.BackColor = ColorTranslator.FromHtml(item.Color);
                        }

                        if (item.PkButton == 4)
                        {
                            btn.Enabled = false;
                            btn.BackColor = ColorTranslator.FromHtml("#D6D6D2");
                        }

                    }


                    btn.Click += async (s, e) =>
                    {
                        var m = (ButtonMeta)((Button)s).Tag;

                        // Evita múltiples clicks durante el POST
                        var b = (Button)s;
                        b.Enabled = false;
                        
                        try
                        {
                            int activity = 1;
                            int type = 1;

                            if (m.Id == 2){
                                activity = 2;
                                type = 1;
                            }
                            else if (m.Id == 3){
                                activity = 2;
                                type = 2;
                            }
                            else if (m.Id == 4)
                            {
                                activity = 1;
                                type = 2;
                            }
                            // Mapeo sugerido:
                            // activity = nombre del botón (lo que el usuario hizo)
                            // type     = última "Action" enviada por el backend (contexto/estado)
                            // Si prefieres al revés, invierte los parámetros.
                            bool ok = await SaveAssistantAsync(_currentEmployeeId, activity, type);

                            if (ok)
                            {
                                MessageBox.Show("Registro guardado correctamente.", "Éxito",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Close();
                                return;
                            }
                            else
                                MessageBox.Show("No se pudo guardar el registro.", "Aviso",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        catch (Exception exClick)
                        {
                            MessageBox.Show("Error al guardar: " + exClick.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            b.Enabled = true;
                        }
                    };

                    _buttonsPanel.Controls.Add(btn);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al llamar la API: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (resp != null) resp.Dispose();
                http.Dispose();
            }
        }

        private async Task<bool> SaveAssistantAsync(int pkEmployee, int activity, int type)
        {
            const string SAVE_API_URL = "http://127.0.0.1:8000/saveAsistant"; // <-- tu ruta POST

            HttpClient http = new HttpClient();
            HttpResponseMessage resp = null;

            try
            {
                http.Timeout = TimeSpan.FromSeconds(15);

                // Enviar como application/x-www-form-urlencoded
                var fields = new List<KeyValuePair<string, string>>();
                fields.Add(new KeyValuePair<string, string>("pkEmployee", pkEmployee.ToString()));
                fields.Add(new KeyValuePair<string, string>("activity", activity.ToString()));
                fields.Add(new KeyValuePair<string, string>("type", type.ToString()));

                var content = new FormUrlEncodedContent(fields);

                resp = await http.PostAsync(SAVE_API_URL, content);
                resp.EnsureSuccessStatusCode();

                string responseText = await resp.Content.ReadAsStringAsync();
                responseText = responseText.Trim();

                // El controlador devuelve "true" o "false" como string
                // (por si acaso vienen comillas o espacios)
                responseText = responseText.Trim('"');

                return string.Equals(responseText, "true", StringComparison.OrdinalIgnoreCase);
            }
            finally
            {
                if (resp != null) resp.Dispose();
                http.Dispose();
            }
        }

        public bool TryGetButtonMetaById(int id, out ButtonMeta meta)
        {
            return _buttonMetaById.TryGetValue(id, out meta);
        }
    }

    // ====== Modelos ======
    public class ApiResponse
    {
        [JsonProperty("buttons")]
        public List<ButtonItem> Buttons { get; set; }

        // Agregado: el campo Action del JSON
        [JsonProperty("Action")]
        public string Action { get; set; }
    }

    public class ButtonItem
    {
        [JsonProperty("pk_button")]
        public int PkButton { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("checking_interval")]
        public int CheckingInterval { get; set; }

        [JsonProperty("close_maximum")]
        public int CloseMaximum { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }
    }

    public class ButtonMeta
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CheckingInterval { get; set; }
        public int CloseMaximum { get; set; }
        public string ColorHex { get; set; }
    }
}
