using System;
using System.IO;
using System.Windows.Forms;

namespace NotePad_
{
    public partial class Form1
    {
        /// <summary>
        /// ������ ��������������.
        /// </summary>
        private Timer autoSaveTimer;
        /// <summary>
        /// ��������� ���������� �������� ��������� �������.
        /// </summary>
        /// <param name="time"></param>
        private void SetTimerInterval(int time) => autoSaveTimer.Interval = time;

        /// <summary>
        /// ������ �������� ��������� ������� 30 ���.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AutoSave30sec_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.AutoSaveParam = 30_000;
            Properties.Settings.Default.Save();
            SetTimerInterval(Properties.Settings.Default.AutoSaveParam);
        }

        /// <summary>
        /// ������ �������� ��������� ������� 1 ���.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AutoSave1min_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.AutoSaveParam = 60_000;
            Properties.Settings.Default.Save();
            SetTimerInterval(Properties.Settings.Default.AutoSaveParam);
        }

        /// <summary>
        /// ������ �������� ��������� ������� 5 ���.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AutoSave5min_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.AutoSaveParam = 300_000;
            Properties.Settings.Default.Save();
            SetTimerInterval(Properties.Settings.Default.AutoSaveParam);
        }

        /// <summary>
        /// ������ �������� ��������� ������� 15 ���.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AutoSave15min_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.AutoSaveParam = 900_000;
            Properties.Settings.Default.Save();
            SetTimerInterval(Properties.Settings.Default.AutoSaveParam);
        }

        /// <summary>
        /// �������������� ������.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AutoSaveTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < listOfFilePaths.Count; i++)
                {
                    string filePath = listOfFilePaths[i];

                    if (!String.IsNullOrEmpty(filePath))
                    {
                        if (Path.GetExtension(filePath).ToLower() == ".txt" || Path.GetExtension(filePath).ToLower() == ".cs")
                            listOfTextBoxes[i].SaveFile(filePath, RichTextBoxStreamType.PlainText);
                        else if (Path.GetExtension(filePath).ToLower() == ".rtf")
                            listOfTextBoxes[i].SaveFile(filePath, RichTextBoxStreamType.RichText);
                        isSaved[i] = true;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Autosave error: unable to save file!", "Error",
                   MessageBoxButtons.OK, MessageBoxIcon.Error,
                   MessageBoxDefaultButton.Button1,
                   MessageBoxOptions.DefaultDesktopOnly);
            }
        }
    }
}

