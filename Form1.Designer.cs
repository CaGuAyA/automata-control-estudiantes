namespace RCAMREC
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            captureImgBox = new Emgu.CV.UI.ImageBox();
            lbl_FaceName = new Label();
            textBox1 = new TextBox();
            label1 = new Label();
            button1 = new Button();
            comboBox1 = new ComboBox();
            button2 = new Button();
            label2 = new Label();
            button3 = new Button();
            timer1 = new System.Windows.Forms.Timer(components);
            button4 = new Button();
            current_detected = new Label();
            ((System.ComponentModel.ISupportInitialize)captureImgBox).BeginInit();
            SuspendLayout();
            // 
            // captureImgBox
            // 
            captureImgBox.Location = new Point(206, 12);
            captureImgBox.Name = "captureImgBox";
            captureImgBox.Size = new Size(582, 336);
            captureImgBox.TabIndex = 2;
            captureImgBox.TabStop = false;
            captureImgBox.Click += captureImgBox_Click;
            // 
            // lbl_FaceName
            // 
            lbl_FaceName.AutoSize = true;
            lbl_FaceName.Location = new Point(33, 33);
            lbl_FaceName.Name = "lbl_FaceName";
            lbl_FaceName.Size = new Size(137, 15);
            lbl_FaceName.TabIndex = 3;
            lbl_FaceName.Text = "Nombre de nuevo rostro";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(33, 51);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(121, 23);
            textBox1.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(33, 91);
            label1.Name = "label1";
            label1.Size = new Size(109, 15);
            label1.TabIndex = 5;
            label1.Text = "Nombre de Rostros";
            // 
            // button1
            // 
            button1.Location = new Point(34, 159);
            button1.Name = "button1";
            button1.Size = new Size(120, 54);
            button1.TabIndex = 6;
            button1.Text = "Recordar cara";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(33, 109);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(121, 23);
            comboBox1.TabIndex = 7;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // button2
            // 
            button2.Location = new Point(46, 375);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 8;
            button2.Text = "Stop";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(236, 383);
            label2.Name = "label2";
            label2.Size = new Size(160, 15);
            label2.TabIndex = 9;
            label2.Text = "Nombre del rostro detectado";
            // 
            // button3
            // 
            button3.Location = new Point(668, 383);
            button3.Name = "button3";
            button3.Size = new Size(75, 23);
            button3.TabIndex = 10;
            button3.Text = "Reconocer";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            // 
            // button4
            // 
            button4.Location = new Point(34, 253);
            button4.Name = "button4";
            button4.Size = new Size(121, 56);
            button4.TabIndex = 11;
            button4.Text = "Entrenar reconocimiento";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // current_detected
            // 
            current_detected.AutoSize = true;
            current_detected.Location = new Point(419, 383);
            current_detected.Name = "current_detected";
            current_detected.Size = new Size(97, 15);
            current_detected.TabIndex = 12;
            current_detected.Text = "Rostro detectado";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(current_detected);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(label2);
            Controls.Add(button2);
            Controls.Add(comboBox1);
            Controls.Add(button1);
            Controls.Add(label1);
            Controls.Add(textBox1);
            Controls.Add(lbl_FaceName);
            Controls.Add(captureImgBox);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)captureImgBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Emgu.CV.UI.ImageBox captureImgBox;
        private Label lbl_FaceName;
        private TextBox textBox1;
        private Label label1;
        private Button button1;
        private ComboBox comboBox1;
        private Button button2;
        private Label label2;
        private Button button3;
        private System.Windows.Forms.Timer timer1;
        private Button button4;
        private Label current_detected;
    }
}
