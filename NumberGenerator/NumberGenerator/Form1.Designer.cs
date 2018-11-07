namespace NumberGenerator
{
    partial class Main
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
            this.panelGenerate = new System.Windows.Forms.Panel();
            this.lCountThreads_Generate = new System.Windows.Forms.Label();
            this.lCountNumbers = new System.Windows.Forms.Label();
            this.cmbCountThreads_Generate = new System.Windows.Forms.ComboBox();
            this.tCountNumbers = new System.Windows.Forms.TextBox();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnLoadStopContinue = new System.Windows.Forms.Button();
            this.panelGrid = new System.Windows.Forms.Panel();
            this.DataGrid = new System.Windows.Forms.DataGridView();
            this.panelResult = new System.Windows.Forms.Panel();
            this.rtResult = new System.Windows.Forms.RichTextBox();
            this.cmbCountThreads_MaxValue = new System.Windows.Forms.ComboBox();
            this.btnSearchMaxValue = new System.Windows.Forms.Button();
            this.lCountThreads_MaxValue = new System.Windows.Forms.Label();
            this.panelSearchMaxValue = new System.Windows.Forms.Panel();
            this.panelGenerateStopContinue = new System.Windows.Forms.Panel();
            this.panelGenerate.SuspendLayout();
            this.panelGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGrid)).BeginInit();
            this.panelResult.SuspendLayout();
            this.panelSearchMaxValue.SuspendLayout();
            this.panelGenerateStopContinue.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelGenerate
            // 
            this.panelGenerate.Controls.Add(this.lCountThreads_Generate);
            this.panelGenerate.Controls.Add(this.lCountNumbers);
            this.panelGenerate.Controls.Add(this.cmbCountThreads_Generate);
            this.panelGenerate.Controls.Add(this.tCountNumbers);
            this.panelGenerate.Controls.Add(this.btnGenerate);
            this.panelGenerate.Location = new System.Drawing.Point(12, 12);
            this.panelGenerate.Name = "panelGenerate";
            this.panelGenerate.Size = new System.Drawing.Size(290, 101);
            this.panelGenerate.TabIndex = 0;
            // 
            // lCountThreads_Generate
            // 
            this.lCountThreads_Generate.AutoSize = true;
            this.lCountThreads_Generate.Location = new System.Drawing.Point(3, 32);
            this.lCountThreads_Generate.Name = "lCountThreads_Generate";
            this.lCountThreads_Generate.Size = new System.Drawing.Size(35, 13);
            this.lCountThreads_Generate.TabIndex = 6;
            this.lCountThreads_Generate.Text = "label2";
            // 
            // lCountNumbers
            // 
            this.lCountNumbers.AutoSize = true;
            this.lCountNumbers.Location = new System.Drawing.Point(3, 6);
            this.lCountNumbers.Name = "lCountNumbers";
            this.lCountNumbers.Size = new System.Drawing.Size(35, 13);
            this.lCountNumbers.TabIndex = 5;
            this.lCountNumbers.Text = "label1";
            // 
            // cmbCountThreads_Generate
            // 
            this.cmbCountThreads_Generate.FormattingEnabled = true;
            this.cmbCountThreads_Generate.Location = new System.Drawing.Point(166, 29);
            this.cmbCountThreads_Generate.Name = "cmbCountThreads_Generate";
            this.cmbCountThreads_Generate.Size = new System.Drawing.Size(121, 21);
            this.cmbCountThreads_Generate.TabIndex = 2;
            // 
            // tCountNumbers
            // 
            this.tCountNumbers.Location = new System.Drawing.Point(166, 3);
            this.tCountNumbers.Name = "tCountNumbers";
            this.tCountNumbers.Size = new System.Drawing.Size(121, 20);
            this.tCountNumbers.TabIndex = 1;
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(3, 56);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(284, 32);
            this.btnGenerate.TabIndex = 0;
            this.btnGenerate.Text = "button1";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // btnLoadStopContinue
            // 
            this.btnLoadStopContinue.Location = new System.Drawing.Point(3, 3);
            this.btnLoadStopContinue.Name = "btnLoadStopContinue";
            this.btnLoadStopContinue.Size = new System.Drawing.Size(284, 32);
            this.btnLoadStopContinue.TabIndex = 1;
            this.btnLoadStopContinue.Text = "button1";
            this.btnLoadStopContinue.UseVisualStyleBackColor = true;
            this.btnLoadStopContinue.Click += new System.EventHandler(this.loadStop_Click);
            // 
            // panelGrid
            // 
            this.panelGrid.Controls.Add(this.DataGrid);
            this.panelGrid.Location = new System.Drawing.Point(308, 12);
            this.panelGrid.Name = "panelGrid";
            this.panelGrid.Size = new System.Drawing.Size(400, 437);
            this.panelGrid.TabIndex = 1;
            // 
            // DataGrid
            // 
            this.DataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGrid.Location = new System.Drawing.Point(3, 3);
            this.DataGrid.Name = "DataGrid";
            this.DataGrid.Size = new System.Drawing.Size(394, 431);
            this.DataGrid.TabIndex = 0;
            // 
            // panelResult
            // 
            this.panelResult.Controls.Add(this.rtResult);
            this.panelResult.Location = new System.Drawing.Point(714, 12);
            this.panelResult.Name = "panelResult";
            this.panelResult.Size = new System.Drawing.Size(258, 437);
            this.panelResult.TabIndex = 2;
            // 
            // rtResult
            // 
            this.rtResult.Location = new System.Drawing.Point(3, 3);
            this.rtResult.Name = "rtResult";
            this.rtResult.Size = new System.Drawing.Size(252, 431);
            this.rtResult.TabIndex = 3;
            this.rtResult.Text = "";
            // 
            // cmbCountThreads_MaxValue
            // 
            this.cmbCountThreads_MaxValue.FormattingEnabled = true;
            this.cmbCountThreads_MaxValue.Location = new System.Drawing.Point(166, 3);
            this.cmbCountThreads_MaxValue.Name = "cmbCountThreads_MaxValue";
            this.cmbCountThreads_MaxValue.Size = new System.Drawing.Size(121, 21);
            this.cmbCountThreads_MaxValue.TabIndex = 3;
            // 
            // btnSearchMaxValue
            // 
            this.btnSearchMaxValue.Location = new System.Drawing.Point(3, 30);
            this.btnSearchMaxValue.Name = "btnSearchMaxValue";
            this.btnSearchMaxValue.Size = new System.Drawing.Size(284, 32);
            this.btnSearchMaxValue.TabIndex = 4;
            this.btnSearchMaxValue.Text = "button1";
            this.btnSearchMaxValue.UseVisualStyleBackColor = true;
            this.btnSearchMaxValue.Click += new System.EventHandler(this.btnSearchMaxValue_Click);
            // 
            // lCountThreads_MaxValue
            // 
            this.lCountThreads_MaxValue.AutoSize = true;
            this.lCountThreads_MaxValue.Location = new System.Drawing.Point(3, 6);
            this.lCountThreads_MaxValue.Name = "lCountThreads_MaxValue";
            this.lCountThreads_MaxValue.Size = new System.Drawing.Size(35, 13);
            this.lCountThreads_MaxValue.TabIndex = 7;
            this.lCountThreads_MaxValue.Text = "label3";
            // 
            // panelSearchMaxValue
            // 
            this.panelSearchMaxValue.Controls.Add(this.lCountThreads_MaxValue);
            this.panelSearchMaxValue.Controls.Add(this.cmbCountThreads_MaxValue);
            this.panelSearchMaxValue.Controls.Add(this.btnSearchMaxValue);
            this.panelSearchMaxValue.Location = new System.Drawing.Point(12, 171);
            this.panelSearchMaxValue.Name = "panelSearchMaxValue";
            this.panelSearchMaxValue.Size = new System.Drawing.Size(290, 76);
            this.panelSearchMaxValue.TabIndex = 3;
            // 
            // panelGenerateStopContinue
            // 
            this.panelGenerateStopContinue.Controls.Add(this.btnLoadStopContinue);
            this.panelGenerateStopContinue.Location = new System.Drawing.Point(12, 119);
            this.panelGenerateStopContinue.Name = "panelGenerateStopContinue";
            this.panelGenerateStopContinue.Size = new System.Drawing.Size(290, 46);
            this.panelGenerateStopContinue.TabIndex = 4;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 461);
            this.Controls.Add(this.panelGenerateStopContinue);
            this.Controls.Add(this.panelSearchMaxValue);
            this.Controls.Add(this.panelResult);
            this.Controls.Add(this.panelGrid);
            this.Controls.Add(this.panelGenerate);
            this.Name = "Main";
            this.Text = "Main";
            this.panelGenerate.ResumeLayout(false);
            this.panelGenerate.PerformLayout();
            this.panelGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGrid)).EndInit();
            this.panelResult.ResumeLayout(false);
            this.panelSearchMaxValue.ResumeLayout(false);
            this.panelSearchMaxValue.PerformLayout();
            this.panelGenerateStopContinue.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelGenerate;
        private System.Windows.Forms.Panel panelGrid;
        private System.Windows.Forms.DataGridView DataGrid;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button btnLoadStopContinue;
        private System.Windows.Forms.Panel panelResult;
        private System.Windows.Forms.RichTextBox rtResult;
        private System.Windows.Forms.Button btnSearchMaxValue;
        private System.Windows.Forms.ComboBox cmbCountThreads_MaxValue;
        private System.Windows.Forms.ComboBox cmbCountThreads_Generate;
        private System.Windows.Forms.TextBox tCountNumbers;
        private System.Windows.Forms.Label lCountThreads_MaxValue;
        private System.Windows.Forms.Label lCountThreads_Generate;
        private System.Windows.Forms.Label lCountNumbers;
        private System.Windows.Forms.Panel panelSearchMaxValue;
        private System.Windows.Forms.Panel panelGenerateStopContinue;
    }
}

