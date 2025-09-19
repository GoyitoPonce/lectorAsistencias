using System.Windows.Forms;
using DPUruNet;

namespace UareUSampleCSharp
{
    public partial class ReaderSelection : Form
    {
        private ReaderCollection _readers;

        private Form_Main _sender;

        public Form_Main Sender
        {
            get { return _sender; }
            set { _sender = value; }
        }
    
        private Reader _reader;

        public Reader CurrentReader
        {
            get { return _reader; }
            set { _reader = value; }
        }
                
        public ReaderSelection()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Clear the control of readers, get readers and display.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(System.Object sender, System.EventArgs e)
        {
            cboReaders.Text = string.Empty;
            cboReaders.Items.Clear();
            cboReaders.SelectedIndex = -1;

            _readers = ReaderCollection.GetReaders();

            foreach (Reader Reader in _readers)
            {
                cboReaders.Items.Add(Reader.Description.SerialNumber);
            }

            if (cboReaders.Items.Count > 0)
            {
                cboReaders.SelectedIndex = 0;
                btnSelect.Enabled = true;
            }
            else
            {
                btnSelect.Enabled = false;
            }
        }
       

        private void btnReaderSelect_Click(System.Object sender, System.EventArgs e)
        {
            if (_sender.CurrentReader != null)
            {
                _sender.CurrentReader.Dispose();
                _sender.CurrentReader = null;
            }
            _sender.CurrentReader = _readers[cboReaders.SelectedIndex];
            this.Close();
        }

        private void btnBack_Click(System.Object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void ReaderSelection_Load(object sender, System.EventArgs e)
        {
            btnRefresh_Click(this, new System.EventArgs());
        }
    }
}
