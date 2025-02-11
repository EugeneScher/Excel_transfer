namespace OptimizatorDEMO
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSourceFile = new System.Windows.Forms.Button();
            this.txtSourceFile = new System.Windows.Forms.TextBox();
            this.btnDestinationFile = new System.Windows.Forms.Button();
            this.txtDestinationFile = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSourceCell = new System.Windows.Forms.TextBox();
            this.txtDestinationCell = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnTransferData = new System.Windows.Forms.Button();
            this.cmbSourceSheet = new System.Windows.Forms.ComboBox();
            this.cmbDestinationSheet = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.rbRange = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            //
            // btnSourceFile
            //
            this.btnSourceFile.Location = new System.Drawing.Point(392, 30);
            this.btnSourceFile.Name = "btnSourceFile";
            this.btnSourceFile.Size = new System.Drawing.Size(112, 23);
            this.btnSourceFile.TabIndex = 0;
            this.btnSourceFile.Text = "Выбрать файл";
            this.btnSourceFile.UseVisualStyleBackColor = true;
            this.btnSourceFile.Click += new System.EventHandler(this.btnSourceFile_Click);
            //
            // txtSourceFile
            //
            this.txtSourceFile.Location = new System.Drawing.Point(24, 32);
            this.txtSourceFile.Name = "txtSourceFile";
            this.txtSourceFile.ReadOnly = true;
            this.txtSourceFile.Size = new System.Drawing.Size(362, 20);
            this.txtSourceFile.TabIndex = 1;
            //
            // btnDestinationFile
            //
            this.btnDestinationFile.Location = new System.Drawing.Point(392, 79);
            this.btnDestinationFile.Name = "btnDestinationFile";
            this.btnDestinationFile.Size = new System.Drawing.Size(112, 23);
            this.btnDestinationFile.TabIndex = 2;
            this.btnDestinationFile.Text = "Выбрать файл";
            this.btnDestinationFile.UseVisualStyleBackColor = true;
            this.btnDestinationFile.Click += new System.EventHandler(this.btnDestinationFile_Click);
            //
            // txtDestinationFile
            //
            this.txtDestinationFile.Location = new System.Drawing.Point(24, 81);
            this.txtDestinationFile.Name = "txtDestinationFile";
            this.txtDestinationFile.ReadOnly = true;
            this.txtDestinationFile.Size = new System.Drawing.Size(362, 20);
            this.txtDestinationFile.TabIndex = 3;
            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Исходный файл:";
            //
            // label2
            //
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Целевой файл:";
            //
            // txtSourceCell
            //
            this.txtSourceCell.Location = new System.Drawing.Point(24, 181);
            this.txtSourceCell.Name = "txtSourceCell";
            this.txtSourceCell.Size = new System.Drawing.Size(161, 20);
            this.txtSourceCell.TabIndex = 6;
            //
            // txtDestinationCell
            //
            this.txtDestinationCell.Location = new System.Drawing.Point(209, 181);
            this.txtDestinationCell.Name = "txtDestinationCell";
            this.txtDestinationCell.Size = new System.Drawing.Size(161, 20);
            this.txtDestinationCell.TabIndex = 7;
            //
            // label3
            //
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 165);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Исходная ячейка/диапазон";
            //
            // label4
            //
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(206, 165);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(121, 13);
            this.label4.Text = "Целевая ячейка/диапазон";
            //
            // btnTransferData
            //
            this.btnTransferData.Location = new System.Drawing.Point(24, 256);
            this.btnTransferData.Name = "btnTransferData";
            this.btnTransferData.Size = new System.Drawing.Size(480, 23);
            this.btnTransferData.TabIndex = 10;
            this.btnTransferData.Text = "Перенести данные";
            this.btnTransferData.UseVisualStyleBackColor = true;
            this.btnTransferData.Click += new System.EventHandler(this.btnTransferData_Click);
            //
            // cmbSourceSheet
            //
            this.cmbSourceSheet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSourceSheet.FormattingEnabled = true;
            this.cmbSourceSheet.Location = new System.Drawing.Point(24, 130);
            this.cmbSourceSheet.Name = "cmbSourceSheet";
            this.cmbSourceSheet.Size = new System.Drawing.Size(161, 21);
            this.cmbSourceSheet.TabIndex = 11;
            //
            // cmbDestinationSheet
            //
            this.cmbDestinationSheet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDestinationSheet.FormattingEnabled = true;
            this.cmbDestinationSheet.Location = new System.Drawing.Point(209, 130);
            this.cmbDestinationSheet.Name = "cmbDestinationSheet";
            this.cmbDestinationSheet.Size = new System.Drawing.Size(161, 21);
            this.cmbDestinationSheet.TabIndex = 12;
            //
            // label5
            //
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 114);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Исходный лист";
            //
            // label6
            //
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(206, 114);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 13);
            this.label6.Text = "Целевой лист";
            //
            // progressBar
            //
            this.progressBar.Location = new System.Drawing.Point(24, 285);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(480, 23);
            this.progressBar.TabIndex = 14;
            this.progressBar.Visible = false;
            //
            // rbRange
            //
            this.rbRange.AutoSize = true;
            this.rbRange.Location = new System.Drawing.Point(24, 216);
            this.rbRange.Name = "rbRange";
            this.rbRange.Size = new System.Drawing.Size(65, 17);
            this.rbRange.TabIndex = 15;
            this.rbRange.TabStop = true;
            this.rbRange.Text = "Диапазон";
            this.rbRange.UseVisualStyleBackColor = true;
            //
            // MainForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(522, 323);
            this.Controls.Add(this.rbRange);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbDestinationSheet);
            this.Controls.Add(this.cmbSourceSheet);
            this.Controls.Add(this.btnTransferData);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtDestinationCell);
            this.Controls.Add(this.txtSourceCell);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtDestinationFile);
            this.Controls.Add(this.btnDestinationFile);
            this.Controls.Add(this.txtSourceFile);
            this.Controls.Add(this.btnSourceFile);
            this.Name = "MainForm";
            this.Text = "Перенос данных из Excel";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSourceFile;
        private System.Windows.Forms.TextBox txtSourceFile;
        private System.Windows.Forms.Button btnDestinationFile;
        private System.Windows.Forms.TextBox txtDestinationFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSourceCell;
        private System.Windows.Forms.TextBox txtDestinationCell;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnTransferData;
        private System.Windows.Forms.ComboBox cmbSourceSheet;
        private System.Windows.Forms.ComboBox cmbDestinationSheet;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.RadioButton rbRange;
    }
}

