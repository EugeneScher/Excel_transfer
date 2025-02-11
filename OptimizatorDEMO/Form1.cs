using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using OfficeOpenXml;
using System.Diagnostics;  // Для Debug.WriteLine

namespace OptimizatorDEMO
{
    public partial class Form1 : Form
    {
        private string sourceFilePath;
        private string destinationFilePath;
        private BackgroundWorker bw;

        public Form1()
        {
            InitializeComponent();
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            bw = new BackgroundWorker();
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);

            rbRange.Checked = true; // По умолчанию выбран перенос диапазона
        }

        private void btnSourceFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
            openFileDialog.Title = "Выберите исходный файл Excel";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                sourceFilePath = openFileDialog.FileName;
                txtSourceFile.Text = sourceFilePath;
                LoadSheetNames(sourceFilePath, cmbSourceSheet);
            }
        }

        private void btnDestinationFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
            openFileDialog.Title = "Выберите целевой файл Excel";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                destinationFilePath = openFileDialog.FileName;
                txtDestinationFile.Text = destinationFilePath;
                LoadSheetNames(destinationFilePath, cmbDestinationSheet);
            }
        }

        private async void btnTransferData_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(sourceFilePath) || string.IsNullOrEmpty(destinationFilePath))
            {
                MessageBox.Show("Пожалуйста, выберите исходный и целевой файлы.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(txtSourceCell.Text) || string.IsNullOrEmpty(txtDestinationCell.Text))
            {
                MessageBox.Show("Пожалуйста, укажите исходную и целевую ячейки/диапазоны.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (cmbSourceSheet.SelectedItem == null || cmbDestinationSheet.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, выберите листы.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            progressBar.Value = 0;
            progressBar.Visible = true;
            btnTransferData.Enabled = false;

            TransferDataArgs args = new TransferDataArgs
            {
                SourceFile = sourceFilePath,
                DestinationFile = destinationFilePath,
                SourceSheetName = cmbSourceSheet.SelectedItem.ToString(),
                DestinationSheetName = cmbDestinationSheet.SelectedItem.ToString(),
                SourceCells = txtSourceCell.Text, // Теперь передаем строку с ячейками/диапазоном
                DestinationCells = txtDestinationCell.Text
            };

            bw.RunWorkerAsync(args);
        }

        private class TransferDataArgs
        {
            public string SourceFile { get; set; }
            public string DestinationFile { get; set; }
            public string SourceSheetName { get; set; }
            public string DestinationSheetName { get; set; }
            public string SourceCells { get; set; } // Строка с ячейками/диапазоном
            public string DestinationCells { get; set; } // Строка с ячейками/диапазоном
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            Debug.WriteLine("bw_DoWork: вход"); // ЛОГ
            BackgroundWorker worker = sender as BackgroundWorker;
            TransferDataArgs args = (TransferDataArgs)e.Argument;

            try
            {
                if (rbRange.Checked)
                {
                    TransferDataRange(args.SourceFile, args.DestinationFile, args.SourceSheetName, args.DestinationSheetName, args.SourceCells, args.DestinationCells, worker, e);
                }

                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    Debug.WriteLine("bw_DoWork: отмена запрошена, выход"); // ЛОГ
                    return;
                }

                worker.ReportProgress(100); // Сообщаем о завершении
                Debug.WriteLine("bw_DoWork: завершено успешно"); // ЛОГ
            }
            catch (Exception ex)
            {
                e.Result = ex.Message;
                e.Cancel = true;
                Debug.WriteLine($"bw_DoWork: исключение: {ex.Message}"); // ЛОГ
            }
            Debug.WriteLine("bw_DoWork: выход"); // ЛОГ
        }

        // Метод для переноса диапазона (A1:B2)
        private void TransferDataRange(string sourceFile, string destinationFile, string sourceSheetName, string destinationSheetName, string sourceRange, string destinationRange, BackgroundWorker worker, DoWorkEventArgs e)
        {
            Debug.WriteLine("TransferDataRange: вход");

            FileInfo sourceFileInfo = new FileInfo(sourceFile);
            FileInfo destinationFileInfo = new FileInfo(destinationFile);

            using (ExcelPackage sourcePackage = new ExcelPackage(sourceFileInfo))
            using (ExcelPackage destinationPackage = new ExcelPackage(destinationFileInfo))
            {
                ExcelWorksheet sourceWorksheet = sourcePackage.Workbook.Worksheets[sourceSheetName];
                ExcelWorksheet destinationWorksheet = destinationPackage.Workbook.Worksheets[destinationSheetName];

                if (sourceWorksheet == null) throw new Exception($"Лист '{sourceSheetName}' не найден в исходном файле.");
                if (destinationWorksheet == null) throw new Exception($"Лист '{destinationSheetName}' не найден в целевом файле.");

                ExcelAddress sourceExcelAddress = new ExcelAddress(sourceRange);
                ExcelAddress destinationExcelAddress = new ExcelAddress(destinationRange);

                int startRow = sourceExcelAddress.Start.Row;
                int startColumn = sourceExcelAddress.Start.Column; // Получаем числовой индекс столбца
                int endRow = sourceExcelAddress.End.Row;
                int endColumn = sourceExcelAddress.End.Column;

                int destStartRow = destinationExcelAddress.Start.Row;
                int destStartColumn = destinationExcelAddress.Start.Column; // Получаем числовой индекс столбца

                int rowCount = endRow - startRow + 1;
                int colCount = endColumn - startColumn + 1;

                //Проверка, что диапазоны одинакового размера
                if ((destinationExcelAddress.End.Row - destinationExcelAddress.Start.Row + 1) != rowCount ||
                    (destinationExcelAddress.End.Column - destinationExcelAddress.Start.Column + 1) != colCount)
                {
                    throw new Exception("Исходный и целевой диапазоны должны быть одинакового размера");
                }

                int totalCells = rowCount * colCount;
                int processedCells = 0;

                for (int rowOffset = 0; rowOffset < rowCount; rowOffset++)
                {
                    for (int colOffset = 0; colOffset < colCount; colOffset++)
                    {
                        Debug.WriteLine($"TransferDataRange: worker.CancellationPending = {worker.CancellationPending}");
                        if (worker.CancellationPending)
                        {
                            e.Cancel = true;
                            Debug.WriteLine("TransferDataRange: отмена запрошена, выход из цикла");
                            return;
                        }

                        int sourceColumnIndex = startColumn + colOffset;
                        int destinationColumnIndex = destStartColumn + colOffset;

                        string currentSourceCell = ColumnIndexToColumnLetter(sourceColumnIndex) + (startRow + rowOffset).ToString();
                        string currentDestinationCell = ColumnIndexToColumnLetter(destinationColumnIndex) + (destStartRow + rowOffset).ToString();

                        object cellValue = sourceWorksheet.Cells[currentSourceCell].Value;
                        destinationWorksheet.Cells[currentDestinationCell].Value = cellValue;

                        processedCells++;
                        worker.ReportProgress((int)((double)processedCells / totalCells * 100));
                    }
                }

                if (!worker.CancellationPending)
                {
                    Debug.WriteLine("TransferDataRange: сохраняем файл");
                    destinationPackage.Save();
                }
                else
                {
                    Debug.WriteLine("TransferDataRange: отмена запрошена, файл не сохранен");
                }
            }
            Debug.WriteLine("TransferDataRange: выход");
        }

        private string ColumnIndexToColumnLetter(int columnIndex)
        {
            string columnLetter = "";
            while (columnIndex > 0)
            {
                int modulo = (columnIndex - 1) % 26;
                columnLetter = Convert.ToChar('A' + modulo).ToString() + columnLetter;
                columnIndex = (columnIndex - modulo) / 26;
            }
            return columnLetter;
        }



        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Debug.WriteLine("bw_RunWorkerCompleted: вход"); // ЛОГ
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    progressBar.Visible = false;
                    btnTransferData.Enabled = true;

                    if (e.Cancelled) // Проверяем, была ли отмена
                    {
                        MessageBox.Show($"Операция отменена пользователем: {e.Result}", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        Debug.WriteLine($"bw_RunWorkerCompleted: отмена пользователем: {e.Result}"); // ЛОГ
                    }
                    else if (e.Error != null)
                    {
                        MessageBox.Show($"Произошла ошибка: {e.Error.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Debug.WriteLine($"bw_RunWorkerCompleted: исключение: {e.Error.Message}"); // ЛОГ
                    }
                    else
                    {
                        MessageBox.Show("Данные успешно перенесены!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Debug.WriteLine("bw_RunWorkerCompleted: завершено успешно"); // ЛОГ
                    }
                }));
            }
            else
            {
                progressBar.Visible = false;
                btnTransferData.Enabled = true;

                if (e.Cancelled) // Проверяем, была ли отмена
                {
                    MessageBox.Show($"Операция отменена пользователем: {e.Result}", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Debug.WriteLine($"bw_RunWorkerCompleted: отмена пользователем: {e.Result}"); // ЛОГ
                }
                else if (e.Error != null)
                {
                    MessageBox.Show($"Произошла ошибка: {e.Error.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Debug.WriteLine($"bw_RunWorkerCompleted: исключение: {e.Error.Message}"); // ЛОГ
                }
                else
                {
                    MessageBox.Show("Данные успешно перенесены!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Debug.WriteLine("bw_RunWorkerCompleted: завершено успешно"); // ЛОГ
                }
            }
            Debug.WriteLine("bw_RunWorkerCompleted: выход"); // ЛОГ
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (bw.IsBusy)
            {
                bw.CancelAsync();
                e.Cancel = true; // Предотвращаем немедленное закрытие формы
            }
        }

        private void LoadSheetNames(string filePath, System.Windows.Forms.ComboBox comboBox)
        {
            comboBox.Items.Clear();

            try
            {
                FileInfo fileInfo = new FileInfo(filePath);

                using (ExcelPackage package = new ExcelPackage(fileInfo))
                {
                    if (package.Workbook != null && package.Workbook.Worksheets != null)
                    {
                        foreach (var worksheet in package.Workbook.Worksheets)
                        {
                            comboBox.Items.Add(worksheet.Name);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке имен листов: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}