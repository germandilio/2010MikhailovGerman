using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections;

/*  
    Файл содержит метод Main, в котором определется ОС и в зависимости от нее запускает графический интерфейс для
    Mac OS и Linux, или (коммандный) интерфейс терминала для Windows.

    line 20 - 40 метод  Main().
    line 43 - 195 все кроссплатформенные методы.

*/

namespace Peergrade
{
    partial class FileManager
    {
        // Шапка, которая будет подсказывать пользователю что сейчас надо сделать.
        private static string headToPrint;

        /// <summary>
        /// Метод является переклюяателем между двумя разными реализациями приложения(для Windows, Linux и Mac).
        /// </summary>
        /// <param name="args"></param>
        private static void Main(string[] args)
        {
            // Получение информации об операционной системе.
            string systemIO = Environment.OSVersion.ToString();

            if (systemIO.ToLower().Contains("unix") || systemIO.ToLower().Contains("linux"))
            {
                // ОС - Mac OS или Linux.
                RunForUnix();
            }
            else 
            {
                // ОС - Microsoft Windows.
                RunForWindows();
            }
        }


        /// <summary>
        /// Метод для получения информации о файле по указанному пути в директории.
        /// </summary>
        /// <param name="dir">Путь до директории</param>
        static void GetInformationAboutFile(string pathtofile)
        {
            try
            {
                Console.Clear();
                // Проверка на существование файла.
                if (File.Exists(pathtofile))
                {
                    Console.WriteLine($"Файл {Path.GetFileName(pathtofile)} :");
                    // Время создания.
                    Console.WriteLine($"Время создания: {File.GetCreationTime(pathtofile)}");
                    // Время последнего обращения.
                    Console.WriteLine($"Время последнего обращения: {File.GetLastAccessTime(pathtofile)}");
                    // Время последнего изменения.
                    Console.WriteLine($"Время последнего изменения: {File.GetLastWriteTime(pathtofile)}");
                }

            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine("Пожалуйста, выберите только поддерживаемыq файл ");
                Console.WriteLine("Ошибка: " + ex.Message);
                // Красивый выход в начало через ESC.
                return;
            }

        }


        /// <summary>
        /// Метод вывода текстового файла в консоль, в выбранной кодировке.
        /// </summary>
        /// <param name="pathToFile">Путь к файлу, содержимое котрого надо вывести.</param>
        /// <param name="encoding">Кодировка.</param>
        private static void ConsoleOutputFileInSelectedEncoding(string pathToFile, Encoding encoding)
        {
            // Обработка исклюяений.
            try
            {
                string[] output = File.ReadAllLines(pathToFile, encoding);
                // Вывод на экран.
                Console.Clear();
                Console.WriteLine("Текст из файла: ");
                for (int i = 0; i < output.Length; i++)
                {
                    Console.WriteLine(output[i]);
                }
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine("Ошибка: " + ex.Message);
                // Красивый выход в начало через ESC.
                return;
            }

        }


        /// <summary>
        /// Метод возвращает выор пользователя(пункт подменю или кодировка).
        /// </summary>
        /// <param name="Items">Пункты подменю.</param>
        /// <returns>Выбор пользователя.</returns>
        private static int GetUserItem(string[] Items)
        {
            // Метод подсвечивает выбор пользователеля возвращает номер выбранного пункта подменю (При нажатии Tab).

            Console.CursorVisible = false;
            // Чтобы не было мигающего курсора.
            int choice = 0;
            while (true)
            {
                PrintDirectories(Items, choice);

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.UpArrow:
                        choice--;
                        break;
                    case ConsoleKey.DownArrow:
                        choice++;
                        break;
                    case ConsoleKey.Enter:
                        return choice;
                }
                // Зацикливаем выбор.
                choice = (choice + Items.Length) % Items.Length;
            }
        }


        /// <summary>
        /// Метод является переключателем кодировок, в зависимости от выбора пользователя.
        /// </summary>
        /// <returns>Выбранную кодировку&</returns>
        private static Encoding ChooseEncoding()
        {
            headToPrint = "ВЫБЕРИТЕ КОДИРОВКУ: (для подтверждения выбора нажмите Enter)";

            string[] Encodings = { "UTF-8", "UTF-32", "Unicode", "ASCII", };
            int item = GetUserItem(Encodings);
            // Свитчер выбора кодировок.
            switch (item)
            {
                case 0:
                    return Encoding.UTF8;
                case 1:
                    return Encoding.UTF32;
                case 2:
                    return Encoding.Unicode;
                case 3:
                    return Encoding.ASCII;
                default:
                    return Encoding.UTF8;
            }
        }


        /// <summary>
        /// Метод печатает передоваемое меню, подсвечивая красным цветом текущий выбор пользователя.
        /// </summary>
        /// <param name="Directories">Массив отображаемых названий.</param>
        /// <param name="choice">Выобор пользователя.</param>
        private static void PrintDirectories(string[] Directories, int choice)
        {
            Console.Clear();
            // Печать шапки, которая подсказывает что делать.
            Console.WriteLine(headToPrint);
            Console.WriteLine();
            // Текущуюю директорию.

            for (int i = 0; i < Directories.Length; i++)
            {


                if (i == choice)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(Directories[i]);
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine(Directories[i]);
                }
            }
        }



    }
}