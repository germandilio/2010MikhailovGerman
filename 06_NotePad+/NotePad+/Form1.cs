using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;


namespace NotePad_
{
    public partial class Form1 : Form
    {
        private readonly List<RichTextBox> listOfTextBoxes;
        private readonly List<string> listOfFilePaths;
        private readonly List<bool> isSaved;
        // Путь к файлу с ссылками на последние открытые документы.
        private readonly string fileLogName = "..\\..\\..\\Resources\\LinksToOpenedFiles\\Links.txt";

        public Form1()
        {
            listOfTextBoxes = new List<RichTextBox>();
            listOfFilePaths = new List<string>();
            isSaved = new List<bool>();

            InitializeComponent();
            // Сброс настроек кнопок форматирования.
            ResetAllCheckStatus();
            // Установка темы, в зависимости от выбранных настроек.
            SetThemeMode(Properties.Settings.Default.ThemeMode);

            // Старт автосохранения.
            autoSaveTimer = new Timer();
            SetTimerInterval(Properties.Settings.Default.AutoSaveParam);
            autoSaveTimer.Tick += AutoSaveTimer_Tick;
            autoSaveTimer.Start();

            // Старт журналирования.
            LoggingTimer = new Timer();
            SetLoggingInterval(Properties.Settings.Default.LoggingTimer);
            LoggingTimer.Tick += LoggingTimer_Tick;
            LoggingTimer.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Создание нового документа по дефолту.
            listOfTextBoxes.Add(RichTextBox1);
            listOfFilePaths.Add(String.Empty);
            isSaved.Add(true);

            // Открытие последних файлов.
            if (!Properties.Settings.Default.IsChildForm)
                OpenClosedFiles();

        }

        /// <summary>
        /// Данный метод восстанавливает последние открытые пользователем документы.
        /// </summary>
        private void OpenClosedFiles()
        {
            try
            {
                string[] paths = File.ReadAllLines(fileLogName);
                foreach (var path in paths)
                {
                    if (!String.IsNullOrEmpty(path))
                    {
                        TabAdd_Click(this, new EventArgs());

                        if (Path.GetExtension(path).ToLower() == ".txt" || Path.GetExtension(path).ToLower() == ".cs")
                            listOfTextBoxes[TabControl1.SelectedIndex].LoadFile(path, RichTextBoxStreamType.PlainText);
                        else if (Path.GetExtension(path).ToLower() == ".rtf")
                            listOfTextBoxes[TabControl1.SelectedIndex].LoadFile(path, RichTextBoxStreamType.RichText);

                        TabControl1.SelectedTab.Text = Path.GetFileName(path);
                        listOfFilePaths[TabControl1.SelectedIndex] = path;
                        isSaved[TabControl1.SelectedIndex] = true;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Can't restore last open files!", "Error",
                       MessageBoxButtons.OK, MessageBoxIcon.Error,
                       MessageBoxDefaultButton.Button1,
                       MessageBoxOptions.DefaultDesktopOnly);
            }

        }

        /// <summary>
        /// Обработчик кнопки выхода(ctrl+f4).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e) => Application.Exit();

        /// <summary>
        /// Открытие файла.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog1.Filter = "Text File(*.txt) |*.txt|Rich Text Format(*.rtf) |*.rtf| C# |*.cs";
            OpenFileDialog1.Title = "Open";
            OpenFileDialog1.CheckFileExists = true;
            OpenFileDialog1.FileName = String.Empty;
            if (OpenFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;

            string filename = OpenFileDialog1.FileName;
            if (!listOfFilePaths.Contains(filename))
            {
                TabAdd_Click(sender, e);
                try
                {
                    if (Path.GetExtension(filename).ToLower() == ".txt" || Path.GetExtension(filename).ToLower() == ".cs")
                        listOfTextBoxes[TabControl1.SelectedIndex].LoadFile(filename, RichTextBoxStreamType.PlainText);
                    else if (Path.GetExtension(filename).ToLower() == ".rtf")
                        listOfTextBoxes[TabControl1.SelectedIndex].LoadFile(filename, RichTextBoxStreamType.RichText);

                    isSaved[TabControl1.SelectedIndex] = true;
                    TabControl1.SelectedTab.Text = Path.GetFileName(filename);
                    listOfFilePaths[TabControl1.SelectedIndex] = filename;
                }
                catch (Exception)
                {
                    MessageBox.Show("Can't open the file!", "Error",
                           MessageBoxButtons.OK, MessageBoxIcon.Error,
                           MessageBoxDefaultButton.Button1,
                           MessageBoxOptions.DefaultDesktopOnly);
                }
            }
            else
                MessageBox.Show("This file is already opened!", "Warning",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning,
                              MessageBoxDefaultButton.Button1,
                              MessageBoxOptions.DefaultDesktopOnly);


        }

        /// <summary>
        /// Вставка содержимого из буфера обмена.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string clipboardText = Clipboard.GetText();
            if (!String.IsNullOrEmpty(clipboardText))
            {
                if (TabControl1.SelectedIndex != -1)
                    listOfTextBoxes[TabControl1.SelectedIndex].Paste();

            }

        }

        /// <summary>
        /// Отмена действия.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TabControl1.SelectedIndex != -1)
            {
                if (listOfTextBoxes[TabControl1.SelectedIndex].CanUndo)
                {
                    listOfTextBoxes[TabControl1.SelectedIndex].Undo();
                }
            }
        }

        /// <summary>
        /// Повторение предыдущего действия, если это возможно.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RedoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TabControl1.SelectedIndex != -1)
                if (listOfTextBoxes[TabControl1.SelectedIndex].CanRedo == true)
                    listOfTextBoxes[TabControl1.SelectedIndex].Redo();

        }

        /// <summary>
        /// Сохранить как.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog1.Title = "Save As";
            SaveFileDialog1.Filter = "Text File(*.txt) |*.txt|Rich Text Format(*.rtf) |*.rtf | C# |*.cs";
            SaveFileDialog1.FileName = String.Empty;
            SaveFileDialog1.OverwritePrompt = true;

            if (SaveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;

            if (TabControl1.SelectedIndex != -1)
            {
                string filename = SaveFileDialog1.FileName;
                try
                {
                    if (Path.GetExtension(filename).ToLower() == ".txt" || Path.GetExtension(filename).ToLower() == ".cs")
                        listOfTextBoxes[TabControl1.SelectedIndex].SaveFile(filename, RichTextBoxStreamType.PlainText);
                    else if (Path.GetExtension(filename).ToLower() == ".rtf")
                        listOfTextBoxes[TabControl1.SelectedIndex].SaveFile(filename, RichTextBoxStreamType.RichText);

                    isSaved[TabControl1.SelectedIndex] = true;
                    listOfFilePaths[TabControl1.SelectedIndex] = filename;
                    TabControl1.SelectedTab.Text = Path.GetFileName(filename);
                }
                catch (Exception)
                {
                    MessageBox.Show($"Can't save the file! {filename} ", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.DefaultDesktopOnly);
                }

            }

        }

        /// <summary>
        /// Сохранить открытый файл.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (TabControl1.SelectedIndex != -1)
                {
                    string filePath = listOfFilePaths[TabControl1.SelectedIndex];
                    if (!String.IsNullOrEmpty(filePath))
                    {
                        if (Path.GetExtension(filePath).ToLower() == ".txt" || Path.GetExtension(filePath).ToLower() == ".cs")
                            listOfTextBoxes[TabControl1.SelectedIndex].SaveFile(filePath, RichTextBoxStreamType.PlainText);
                        else if (Path.GetExtension(filePath).ToLower() == ".rtf")
                            listOfTextBoxes[TabControl1.SelectedIndex].SaveFile(filePath, RichTextBoxStreamType.RichText);
                        isSaved[TabControl1.SelectedIndex] = true;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Can't save the file!", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        /// <summary>
        /// Выделить весь текст в текущем richTextBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TabControl1.SelectedIndex != -1)
                if (listOfTextBoxes[TabControl1.SelectedIndex].Text.Length > 0)
                    listOfTextBoxes[TabControl1.SelectedIndex].SelectAll();

        }

        /// <summary>
        /// Открытие описания приложения.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string readmeFilePath = "..\\..\\..\\README.rtf";
                TabAdd_Click(sender, e);
                listOfTextBoxes[TabControl1.SelectedIndex].LoadFile(readmeFilePath, RichTextBoxStreamType.RichText);
                TabControl1.SelectedTab.Text = Path.GetFileName(readmeFilePath);
                listOfFilePaths[TabControl1.SelectedIndex] = readmeFilePath;
                isSaved[TabControl1.SelectedIndex] = true;
            }
            catch (Exception)
            {
                MessageBox.Show("Can't open \"README.rtf\"!", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        /// <summary>
        /// Скопировать в буфер обмена.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TabControl1.SelectedIndex != -1)
                if (listOfTextBoxes[TabControl1.SelectedIndex].TextLength > 0)
                    listOfTextBoxes[TabControl1.SelectedIndex].Copy();

        }

        /// <summary>
        /// Вырезать выбранный текст.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TabControl1.SelectedIndex != -1)
                if (listOfTextBoxes[TabControl1.SelectedIndex].SelectedText != String.Empty)
                    listOfTextBoxes[TabControl1.SelectedIndex].Cut();

        }

        /// <summary>
        /// Вызов контекстного меню правой кнопкой мыши.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RichTextBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && TabControl1.SelectedIndex != -1)
                listOfTextBoxes[TabControl1.SelectedIndex].ContextMenuStrip = ContextMenuStrip1;
        }

        /// <summary>
        /// Удалить текущую вкладку.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemovetoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int removeIndex = TabControl1.SelectedIndex;
            // Предупреждение о закрытии файла.
            FileSaveWarning(removeIndex, removeIndex + 1);

            if (removeIndex != -1)
            {
                // Отписываем от всех событий.
                listOfTextBoxes[removeIndex].MouseUp -= RichTextBox1_MouseUp;
                listOfTextBoxes[removeIndex].SelectionChanged -= RichTextBox_SelectionChanged;
                listOfTextBoxes[removeIndex].TextChanged -= richTextBox_TextChanged;
                listOfTextBoxes[removeIndex].FontChanged -= richTextBox_TextChanged;
                // Удаляем.
                listOfTextBoxes.RemoveAt(removeIndex);
                listOfFilePaths.RemoveAt(removeIndex);
                isSaved.RemoveAt(removeIndex);
                TabControl1.TabPages.RemoveAt(removeIndex);
            }

        }

        /// <summary>
        /// Удаление всех вкладок.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveAlltoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // Предупреждение о закрытии всех файлов.
            FileSaveWarning(0, isSaved.Count);

            // Отписываем все RichTextBoxes от событий.
            for (int i = 0; i < listOfTextBoxes.Count; i++)
            {
                listOfTextBoxes[i].MouseUp -= RichTextBox1_MouseUp;
                listOfTextBoxes[i].SelectionChanged -= RichTextBox_SelectionChanged;
                listOfTextBoxes[i].TextChanged -= richTextBox_TextChanged;
                listOfTextBoxes[i].FontChanged -= richTextBox_TextChanged;
            }
            // Удаляем.
            listOfTextBoxes.RemoveRange(0, listOfTextBoxes.Count);
            listOfFilePaths.RemoveRange(0, listOfFilePaths.Count);
            isSaved.RemoveRange(0, isSaved.Count);
            TabControl1.TabPages.Clear();
        }

        /// <summary>
        /// Вызов предупреждения о закрытии, сохранение всех настроек и запись ссылок на последние открытые файлы.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Предупреждение о закрытии всех файлов.
            FileSaveWarning(0, isSaved.Count, e);
            try
            {
                File.Delete(fileLogName);
                File.WriteAllLines(fileLogName, listOfFilePaths);
                File.SetAttributes(fileLogName, FileAttributes.Hidden);
            }
            catch (Exception)
            {

                MessageBox.Show("The paths of the last opened files cannot be saved!", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.DefaultDesktopOnly);
            }
            Properties.Settings.Default.IsChildForm = false;
            Properties.Settings.Default.Save();

            DeleteAllLogs();
        }

        /// <summary>
        /// Предупреждение о закрытии приложения. при удалении вкладки.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="count"></param>
        private void FileSaveWarning(int start, int count)
        {
            for (int i = start; i < count; i++)
            {
                if (!isSaved[i])
                    try
                    {
                        if (String.IsNullOrEmpty(listOfFilePaths[i]))
                        {
                            var dialogResult = MessageBox.Show($"Do you want to save the Untitled-{i + 1} file?", "Closing...", MessageBoxButtons.YesNo, MessageBoxIcon.Information,
                                MessageBoxDefaultButton.Button1,
                                MessageBoxOptions.DefaultDesktopOnly);
                            if (dialogResult == DialogResult.Yes)
                            {
                                // Cохранить как.
                                SaveFileDialog1.Title = "Save As";
                                SaveFileDialog1.Filter = "Text File(*.txt) |*.txt|Rich Text Format(*.rtf) |*.rtf | C# |*.cs";
                                SaveFileDialog1.FileName = String.Empty;
                                SaveFileDialog1.OverwritePrompt = true;
                                if (SaveFileDialog1.ShowDialog() == DialogResult.Cancel)
                                    return;

                                if (Path.GetExtension(listOfFilePaths[i]).ToLower() == ".txt" || Path.GetExtension(listOfFilePaths[i]).ToLower() == ".cs")
                                    listOfTextBoxes[i].SaveFile(listOfFilePaths[i], RichTextBoxStreamType.PlainText);
                                else if (Path.GetExtension(listOfFilePaths[i]).ToLower() == ".rtf")
                                    listOfTextBoxes[i].SaveFile(listOfFilePaths[i], RichTextBoxStreamType.RichText);
                                isSaved[i] = true;
                            }

                        }
                        else
                        {
                            var dialogResult = MessageBox.Show($"Do you want to save the file: {listOfFilePaths[i]}?", "Closing...", MessageBoxButtons.YesNo, MessageBoxIcon.Information,
                                MessageBoxDefaultButton.Button1,
                                MessageBoxOptions.DefaultDesktopOnly);
                            if (dialogResult == DialogResult.Yes)
                            {
                                // Cохранить.
                                if (Path.GetExtension(listOfFilePaths[i]).ToLower() == ".txt" || Path.GetExtension(listOfFilePaths[i]).ToLower() == ".cs")
                                    listOfTextBoxes[i].SaveFile(listOfFilePaths[i], RichTextBoxStreamType.PlainText);
                                else if (Path.GetExtension(listOfFilePaths[i]).ToLower() == ".rtf")
                                    listOfTextBoxes[i].SaveFile(listOfFilePaths[i], RichTextBoxStreamType.RichText);
                                isSaved[i] = true;
                            }
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Can't save the file!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1,
                                MessageBoxOptions.DefaultDesktopOnly);
                    }
            }
        }

        /// <summary>
        /// Предупреждение о закрытии приложения, с возможностью отменить закрытие формы.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="count"></param>
        /// <param name="e"></param>
        private void FileSaveWarning(int start, int count, FormClosingEventArgs e)
        {
            for (int i = start; i < count; i++)
            {
                if (!isSaved[i])
                    try
                    {
                        if (String.IsNullOrEmpty(listOfFilePaths[i]))
                        {
                            var dialogResult = MessageBox.Show($"Do you want to save the \"Untitled-{i + 1}\" file?", "Closing...", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information,
                                MessageBoxDefaultButton.Button1,
                                MessageBoxOptions.DefaultDesktopOnly);
                            if (dialogResult == DialogResult.Cancel)
                                e.Cancel = true;
                            else if (dialogResult == DialogResult.Yes)
                            {
                                // Cохранить как.
                                SaveFileDialog1.Title = "Save As";
                                SaveFileDialog1.Filter = "Text File(*.txt) |*.txt|Rich Text Format(*.rtf) |*.rtf | C# |*.cs";
                                SaveFileDialog1.FileName = String.Empty;
                                SaveFileDialog1.OverwritePrompt = true;
                                if (SaveFileDialog1.ShowDialog() == DialogResult.Cancel)
                                    return;

                                if (Path.GetExtension(listOfFilePaths[i]).ToLower() == ".txt" || Path.GetExtension(listOfFilePaths[i]).ToLower() == ".cs")
                                    listOfTextBoxes[i].SaveFile(listOfFilePaths[i], RichTextBoxStreamType.PlainText);
                                else if (Path.GetExtension(listOfFilePaths[i]).ToLower() == ".rtf")
                                    listOfTextBoxes[i].SaveFile(listOfFilePaths[i], RichTextBoxStreamType.RichText);
                                isSaved[i] = true;
                            }

                        }
                        else
                        {
                            var dialogResult = MessageBox.Show($"Do you want to save the file: {listOfFilePaths[i]}?", "Closing...", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information,
                                MessageBoxDefaultButton.Button1,
                                MessageBoxOptions.DefaultDesktopOnly);
                            if (dialogResult == DialogResult.Cancel)
                                e.Cancel = true;
                            else if (dialogResult == DialogResult.Yes)
                            {
                                // Cохранить.
                                if (Path.GetExtension(listOfFilePaths[i]).ToLower() == ".txt" || Path.GetExtension(listOfFilePaths[i]).ToLower() == ".cs")
                                    listOfTextBoxes[i].SaveFile(listOfFilePaths[i], RichTextBoxStreamType.PlainText);
                                else if (Path.GetExtension(listOfFilePaths[i]).ToLower() == ".rtf")
                                    listOfTextBoxes[i].SaveFile(listOfFilePaths[i], RichTextBoxStreamType.RichText);
                                isSaved[i] = true;
                            }
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Can't save the file!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1,
                                MessageBoxOptions.DefaultDesktopOnly);
                    }
            }
        }

        /// <summary>
        /// Добавление новой вкладки.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabAdd_Click(object sender, EventArgs e)
        {
            int index = TabControl1.TabCount;
            TabPage newPage = new TabPage
            {
                Location = new Point(4, 24),
                Name = "newTabPage",
                Padding = new Padding(3),
                Size = new Size(715, 359),
                TabIndex = index,
                Text = $"Untitled-{index + 1}",
                UseVisualStyleBackColor = true
            };
            RichTextBox newRichTextBox = new RichTextBox
            {
                Dock = DockStyle.Fill,
                Location = new Point(0, 0),
                Size = new Size(715, 359),
                Text = String.Empty
            };
            newRichTextBox.MouseUp += new MouseEventHandler(this.RichTextBox1_MouseUp);
            newRichTextBox.SelectionChanged += new EventHandler(this.RichTextBox_SelectionChanged);
            newRichTextBox.TextChanged += new EventHandler(this.richTextBox_TextChanged);
            newRichTextBox.FontChanged += new EventHandler(this.richTextBox_TextChanged);

            newRichTextBox.BorderStyle = BorderStyle.None;

            if (darkThemeOn)
            {
                newRichTextBox.BackColor = Color.FromArgb(30, 31, 32);
                newRichTextBox.ForeColor = Color.FromArgb(221, 221, 221);
            }

            listOfFilePaths.Add(String.Empty);
            listOfTextBoxes.Add(newRichTextBox);
            isSaved.Add(true);
            newPage.Controls.Add(newRichTextBox);
            TabControl1.TabPages.Add(newPage);
            TabControl1.SelectTab(index);
        }

        /// <summary>
        /// Темная тема(вкл/ выкл).
        /// </summary>
        private bool darkThemeOn;

        /// <summary>
        /// Установить выбранную тему.
        /// </summary>
        /// <param name="themeMode"></param>
        private void SetThemeMode(bool themeMode)
        {
            darkThemeOn = themeMode;
            if (themeMode)
                DarkTheme_Click(this, new EventArgs());
            else
                LightTheme_Click(this, new EventArgs());
        }

        /// <summary>
        /// Установка темной темы.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DarkTheme_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.ThemeMode = true;
            Properties.Settings.Default.Save();
            darkThemeOn = true;
            Color backColor = Color.FromArgb(30, 31, 32);
            Color fontColor = Color.FromArgb(221, 221, 221);
            ResetControlsBackColor(this, backColor, fontColor);
            ResetAllMenuItemsBackColor(backColor, fontColor);

        }

        /// <summary>
        /// Установка цвета фона и текста.
        /// </summary>
        /// <param name="backColor">Цвет фона</param>
        /// <param name="fontColor">Цвет шрифта</param>
        private void ResetAllMenuItemsBackColor(Color backColor, Color fontColor)
        {
            FileToolStripMenuItem.BackColor = EditToolStripMenuItem.BackColor = AboutToolStripMenuItem.BackColor = backColor;
            PreferencesToolStripMenuItem1.BackColor = FormatToolStripMenuItem1.BackColor = FonttoolStripMenuItem1.BackColor = FontButton.BackColor = backColor;
            NewToolStripMenuItem1.BackColor = NewToolStripButton.BackColor = OpenToolStripMenuItem.BackColor = OpenToolStripButton.BackColor = backColor;
            SaveToolStripMenuItem.BackColor = SaveToolStripButton.BackColor = SaveAsToolStripMenuItem.BackColor = SaveAlltoolStripMenuItem1.BackColor = backColor;
            ExitToolStripMenuItem.BackColor = CutToolStripButton.BackColor = CutToolStripMenuItem.BackColor = ContextCut.BackColor = backColor;
            CopyToolStripMenuItem.BackColor = CopyToolStripButton.BackColor = ContextCopy.BackColor = backColor;
            PasteToolStripMenuItem.BackColor = PasteToolStripButton.BackColor = ContextPaste.BackColor = backColor;
            UndoToolStripMenuItem.BackColor = RedoToolStripMenuItem.BackColor = backColor;
            ItalicStripMenuItem1.BackColor = Italic.BackColor = BoldStripMenuItem1.BackColor = Bold.BackColor = backColor;
            UnderlineStripMenuItem1.BackColor = Underline.BackColor = NewFormtoolStripMenuItem1.BackColor = backColor;
            StrikeoutStripMenuItem1.BackColor = Strikeout.BackColor = backColor;
            SelectAllToolStripMenuItem.BackColor = ContextSellectAll.BackColor = backColor;
            AutoSavetoolStripMenuItem1.BackColor = AutoSave15min.BackColor = AutoSave1min.BackColor = AutoSave30sec.BackColor = AutoSave5min.BackColor = backColor;
            ThemeMode.BackColor = DarkTheme.BackColor = LightTheme.BackColor = backColor;
            TabPage1.BackColor = TabtoolStripSplitButton.BackColor = AddtoolStripMenuItem1.BackColor = RemovetoolStripMenuItem1.BackColor = RemoveAlltoolStripMenuItem1.BackColor = backColor;
            ContextSeparator.BackColor = SToolStripSeparator.BackColor = SToolStripSeparator1.BackColor = SToolStripSeparator10.BackColor = SToolStripSeparator3.BackColor = backColor;
            ToolStripSeparator4.BackColor = SToolStripSeparator6.BackColor = SToolStripSeparator7.BackColor = SToolStripSeparator8.BackColor = backColor;
            Logging.BackColor = TimerLogging.BackColor = Logging1min.BackColor = Logging5min.BackColor = Logging10min.BackColor = PreviousVersion.BackColor = backColor;

            FileToolStripMenuItem.ForeColor = EditToolStripMenuItem.ForeColor = FontButton.ForeColor = FonttoolStripMenuItem1.ForeColor = fontColor;
            PreferencesToolStripMenuItem1.ForeColor = FormatToolStripMenuItem1.ForeColor = AboutToolStripMenuItem.ForeColor = fontColor;
            NewToolStripMenuItem1.ForeColor = NewToolStripButton.ForeColor = OpenToolStripMenuItem.ForeColor = OpenToolStripButton.ForeColor = fontColor;
            SaveToolStripMenuItem.ForeColor = SaveToolStripButton.ForeColor = SaveAsToolStripMenuItem.ForeColor = SaveAlltoolStripMenuItem1.ForeColor = fontColor;
            ExitToolStripMenuItem.ForeColor = CutToolStripButton.ForeColor = CutToolStripMenuItem.ForeColor = ContextCut.ForeColor = fontColor;
            CopyToolStripMenuItem.ForeColor = CopyToolStripButton.ForeColor = ContextCopy.ForeColor = fontColor;
            PasteToolStripMenuItem.ForeColor = PasteToolStripButton.ForeColor = ContextPaste.ForeColor = fontColor;
            UndoToolStripMenuItem.ForeColor = RedoToolStripMenuItem.ForeColor = fontColor;
            ItalicStripMenuItem1.ForeColor = Italic.ForeColor = BoldStripMenuItem1.ForeColor = Bold.ForeColor = fontColor;
            UnderlineStripMenuItem1.ForeColor = Underline.ForeColor = NewFormtoolStripMenuItem1.ForeColor = fontColor;
            StrikeoutStripMenuItem1.ForeColor = Strikeout.ForeColor = fontColor;
            SelectAllToolStripMenuItem.ForeColor = ContextSellectAll.ForeColor = fontColor;
            AutoSavetoolStripMenuItem1.ForeColor = AutoSave15min.ForeColor = AutoSave1min.ForeColor = AutoSave30sec.ForeColor = AutoSave5min.ForeColor = fontColor;
            ThemeMode.ForeColor = DarkTheme.ForeColor = LightTheme.ForeColor = fontColor;
            TabPage1.ForeColor = TabtoolStripSplitButton.ForeColor = AddtoolStripMenuItem1.ForeColor = RemovetoolStripMenuItem1.ForeColor = RemoveAlltoolStripMenuItem1.ForeColor = fontColor;
            ContextSeparator.ForeColor = SToolStripSeparator.ForeColor = SToolStripSeparator1.ForeColor = SToolStripSeparator10.ForeColor = SToolStripSeparator3.ForeColor = fontColor;
            ToolStripSeparator4.ForeColor = SToolStripSeparator6.ForeColor = SToolStripSeparator7.ForeColor = SToolStripSeparator8.ForeColor = fontColor;
            Logging.ForeColor = TimerLogging.ForeColor = Logging1min.ForeColor = Logging5min.ForeColor = Logging10min.ForeColor = PreviousVersion.ForeColor = fontColor;
        }

        /// <summary>
        /// Сброс всех настроек цвета вона и шрифта через control.
        /// </summary>
        /// <param name="control"></param>
        /// <param name="back"></param>
        /// <param name="font"></param>
        private void ResetControlsBackColor(Control control, Color back, Color font)
        {
            control.BackColor = back;
            control.ForeColor = font;
            if (control.HasChildren)
            {
                foreach (Control childControl in control.Controls)
                {
                    ResetControlsBackColor(childControl, back, font);
                }
            }
        }

        /// <summary>
        /// Установить светлую тему.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LightTheme_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.ThemeMode = false;
            Properties.Settings.Default.Save();
            darkThemeOn = false;

            Color backColor = SystemColors.Window;
            Color fontColor = SystemColors.WindowText;
            ResetControlsBackColor(this, backColor, fontColor);
            ResetAllMenuItemsBackColor(backColor, fontColor);

        }

        /// <summary>
        /// Обработчик изменения текста в выбранном richTextBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void richTextBox_TextChanged(object sender, EventArgs e)
        {
            if (TabControl1.SelectedIndex != -1)
                isSaved[TabControl1.SelectedIndex] = false;
        }

        /// <summary>
        /// Смена шрифта выбранного текста.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FontText_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog = new FontDialog();

            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                if (TabControl1.SelectedIndex != -1)
                    listOfTextBoxes[TabControl1.SelectedIndex].SelectionFont = fontDialog.Font;
            }
        }

        /// <summary>
        /// Создание нового документа в новом окне.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewFormtoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.IsChildForm = true;
            Properties.Settings.Default.Save();
            var childForm = new Form1();
            childForm.Show();
        }

    }
}



