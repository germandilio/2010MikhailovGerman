using System;
using System.Linq;
using System.IO;
using System.Text;

/*  
    Реализация консольного интерфейса для Windows.
*/

namespace Peergrade
{
    partial class FileManager
    {

        /// <summary>
        /// Метод создания текстового файла.
        /// </summary>
        /// <param name="pathToFile">Путь директории, в которой будет расположен файл.</param>
        /// <param name="encoding">Кодировка.</param>
        private static void CreateFileInSelectedEncodingWindows(string pathToFile, Encoding encoding)
        {
            Console.Clear();
            string pathToChosenFile = pathToFile + Path.VolumeSeparatorChar + "NewTextFile.txt";

            Console.WriteLine("Введите текст, который хотите записать в файл, затем нажмите Enter ");
            Console.CursorVisible = true;
            // Обработка исключений.
            try
            {
                string line = Console.ReadLine();
                File.WriteAllText(pathToChosenFile, line, encoding);
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine("Ошибка создания файла: " + ex.Message);
                // Красивый выход в начало через ESC.
                return;
            }
            Console.WriteLine();
            Console.WriteLine("Готово!!! Ваш файл расположен в : ");
            Console.WriteLine(pathToChosenFile);

        }

        /// <summary>
        /// Метод печатает в консоль правила использования файлового менеджера.
        /// </summary>
        private static void HelpMenuWindows()
        {
            // Вызов подсказок в самом начале, как пользоваться программой.
            Console.WriteLine();
            Console.WriteLine("     Добро пожаловать в Файловый менеджер !!!                                                         ");
            Console.WriteLine(" -----------------------------------------------------------------------------------------------------");
            Console.WriteLine(" Файловый менеджер распознает следующие команды:                                                      ");
            Console.WriteLine(" -----------------------------------------------------------------------------------------------------");
            Console.WriteLine(@" cd - означает переместиться в директорию по указанному пути (change directory).                     ");
            Console.WriteLine(@" vf - посмотреть файлы и папки в данной директори (view files).                                      ");
            Console.WriteLine(@" mv - Переместить файл (move directory).                                                             ");
            Console.WriteLine(@" rmdir - Удалить пустую папку (remove directory).                                                    ");
            Console.WriteLine(@" rm - удалить файл.                                                                                  ");
            Console.WriteLine(@" rm -r  - удаление папки и всех вложенных файлов.                                                    ");
            Console.WriteLine(@" info - получить информацию о файле.                                                                 ");
            Console.WriteLine(@" print - вывести в консоль текстовый файл.                                                           ");
            Console.WriteLine(@" concat - сконкотенировать файлы в один, и вывести в консоль.                                        ");
            Console.WriteLine(@" exit - выйти из терминала.                                                                          ");
            Console.WriteLine(" -----------------------------------------------------------------------------------------------------");
            Console.WriteLine("  ВНИМАНИЕ!!! Следует сначала вводить желаемую команду, нажать Enter, а затем вводить путь.           ");
            Console.WriteLine(" -----------------------------------------------------------------------------------------------------");
            Console.WriteLine(" Нажмите Enter, чтобы продолжить ...                                                                  ");
            Console.WriteLine(" Или любую другую клавишу, чтобы выйти.                                                               ");
        }

       
        
        /// <summary>
        /// Метод запускает файловый менеджер для Windows.
        /// </summary>
        private static void RunForWindows()
        {
            // Печать меню.
            HelpMenuWindows();
            // Для продолжения нужно ввести Enter.
            if (Console.ReadKey(true).Key == ConsoleKey.Enter)
            {
                do
                {
                    Console.Clear();
                    try
                    {
                        // Запускаемм терминал.
                        Commands();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(" Ошибка: " + ex.Message);
                        Environment.Exit(0);
                    }
                    // После совершения операции запрашивать продолжение.

                    Console.WriteLine("------------------------------------------------------------------");
                    Console.WriteLine("Для выхода нажмите Esc, или любую другую клавишу чтобы продолжить ");
                } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// Тело файлового менеджера (интерфейс терминала).
        /// </summary>
        private static void Commands()
        { 

            string command = String.Empty;
            do
            {
                    // Получаем директорию, в которой сейчас находимся.
                    string dir = Directory.GetCurrentDirectory();
                    // Печатаем текущий путь.
                    Console.WriteLine(dir + ": ");
                    // Считываем команду пользователя.
                    command = Console.ReadLine();
                    command.TrimEnd(' ');
                // Свитчер по командам.
                switch (command)
                {
                    case "cd":
                        {
                            Console.WriteLine(" Введите путь к файлу, который хотите переместиться: ");
                            try
                            {
                                // Переместиться в директорию.
                                string path = Console.ReadLine();
                                Directory.SetCurrentDirectory(path);

                            }
                            catch (Exception ex)
                            {
                                Console.Clear();
                                Console.WriteLine("Ошибка: " + ex.Message);
                            }
                            break;
                        }
                    case "vf":
                        {
                            try
                            {
                                // Посмотреть все файлы в директории.
                                foreach (string file in Directory.EnumerateFileSystemEntries(dir))
                                {
                                    Console.WriteLine(file.Split(Path.VolumeSeparatorChar)[file.Split(Path.VolumeSeparatorChar).Length - 1]);
                                }
                                Console.WriteLine();

                            }
                            catch (Exception ex)
                            {
                                Console.Clear();
                                Console.WriteLine(" Ошибка: " + ex.Message);
                            }
                            break;
                        }
                    case "mv":
                        {
                            try
                            {
                                // Переместить файл.
                                Console.WriteLine(" Введите путь к файлу, который хотите переместить: ");
                                string path1 = Console.ReadLine();
                                string path2 = Console.ReadLine();

                                Directory.Move(path1, path2);
                            }
                            catch (Exception ex)
                            {
                                Console.Clear();
                                Console.WriteLine(" Ошибка: " + ex.Message);
                            }
                            break;
                        }
                    case "rmdir":
                        {
                            try
                            {
                                Console.WriteLine(" Введите путь к файлу, который хотите  удалить: ");
                                string path = Console.ReadLine();

                                if (Directory.EnumerateFileSystemEntries(path).Count() > 0)
                                {
                                    Console.WriteLine(" Папка не пустая, для удаления данной папки воспользуйтесь командой  \"rm -r\" ");
                                }
                                else
                                {
                                    // Удалить директорию.
                                    Directory.Delete(path);
                                }

                            }
                            catch (Exception ex)
                            {
                                Console.Clear();
                                Console.WriteLine("Ошибка: " + ex.Message);
                            }
                            break;
                        }
                    case "rm":
                        {
                            try
                            {
                                Console.WriteLine(" Введите путь к файлу, который хотите  удалить: ");
                                string path = Console.ReadLine();
                                // Удалить файл.
                                Directory.Delete(path);
                            }
                            catch (Exception ex)
                            {
                                Console.Clear();
                                Console.WriteLine("Ошибка: " + ex.Message);
                            }

                            break;
                        }
                    case "rm -r":
                        {
                            try
                            {
                                Console.WriteLine(" Введите путь к файлу, который хотите  удалить: ");
                                string path = Console.ReadLine();
                                // Удалить папку и все файлы в ней.
                                Directory.Delete(path, true);

                            }
                            catch (Exception ex)
                            {
                                Console.Clear();
                                Console.WriteLine("Ошибка: " + ex.Message);
                            }
                            break;
                        }

                    case "info":
                        {
                            try
                            {
                                Console.WriteLine(" Введите путь к файлу, информацию о котором хотите узнать: ");
                                string path = Console.ReadLine();
                                // Получить информацию о файле.
                                GetInformationAboutFile(path);
                            }
                            catch (Exception ex)
                            {
                                Console.Clear();
                                Console.WriteLine("Ошибка: " + ex.Message);
                            }
                            break;
                        }

                    case "exit":
                        {
                            command = "exit";
                            break;
                        }
                    // Печать в консоль.
                    case "print":
                        {
                            try
                            {
                                Console.WriteLine(" Введите путь к файлу, который хотите вывести в консоль: ");
                                string path = Console.ReadLine();
                                Encoding enc = ChooseEncoding();
                                // Вызов печати.
                                ConsoleOutputFileInSelectedEncoding(path, enc);
                            }
                            catch (Exception ex)
                            {
                                Console.Clear();
                                Console.WriteLine("Ошибка: " + ex.Message);
                            }
                            Console.WriteLine();
                            break;

                        }
                    case "create":
                        {
                            try
                            {
                                Console.WriteLine(" Введите путь к создаваемому файлу: ");
                                string path = Console.ReadLine();

                                Encoding enc = ChooseEncoding();
                                // Вызов создания файла.
                                CreateFileInSelectedEncodingWindows(path, enc);

                            }
                            catch (Exception ex)
                            {
                                Console.Clear();
                                Console.WriteLine("Ошибка: " + ex.Message);
                            }
                            Console.WriteLine();
                            break;
                        }
                    case "concat":
                        {
                            try
                            {
                                Console.WriteLine(" Введите количество конкатенируемых файлов! ");
                                string path = Console.ReadLine();
                                int numbersOfFiles;
                                // Парсим число.
                                if (!int.TryParse(path, out numbersOfFiles) || numbersOfFiles < 1)
                                {
                                    Console.WriteLine(" Введено некорректное число файлов! ");
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine(" Введите путь к файлу: ");
                                    // Считываем путь.
                                    string pathToFile = Console.ReadLine();
                                    // Прочитали первый файл.
                                    string[] output = File.ReadAllLines(pathToFile);
                                    // Создаем файл по пути.

                                    string pathToChosenFile = Directory.GetParent(pathToFile).ToString() + Path.VolumeSeparatorChar + "NewConcatTextFile.txt";
                                    File.AppendAllLines(pathToChosenFile, output, Encoding.UTF8);
                                    // Обрабатываем остальные файлы.
                                    for (int i = 2; i <= numbersOfFiles; i++)
                                    {
                                        Console.WriteLine(" Введите путь к файлу: ");
                                        string pathtoFile = Console.ReadLine();
                                        // Записываем файл.
                                        output = File.ReadAllLines(pathToFile);
                                        File.AppendAllLines(pathToChosenFile, output, Encoding.UTF8);
                                    }
                                    Console.WriteLine("----------------------------");
                                    Console.WriteLine("Готово!!! Ваш файл лежит в: ");
                                    Console.WriteLine(pathToChosenFile);

                                    // Вызываем печать файла в консоль.
                                    try
                                    {
                                        string[] textToPrint = File.ReadAllLines(pathToFile, Encoding.UTF8);
                                        // Вывод на экран.
                                        Console.WriteLine("----------------");
                                        Console.WriteLine("Текст из файла: ");
                                        for (int i = 0; i < output.Length; i++)
                                        {
                                            Console.WriteLine(output[i]);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Ошибка конкотенации файлов: " + ex.Message);
                                        // Красивый выход в начало через ESC.
                                        return;
                                    }


                                }

                            }
                            catch (Exception ex)
                            {
                                Console.Clear();
                                Console.WriteLine("Ошибка: " + ex.Message);
                            }
                            Console.WriteLine();
                            break;
                        }

                    default:
                        {
                            Console.WriteLine(" Неизвестная команда! ");
                            Console.WriteLine();
                            break;
                        }

                }

            } while (command != "exit");

        }
    }
}

