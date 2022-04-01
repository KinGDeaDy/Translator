namespace Translator
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.debugTextBox = new Guna.UI.WinForms.GunaTextBox();
            this.outputLabel = new Guna.UI.WinForms.GunaLabel();
            this.programmLabel = new Guna.UI.WinForms.GunaLabel();
            this.bnfLabel = new Guna.UI.WinForms.GunaLabel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.runButton = new Guna.UI.WinForms.GunaButton();
            this.inputTextBox = new FastColoredTextBoxNS.FastColoredTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.inputTextBox)).BeginInit();
            this.SuspendLayout();
            // 
            // debugTextBox
            // 
            this.debugTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.debugTextBox.BaseColor = System.Drawing.Color.White;
            this.debugTextBox.BorderColor = System.Drawing.Color.Silver;
            this.debugTextBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.debugTextBox.FocusedBaseColor = System.Drawing.Color.White;
            this.debugTextBox.FocusedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.debugTextBox.FocusedForeColor = System.Drawing.SystemColors.ControlText;
            this.debugTextBox.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.debugTextBox.ForeColor = System.Drawing.Color.Red;
            this.debugTextBox.Location = new System.Drawing.Point(12, 537);
            this.debugTextBox.Multiline = true;
            this.debugTextBox.Name = "debugTextBox";
            this.debugTextBox.PasswordChar = '\0';
            this.debugTextBox.ReadOnly = true;
            this.debugTextBox.SelectedText = "";
            this.debugTextBox.Size = new System.Drawing.Size(1528, 116);
            this.debugTextBox.TabIndex = 2;
            // 
            // outputLabel
            // 
            this.outputLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.outputLabel.AutoSize = true;
            this.outputLabel.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.outputLabel.ForeColor = System.Drawing.Color.White;
            this.outputLabel.Location = new System.Drawing.Point(12, 462);
            this.outputLabel.Margin = new System.Windows.Forms.Padding(10);
            this.outputLabel.Name = "outputLabel";
            this.outputLabel.Size = new System.Drawing.Size(96, 37);
            this.outputLabel.TabIndex = 3;
            this.outputLabel.Text = "Вывод";
            // 
            // programmLabel
            // 
            this.programmLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.programmLabel.AutoSize = true;
            this.programmLabel.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.programmLabel.ForeColor = System.Drawing.Color.White;
            this.programmLabel.Location = new System.Drawing.Point(5, 53);
            this.programmLabel.Margin = new System.Windows.Forms.Padding(10);
            this.programmLabel.Name = "programmLabel";
            this.programmLabel.Size = new System.Drawing.Size(160, 37);
            this.programmLabel.TabIndex = 4;
            this.programmLabel.Text = "Программа";
            // 
            // bnfLabel
            // 
            this.bnfLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bnfLabel.AutoSize = true;
            this.bnfLabel.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.bnfLabel.ForeColor = System.Drawing.Color.White;
            this.bnfLabel.Location = new System.Drawing.Point(660, 53);
            this.bnfLabel.Margin = new System.Windows.Forms.Padding(10);
            this.bnfLabel.Name = "bnfLabel";
            this.bnfLabel.Size = new System.Drawing.Size(275, 37);
            this.bnfLabel.TabIndex = 5;
            this.bnfLabel.Text = "Форма Бэкуса-Наура";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Font = new System.Drawing.Font("Segoe UI", 14.2F);
            this.textBox1.Location = new System.Drawing.Point(657, 103);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(883, 324);
            this.textBox1.TabIndex = 6;
            this.textBox1.Text = resources.GetString("textBox1.Text");
            this.textBox1.WordWrap = false;
            // 
            // runButton
            // 
            this.runButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.runButton.AnimationHoverSpeed = 0.07F;
            this.runButton.AnimationSpeed = 0.003F;
            this.runButton.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.runButton.BorderColor = System.Drawing.Color.Black;
            this.runButton.DialogResult = System.Windows.Forms.DialogResult.None;
            this.runButton.FocusedColor = System.Drawing.Color.Empty;
            this.runButton.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.runButton.ForeColor = System.Drawing.Color.White;
            this.runButton.Image = null;
            this.runButton.ImageSize = new System.Drawing.Size(20, 20);
            this.runButton.Location = new System.Drawing.Point(680, 683);
            this.runButton.Name = "runButton";
            this.runButton.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(143)))), ((int)(((byte)(255)))));
            this.runButton.OnHoverBorderColor = System.Drawing.Color.Black;
            this.runButton.OnHoverForeColor = System.Drawing.Color.White;
            this.runButton.OnHoverImage = null;
            this.runButton.OnPressedColor = System.Drawing.Color.Black;
            this.runButton.Size = new System.Drawing.Size(255, 116);
            this.runButton.TabIndex = 9;
            this.runButton.Text = "Run";
            this.runButton.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.runButton.Click += new System.EventHandler(this.runButton_Click);
            // 
            // inputTextBox
            // 
            this.inputTextBox.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.inputTextBox.AutoIndentCharsPatterns = "^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;=]+);\r\n^\\s*(case|default)\\s*[^:]*" +
    "(?<range>:)\\s*(?<range>[^;]+);";
            this.inputTextBox.AutoScrollMinSize = new System.Drawing.Size(0, 26);
            this.inputTextBox.BackBrush = null;
            this.inputTextBox.CharHeight = 26;
            this.inputTextBox.CharWidth = 14;
            this.inputTextBox.CurrentPenSize = 3;
            this.inputTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.inputTextBox.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.inputTextBox.DocumentPath = null;
            this.inputTextBox.Font = new System.Drawing.Font("Courier New", 13.8F);
            this.inputTextBox.IsReplaceMode = false;
            this.inputTextBox.LineNumberColor = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(143)))), ((int)(((byte)(255)))));
            this.inputTextBox.Location = new System.Drawing.Point(12, 103);
            this.inputTextBox.Name = "inputTextBox";
            this.inputTextBox.Paddings = new System.Windows.Forms.Padding(0);
            this.inputTextBox.SelectionChangedDelayedEnabled = false;
            this.inputTextBox.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.inputTextBox.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("inputTextBox.ServiceColors")));
            this.inputTextBox.Size = new System.Drawing.Size(639, 324);
            this.inputTextBox.TabIndex = 10;
            this.inputTextBox.WordWrap = true;
            this.inputTextBox.Zoom = 100;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(25)))), ((int)(((byte)(53)))));
            this.ClientSize = new System.Drawing.Size(1562, 811);
            this.Controls.Add(this.inputTextBox);
            this.Controls.Add(this.runButton);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.bnfLabel);
            this.Controls.Add(this.programmLabel);
            this.Controls.Add(this.outputLabel);
            this.Controls.Add(this.debugTextBox);
            this.MinimumSize = new System.Drawing.Size(1580, 858);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.inputTextBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Guna.UI.WinForms.GunaTextBox debugTextBox;
        private Guna.UI.WinForms.GunaLabel outputLabel;
        private Guna.UI.WinForms.GunaLabel programmLabel;
        private Guna.UI.WinForms.GunaLabel bnfLabel;
        private System.Windows.Forms.TextBox textBox1;
        private Guna.UI.WinForms.GunaButton runButton;
        private FastColoredTextBoxNS.FastColoredTextBox inputTextBox;
    }
}

