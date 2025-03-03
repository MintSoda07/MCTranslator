namespace MCTranslator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            button1 = new Button();
            listBox1 = new ListBox();
            button2 = new Button();
            textBox1 = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            listBox2 = new ListBox();
            button3 = new Button();
            button4 = new Button();
            label5 = new Label();
            label6 = new Label();
            progressBar1 = new ProgressBar();
            richTextBox1 = new RichTextBox();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(1108, 515);
            button1.Name = "button1";
            button1.Size = new Size(110, 23);
            button1.TabIndex = 0;
            button1.Text = "번역";
            button1.UseVisualStyleBackColor = true;
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(12, 27);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(240, 454);
            listBox1.TabIndex = 1;
            // 
            // button2
            // 
            button2.Location = new Point(12, 515);
            button2.Name = "button2";
            button2.Size = new Size(486, 23);
            button2.TabIndex = 2;
            button2.Text = "폴더 선택";
            button2.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(576, 24);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(649, 23);
            textBox1.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(504, 27);
            label1.Name = "label1";
            label1.Size = new Size(66, 15);
            label1.TabIndex = 4;
            label1.Text = "GPT API 키";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = SystemColors.ControlDarkDark;
            label2.Location = new Point(576, 6);
            label2.Name = "label2";
            label2.Size = new Size(649, 15);
            label2.TabIndex = 5;
            label2.Text = "정확한 번역을 위해 GPT 4o를 사용하여 번역합니다. API 키를 입력하기 부담스럽다면, 번역 후 바로 키를 삭제해 주세요.";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 12);
            label3.Name = "label3";
            label3.Size = new Size(71, 15);
            label3.TabIndex = 6;
            label3.Text = "번역할 모드";
            label3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(222, 12);
            label4.Name = "label4";
            label4.Size = new Size(26, 15);
            label4.TabIndex = 7;
            label4.Text = "0개";
            label4.TextAlign = ContentAlignment.MiddleRight;
            // 
            // listBox2
            // 
            listBox2.FormattingEnabled = true;
            listBox2.ItemHeight = 15;
            listBox2.Location = new Point(258, 27);
            listBox2.Name = "listBox2";
            listBox2.Size = new Size(240, 454);
            listBox2.TabIndex = 8;
            // 
            // button3
            // 
            button3.Location = new Point(12, 486);
            button3.Name = "button3";
            button3.Size = new Size(236, 23);
            button3.TabIndex = 9;
            button3.Text = "번역 대상에서 제외";
            button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            button4.Location = new Point(258, 486);
            button4.Name = "button4";
            button4.Size = new Size(240, 23);
            button4.TabIndex = 10;
            button4.Text = "번역 대상에 추가";
            button4.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(472, 12);
            label5.Name = "label5";
            label5.Size = new Size(26, 15);
            label5.TabIndex = 11;
            label5.Text = "0개";
            label5.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(254, 12);
            label6.Name = "label6";
            label6.Size = new Size(71, 15);
            label6.TabIndex = 12;
            label6.Text = "제외된 모드";
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(504, 515);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(598, 23);
            progressBar1.TabIndex = 13;
            progressBar1.Visible = false;
            // 
            // richTextBox1
            // 
            richTextBox1.ImeMode = ImeMode.NoControl;
            richTextBox1.Location = new Point(504, 53);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.ReadOnly = true;
            richTextBox1.ScrollBars = RichTextBoxScrollBars.Vertical;
            richTextBox1.Size = new Size(714, 456);
            richTextBox1.TabIndex = 14;
            richTextBox1.Text = "";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1230, 550);
            Controls.Add(richTextBox1);
            Controls.Add(progressBar1);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(listBox2);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textBox1);
            Controls.Add(button2);
            Controls.Add(listBox1);
            Controls.Add(button1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "모드 번역기 1.0";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private ListBox listBox1;
        private Button button2;
        private TextBox textBox1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private ListBox listBox2;
        private Button button3;
        private Button button4;
        private Label label5;
        private Label label6;
        private ProgressBar progressBar1;
        private RichTextBox richTextBox1;
    }
}
