﻿using System;
using System.Windows.Forms;


namespace FractalSnow
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка:" + ex.Message);
            }
            
        }
    }
}
