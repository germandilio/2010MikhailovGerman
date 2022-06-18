using System;
using System.IO;
using System.Windows.Forms;

namespace NotePad_
{
    public partial class Form1
    {
        /// <summary>
        /// Таймер журналирования.
        /// </summary>
        private Timer LoggingTimer;
        private readonly string LogDirectoryPath = "..\\..\\..\\Resources\\Logging";
        /// <summary>
        /// Установка значения интервала таймера.
        /// </summary>
        /// <param name="time"></param>
        private void SetLoggingInterval(int time) => LoggingTimer.Interval = time;

        /// <summary>
        /// Интервал журналирования 10 минут.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Logging10min_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.LoggingTimer = 600_000;
            Properties.Settings.Default.Save();
            SetLoggingInterval(Properties.Settings.Default.LoggingTimer);
        }

        /// <summary>
        /// Интервал журналирования 1 минут.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Logging1min_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.LoggingTimer = 60_000;
            Properties.Settings.Default.Save();
            SetLoggingInterval(Properties.Settings.Default.LoggingTimer);
        }

        /// <summary>
        /// Интервал журналирования 5 минут.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Logging5min_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.LoggingTimer = 300_000;
            Properties.Settings.Default.Save();
            SetLoggingInterval(Properties.Settings.Default.LoggingTimer);
        }

        /// <summary>
        /// Откат текущего файла в последнее сохраненное состояние.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PreviousVersion_Click(object sender, EventArgs e)
        {
            try
            {
                if (TabControl1.SelectedIndex != -1)
                {
                    string name = String.Empty;
                    if (!String.IsNullOrEmpty(listOfFilePaths[TabControl1.SelectedIndex]))
                        name = Path.GetFileName(listOfFilePaths[TabControl1.SelectedIndex]);
                    else
                        name = TabControl1.TabPages[TabControl1.SelectedIndex].Text + ".rtf";

                    string LoggingfilePath = Path.Combine(LogDirectoryPath, name);

                    if (Path.GetExtension(LoggingfilePath).ToLower() == ".txt" || Path.GetExtension(LoggingfilePath).ToLower() == ".cs")
                        listOfTextBoxes[TabControl1.SelectedIndex].LoadFile(LoggingfilePath, RichTextBoxStreamType.PlainText);
                    else if (Path.GetExtension(LoggingfilePath).ToLower() == ".rtf")
                        listOfTextBoxes[TabControl1.SelectedIndex].LoadFile(LoggingfilePath, RichTextBoxStreamType.RichText);
                }
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Previous version of file not found!", "Warning",
                   MessageBoxButtons.OK, MessageBoxIcon.Warning,
                   MessageBoxDefaultButton.Button1,
                   MessageBoxOptions.DefaultDesktopOnly);
            }
            catch (Exception)
            {
                MessageBox.Show("Error in logging files!", "Error",
                  MessageBoxButtons.OK, MessageBoxIcon.Error,
                  MessageBoxDefaultButton.Button1,
                  MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        /// <summary>
        /// Создание копий открытых файлов.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoggingTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < listOfFilePaths.Count; i++)
                {
                    string filePath = listOfFilePaths[i];

                    if (!String.IsNullOrEmpty(filePath))
                    {
                        string name = Path.GetFileName(filePath);
                        filePath = Path.Combine(LogDirectoryPath, name);
                        if (Path.GetExtension(filePath).ToLower() == ".txt" || Path.GetExtension(filePath).ToLower() == ".cs")
                            listOfTextBoxes[i].SaveFile(filePath, RichTextBoxStreamType.PlainText);
                        else if (Path.GetExtension(filePath).ToLower() == ".rtf")
                            listOfTextBoxes[i].SaveFile(filePath, RichTextBoxStreamType.RichText);
                    }
                    else
                    {
                        string name = TabControl1.TabPages[i].Text + ".rtf";
                        filePath = Path.Combine(LogDirectoryPath, name);
                        listOfTextBoxes[i].SaveFile(filePath, RichTextBoxStreamType.RichText);
                    }
                    isSaved[i] = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Logging error: impossible to create copy! ", "Warning",
                   MessageBoxButtons.OK, MessageBoxIcon.Warning,
                   MessageBoxDefaultButton.Button1,
                   MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        /// <summary>
        /// Очистка логов при закрытии приложения.
        /// </summary>
        private void DeleteAllLogs()
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(LogDirectoryPath);

                foreach (FileInfo file in di.EnumerateFiles())
                {
                    file.Delete();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error deleting log files!", "Warning",
                   MessageBoxButtons.OK, MessageBoxIcon.Warning,
                   MessageBoxDefaultButton.Button1,
                   MessageBoxOptions.DefaultDesktopOnly);
            }

        }
    }
}

