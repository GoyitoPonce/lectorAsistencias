using DPCtlUruNet;

//! @cond
namespace UareUSampleCSharp
{
    partial class EnrollmentControl
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EnrollmentControl));
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.pbFingerprint = new System.Windows.Forms.PictureBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.Instrucciones = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dedo0 = new System.Windows.Forms.PictureBox();
            this.dedo4 = new System.Windows.Forms.PictureBox();
            this.dedo1 = new System.Windows.Forms.PictureBox();
            this.dedo2 = new System.Windows.Forms.PictureBox();
            this.dedo3 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbFingerprint)).BeginInit();
            this.Instrucciones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dedo0)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dedo4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dedo1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dedo2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dedo3)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtMessage
            // 
            this.txtMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtMessage.Location = new System.Drawing.Point(491, 3);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMessage.Size = new System.Drawing.Size(276, 173);
            this.txtMessage.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.Enabled = false;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(410, 314);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(69, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(491, 314);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(72, 23);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Cerrar";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pbFingerprint
            // 
            this.pbFingerprint.Location = new System.Drawing.Point(583, 182);
            this.pbFingerprint.Name = "pbFingerprint";
            this.pbFingerprint.Size = new System.Drawing.Size(184, 167);
            this.pbFingerprint.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbFingerprint.TabIndex = 0;
            this.pbFingerprint.TabStop = false;
            // 
            // comboBox1
            // 
            this.comboBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(1, 115);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(484, 24);
            this.comboBox1.TabIndex = 4;
            this.comboBox1.Text = "Selecciona un Empleado";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // Instrucciones
            // 
            this.Instrucciones.Controls.Add(this.label3);
            this.Instrucciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Instrucciones.Location = new System.Drawing.Point(1, 3);
            this.Instrucciones.Name = "Instrucciones";
            this.Instrucciones.Size = new System.Drawing.Size(484, 106);
            this.Instrucciones.TabIndex = 5;
            this.Instrucciones.TabStop = false;
            this.Instrucciones.Text = "Registra tu huella";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(11, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(467, 77);
            this.label3.TabIndex = 0;
            this.label3.Text = resources.GetString("label3.Text");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(190, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 15);
            this.label2.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(190, 254);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(271, 57);
            this.label4.TabIndex = 8;
            this.label4.Text = "Para comenzar, escanee su dedo y espere hasta que se confirme que el resultado es" +
    " correcto. Repita el proceso con cada uno de los escaneos restantes.";
            this.label4.Visible = false;
            // 
            // dedo0
            // 
            this.dedo0.Image = ((System.Drawing.Image)(resources.GetObject("dedo0.Image")));
            this.dedo0.Location = new System.Drawing.Point(250, 221);
            this.dedo0.Name = "dedo0";
            this.dedo0.Size = new System.Drawing.Size(53, 36);
            this.dedo0.TabIndex = 9;
            this.dedo0.TabStop = false;
            this.dedo0.Visible = false;
            this.dedo0.Click += new System.EventHandler(this.dedo0_Click);
            // 
            // dedo4
            // 
            this.dedo4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.dedo4.Image = ((System.Drawing.Image)(resources.GetObject("dedo4.Image")));
            this.dedo4.Location = new System.Drawing.Point(351, 152);
            this.dedo4.Name = "dedo4";
            this.dedo4.Size = new System.Drawing.Size(25, 48);
            this.dedo4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.dedo4.TabIndex = 10;
            this.dedo4.TabStop = false;
            this.dedo4.Visible = false;
            this.dedo4.Click += new System.EventHandler(this.dedo4_Click);
            // 
            // dedo1
            // 
            this.dedo1.Image = ((System.Drawing.Image)(resources.GetObject("dedo1.Image")));
            this.dedo1.InitialImage = null;
            this.dedo1.Location = new System.Drawing.Point(268, 156);
            this.dedo1.Name = "dedo1";
            this.dedo1.Size = new System.Drawing.Size(40, 55);
            this.dedo1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.dedo1.TabIndex = 11;
            this.dedo1.TabStop = false;
            this.dedo1.Visible = false;
            this.dedo1.Click += new System.EventHandler(this.dedo1_Click);
            // 
            // dedo2
            // 
            this.dedo2.Image = ((System.Drawing.Image)(resources.GetObject("dedo2.Image")));
            this.dedo2.Location = new System.Drawing.Point(292, 142);
            this.dedo2.Name = "dedo2";
            this.dedo2.Size = new System.Drawing.Size(37, 63);
            this.dedo2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.dedo2.TabIndex = 12;
            this.dedo2.TabStop = false;
            this.dedo2.Visible = false;
            this.dedo2.Click += new System.EventHandler(this.dedo2_Click);
            // 
            // dedo3
            // 
            this.dedo3.Image = ((System.Drawing.Image)(resources.GetObject("dedo3.Image")));
            this.dedo3.Location = new System.Drawing.Point(320, 141);
            this.dedo3.Name = "dedo3";
            this.dedo3.Size = new System.Drawing.Size(32, 59);
            this.dedo3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.dedo3.TabIndex = 13;
            this.dedo3.TabStop = false;
            this.dedo3.Visible = false;
            this.dedo3.Click += new System.EventHandler(this.dedo3_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 141);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(232, 153);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Instrucciones";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(18, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(197, 106);
            this.label1.TabIndex = 15;
            this.label1.Text = "Por favor, seleccione el dedo que desea registrar para continuar con el proceso d" +
    "e registro.";
            // 
            // EnrollmentControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(774, 365);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dedo3);
            this.Controls.Add(this.dedo2);
            this.Controls.Add(this.dedo1);
            this.Controls.Add(this.dedo4);
            this.Controls.Add(this.dedo0);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Instrucciones);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.pbFingerprint);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtMessage);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(790, 404);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(790, 404);
            this.Name = "EnrollmentControl";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Enrollment";
            this.Closed += new System.EventHandler(this.frmEnrollment_Closed);
            this.Load += new System.EventHandler(this.EnrollmentControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbFingerprint)).EndInit();
            this.Instrucciones.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dedo0)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dedo4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dedo1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dedo2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dedo3)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.PictureBox pbFingerprint;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.GroupBox Instrucciones;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox dedo0;
        private System.Windows.Forms.PictureBox dedo4;
        private System.Windows.Forms.PictureBox dedo1;
        private System.Windows.Forms.PictureBox dedo2;
        private System.Windows.Forms.PictureBox dedo3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
    }
}
//! @endcond