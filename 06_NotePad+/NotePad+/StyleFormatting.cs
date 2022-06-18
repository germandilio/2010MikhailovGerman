using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace NotePad_
{
    public partial class Form1
    {
        internal bool boldOn;
        internal bool italicOn;
        internal bool underlineOn;
        internal bool strikeoutOn;

        /// <summary>
        /// Сброс состояния всех кнопок форматирования.
        /// </summary>
        private void ResetAllCheckStatus()
        {
            BoldtoolStripButton1.Checked = BoldStripMenuItem1.Checked = Bold.Checked = false;
            ItalictoolStripButton1.Checked = ItalicStripMenuItem1.Checked = Italic.Checked = false;
            UnderlinetoolStripButton1.Checked = Underline.Checked = UnderlineStripMenuItem1.Checked = false;
            StrikeouttoolStripButton1.Checked = Strikeout.Checked = StrikeoutStripMenuItem1.Checked = false;
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Bold".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoldStripMenuItem_Click(object sender, EventArgs e)
        {
            boldOn = !boldOn;
            BoldtoolStripButton1.Checked = Bold.Checked = BoldStripMenuItem1.Checked = boldOn;
            ApplyStyle(CFM_BOLD, boldOn);
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Strikeout".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StrikeoutStripMenuItem1_Click(object sender, EventArgs e)
        {
            strikeoutOn = !strikeoutOn;
            StrikeouttoolStripButton1.Checked = Strikeout.Checked = StrikeoutStripMenuItem1.Checked = strikeoutOn;
            ApplyStyle(CFM_STRIKEOUT, strikeoutOn);
        }

        /// <summary>
        /// Обработчик нажатия кнопки "UnderLine".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UnderlineMenuItem1_Click(object sender, EventArgs e)
        {
            underlineOn = !underlineOn;
            UnderlinetoolStripButton1.Checked = Underline.Checked = UnderlineStripMenuItem1.Checked = underlineOn;
            ApplyStyle(CFM_UNDERLINE, underlineOn);
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Italic".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItalicStripMenuItem_Click(object sender, EventArgs e)
        {
            italicOn = !italicOn;
            ItalictoolStripButton1.Checked = Italic.Checked = ItalicStripMenuItem1.Checked = italicOn;
            ApplyStyle(CFM_ITALIC, italicOn);
        }


        /// <summary>
        /// Приведенный ниже код скопирован с сайта stackoverflow, и доработан(немного переписан) под это приложение.
        /// Выбранный текст изменен.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RichTextBox_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                // Чтобы синхронизировать выделение и кнопки.
                CHARFORMAT cf = new CHARFORMAT();
                cf.cbSize = Marshal.SizeOf(cf);
                SendMessage(new HandleRef(this, listOfTextBoxes[TabControl1.SelectedIndex].Handle), EM_GETCHARFORMAT, SCF_SELECTION, ref cf);

                boldOn = (cf.dwEffects & CFM_BOLD) == CFM_BOLD;
                italicOn = (cf.dwEffects & CFM_ITALIC) == CFM_ITALIC;
                underlineOn = (cf.dwEffects & CFM_UNDERLINE) == CFM_UNDERLINE;
                strikeoutOn = (cf.dwEffects & CFM_STRIKEOUT) == CFM_STRIKEOUT;
                // Установка кнопок в положение, соответствующее выбранному тексту.
                BoldtoolStripButton1.Checked = BoldStripMenuItem1.Checked = Bold.Checked = boldOn;
                ItalictoolStripButton1.Checked = ItalicStripMenuItem1.Checked = Italic.Checked = italicOn;
                UnderlinetoolStripButton1.Checked = Underline.Checked = UnderlineStripMenuItem1.Checked = underlineOn;
                StrikeouttoolStripButton1.Checked = Strikeout.Checked = StrikeoutStripMenuItem1.Checked = strikeoutOn;
            }
            catch (Exception)
            {
                MessageBox.Show("Style buttons sync error.", "Warning",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning,
                              MessageBoxDefaultButton.Button1,
                              MessageBoxOptions.DefaultDesktopOnly);
            }

        }

        [DllImport("user32", CharSet = CharSet.Auto)]
        private static extern int SendMessage(HandleRef hWnd,
                        int msg, int wParam, ref CHARFORMAT lp);

        private const int EM_SETCHARFORMAT = 1092;
        private const int EM_GETCHARFORMAT = 0x0400 + 58;

        private const int CFM_BOLD = 1;
        private const int CFM_ITALIC = 2;
        private const int CFM_STRIKEOUT = 8;
        private const int CFM_UNDERLINE = 4;

        private const int SCF_SELECTION = 1;

        [StructLayout(LayoutKind.Sequential)]
        private struct CHARFORMAT
        {
            public int cbSize;
            public uint dwMask;
            public uint dwEffects;
            public int yHeight;
            public int yOffset;
            public int crTextColor;
            public byte bCharSet;
            public byte bPitchAndFamily;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public char[] szFaceName;
            public short wWeight;
            public short sSpacing;
            public int crBackColor;
            public int LCID;
            public uint dwReserved;
            public short sStyle;
            public short wKerning;
            public byte bUnderlineType;
            public byte bAnimation;
            public byte bRevAuthor;
        }

        /// <summary>
        /// Посимвольная установка формата текста.
        /// </summary>
        /// <param name="fmt"></param>
        private void SetCharFormatMessage(ref CHARFORMAT fmt)
        {
            if (TabControl1.SelectedIndex != -1)
                SendMessage(new HandleRef(this, listOfTextBoxes[TabControl1.SelectedIndex].Handle), EM_SETCHARFORMAT, SCF_SELECTION, ref fmt);
        }

        /// <summary>
        /// Применение стиля к выбранному тексту.
        /// </summary>
        /// <param name="style"></param>
        /// <param name="on"></param>
        private void ApplyStyle(uint style, bool on)
        {
            CHARFORMAT fmt = new CHARFORMAT();
            fmt.cbSize = Marshal.SizeOf(fmt);
            fmt.dwMask = style;

            if (on)
                fmt.dwEffects = style;
            SetCharFormatMessage(ref fmt);
        }
    }
}
