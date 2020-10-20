using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections;

/*  
    Реализация графического интерфейса для Mac OS и Linux.

    line 19 - 285 содержит все методы действий с файлами и все методы, которые печатают информацию на экран.
    line 288 - 580 содержит все методы расчёта и получения путей, которые не видны пользователю (некий backend).
    line 582 - 705 содержит все методы переключатели (switch), которые обрабатывают все действия пользователя
    (все нажатые стрелочки вправо, влево, вниз, вверх, Tab и Enter).
*/

namespace Peergrade
{
    partial class FileManager
    {
        // Поле для автоматического подтверждения файла, при нажатии Tab.
        private static int indexInArrayToChosenFile;
        // Поле для хранения пути, полученного из GetpathToDirectory().
        private static string pathToDir;
        // Поле пути возврата в предыдущую директорию.
        private static string pathToParentDirectory;
        // Поле показывающее пустоту директории.
        private static bool dirIsEmpty = false;


        /// <summary>
        /// Метод вызывает повторение всего решения (вызов правил файлового менеджера).
        /// Вызов первоначального перемещения по папкам.
        /// </summary>
        private static void RunForUnix()
        {
            // Метод, который выводит первичные правила игры.
            HelpMenuMacOs();
            // Нажмите  Enter, чтобы начать.
            if (Console.ReadKey(true).Key == ConsoleKey.Enter)
            {
                do
                {
                    headToPrint = "ВЫБЕРИТЕ ФАЙЛ: (вызов подменю с помощью Enter)";
                    Console.Clear();
                    int mode = 1;
                    try
                    {
                        // Вызов первоначального метода перемещения по папкам.
                        GetPathToDirectory(mode);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Ошибка: " + ex.Message);
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
        /// Метод печатает в консоль правила использования файлового менеджера.
        /// </summary>
        private static void HelpMenuMacOs()
        {
            // Вызов подсказок в самом начале, как пользоваться программой.
            Console.WriteLine();
            Console.WriteLine("     Добро пожаловать в Файловый менеджер !!!                                                         ");
            Console.WriteLine(" -----------------------------------------------------------------------------------------------------");
            Console.WriteLine(" ПОЖАЛУЙСТА!!! Для коректной работы программы, откройте терминал в отдельной вкладке на весь экран    ");
            Console.WriteLine();
            Console.WriteLine(" Навигация по папкам реализована с помощью следующих физических клавиш:                               ");
            Console.WriteLine(" -----------------------------------------------------------------------------------------------------");
            Console.WriteLine(" Стрелочки вниз и вверх - позволяют перемещаться по папкам и файлам в текущей директории.             ");
            Console.WriteLine(" Стрелочки влево и вправо - позволяют переходить внутрь папки или возвращаться назад.                 ");
            Console.WriteLine(" При наведении на файл и нажатии Enter вспылвает подменю, которое позволяет выбрать действия с файлом ");
            Console.WriteLine(" Навигация в подменю аналогичная: перемещение стрелочками, для выбора нажмите - Enter                 ");
            Console.WriteLine(" -----------------------------------------------------------------------------------------------------");
            Console.WriteLine(" Обратите внимание, что перейти внуть файла нельзя (только папки), а также если директория            ");
            Console.WriteLine(" пустая, то перейти в нее нельзя. Потому что там нет файлов и вызвать подменю невозможно              ");
            Console.WriteLine(" -----------------------------------------------------------------------------------------------------");
            Console.WriteLine(" Возможные действия с файлами:                                                                        ");
            Console.WriteLine(" -----------------------------------------------------------------------------------------------------");
            Console.WriteLine(" - Вывод содержимого текстового файла в консоль                                                       ");
            Console.WriteLine(" - Копировать                                                                                         ");
            Console.WriteLine(" - Переместить                                                                                        ");
            Console.WriteLine(" - Удалить                                                                                            ");
            Console.WriteLine(" - Создать файл \".txt\"                                                                              ");
            Console.WriteLine(" - Конкатенация файлов \".txt\"                                                                       ");
            Console.WriteLine(" - Получить информацию о файле                                                                        ");
            Console.WriteLine(" - Вернуться к выбору файла (перемещение в корневую папку)                                            ");
            Console.WriteLine(" -----------------------------------------------------------------------------------------------------");
            Console.WriteLine(" Нажмите Enter, чтобы продолжить ...                                                                  ");
            Console.WriteLine(" Или любую другую клавишу, чтобы выйти.                                                               ");
        }

        /// <summary>
        /// Метод для копирования файла.
        /// </summary>
        /// <param name="pathToFile">Путь к файлу, который копируют.</param>
        private static void CopyFile(string pathToFile)
        {
            // Метод вызывает получение пути, куда копировать и выполняет копирование.

            try
            {
                int mode = 2;
                // Получаем путь ко 2 файлу.
                headToPrint = "ВЫБЕРИТЕ ПУТЬ КОПИРОВАНИЯ ФАЙЛА: (для подтверждения пути нажмите Enter)";
                GetPathToDirectory(mode);

                string name = "Copy" + Path.GetFileName(pathToFile);
                // PathToFile содержит в себе путь 1 файла.
                pathToDir += Path.VolumeSeparatorChar + name;
                File.Copy(pathToFile, pathToDir);

                Console.Clear();
                Console.WriteLine("Готово!!! Ваш файл расположен в : ");
                Console.WriteLine(pathToDir);
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine("Ошибка копирования файла: " + ex.Message);
                // Красивый выход в начало через ESC.
                return;
            }
        }

        /// <summary>
        /// Метод перемещения файла.
        /// </summary>
        /// <param name="pathToFile">Путь, к перемещаемому файлу.</param>
        private static void ReplaceFile(string pathToFile)
        {
            // Обработка исключений.            
            try
            {
                int mode = 2;
                // Получаем путь ко 2 файлу.
                headToPrint = "ВЫБЕРИТЕ ПУТЬ ПЕРЕМЕЩЕНИЯ ФАЙЛА: (для подтверждения пути нажмите Enter)";
                GetPathToDirectory(mode);
                string name = Path.GetFileName(pathToFile);

                pathToDir += Path.VolumeSeparatorChar + name;
                File.Move(pathToFile, pathToDir);
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine("Ошибка перемещения файла: " + ex.Message);
                // Красивый выход в начало через ESC.
                return;
            }

        }

        /// <summary>
        /// Метод удаления файла.
        /// </summary>
        /// <param name="pathToFile">Путь удаляемого файла.</param>
        private static void DeleteFile(string pathToFile)
        {
            // Обработка исключений.
            try
            {
                File.Delete(pathToFile);
                Console.Clear();
                Console.WriteLine("Файл успешно удалён.");
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine("Ошибка удаления файла: " + ex.Message);
                // Красивый выход в начало через ESC.
                return;
            }

        }

        /// <summary>
        /// Метод создания текстового файла.
        /// </summary>
        /// <param name="pathToFile">Путь директории, в которой будет расположен файл.</param>
        /// <param name="encoding">Кодировка.</param>
        private static void CreateFileInSelectedEncodingMacOs(string pathToFile, Encoding encoding)
        {
            Console.Clear();
            string pathToChosenFile = Directory.GetParent(pathToFile).ToString() + Path.VolumeSeparatorChar + "NewTextFile.txt";
            
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
        /// Метод конкатенации файлов.
        /// </summary>
        /// <param name="pathToFile">Путь к первому файлу.</param>
        private static void ConcatinationOfFiles(string pathToFile)
        {
            // Ввод количества файлов.
            Console.Clear();
            int numberOfFiles;
            do
            {
                Console.WriteLine("Введите количество текстовых файлов, которые вы хотите склеить");
            } while (!int.TryParse(Console.ReadLine(), out numberOfFiles) || numberOfFiles < 1);

            // Обработка исклюяений.
            try
            {
                // Прочитали первый файл.
                string[] output = File.ReadAllLines(pathToFile);
                string pathToChosenFile = Directory.GetParent(pathToFile).ToString() + Path.VolumeSeparatorChar + "NewConcatTextFile.txt";
                File.AppendAllLines(pathToChosenFile, output, Encoding.UTF8);
                // Читаем остальные файлы.
                for (int i = 1; i < numberOfFiles; i++)
                {
                    int mode = 3;
                    headToPrint = "ВЫБЕРИТЕ ФАЙЛ ДЛЯ КОНКАТЕНАЦИИ: (для подтверждения файла нажмите Enter)";
                    GetPathToDirectory(mode);
                    output = File.ReadAllLines(pathToDir);
                    File.AppendAllLines(pathToChosenFile, output, Encoding.UTF8);
                }

                Console.Clear();
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
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine("Пожалуйста, выберите только поддерживаемые файлы \".txt\" ");
                Console.WriteLine("Ошибка: " + ex.Message);
                // Красивый выход в начало через ESC.
                return;
            }
        }


        /// <summary>
        /// Метод возвращает список дисков на Windows или переходит в /Users на Mac OS.
        /// </summary>
        /// <returns>Массив путей к дискам.</returns>
        private static string[] GetDrives()
        {

            string[] Directories = new string[1];
            // Получаем начальный путь в зависимости от системы.
            string systemIO = Environment.OSVersion.ToString();
            if (systemIO.ToLower().Contains("unix"))
            {
                // Для MacOS.
                Directories = new string[1] { "/Volumes" };
            }
            else
            {
                // Для Linux.
                Directories = new string[1];
                try
                {
                    DriveInfo[] drinfo = DriveInfo.GetDrives();
                    Directories = new string[drinfo.Length];
                    for (int i = 0; i < drinfo.Length; i++)
                    {
                        Directories[i] = drinfo[i].ToString();
                    }
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    Console.WriteLine("Ошибка: " + ex.Message);
                    // Красивый выход в начало через ESC.
                    Environment.Exit(0);
                }
            }
            return Directories;

        }

        /// <summary>
        /// Метод кладёт в массив Directories пути файлов в выбранной пользователем директории.
        /// То есть переходит на уровень ниже в директорию.
        /// </summary>
        /// <param name="Directories">Массив путей к файлам в данной директории.</param>
        /// <param name="choice">Выбранная пользователем директория(индекс в массиве путей).</param>
        /// <param name="mode">Режим работы(возвращать только папки или файлы).</param>
        private static void MoveInsideDirectory(ref string[] Directories, int choice, int mode)
        {
            string[] FilesInDiretory;
            string pathToChosenFile = Path.GetFullPath(Directories[choice]);
            Directories = Directory.GetDirectories(pathToChosenFile);
            // Если метод вызван не из методов копирования и перемещения.
            if (mode == 1 || mode == 3)
            {
                if (Directories.Length != 0)
                {
                    FilesInDiretory = Directory.GetFiles(pathToChosenFile);

                    // Конкатенация двух массивов(файлов и директорий).
                    int Length = Directories.Length;
                    Array.Resize(ref Directories, Length + FilesInDiretory.Length);
                    for (int i = Length; i < Directories.Length; i++)
                    {
                        Directories[i] = FilesInDiretory[i - Length];
                    }
                }
                else
                {
                    // Если директория, в которую перешел пользователь пустая, то в массив кладем только файлы.
                    Directories = Directory.GetFiles(pathToChosenFile);

                }
            }

        }

        /// <summary>
        /// Метод кладёт в массив Directories пути файлов директории на уровень выше.
        /// То есть переходит на уровень выше в директорию.
        /// </summary>
        /// <param name="Directories">Массив путей к файлам.</param>
        /// <param name="mode">Режим работы метода(возвращать только папки или файлы).</param>
        private static void MoveToParentDirectory(ref string[] Directories, int mode)
        {
            string[] FilesInDirectory;

            // Получение пути до родительской папки.
            string pathToChosenFile;
            if (Directories.Length != 0)
            {
                pathToChosenFile = Directory.GetParent(Directories[0]).ToString();
                pathToChosenFile = Directory.GetParent(pathToChosenFile).ToString();
            }
            else
            {
                pathToChosenFile = pathToParentDirectory;
            }

            Directories = Directory.GetDirectories(pathToChosenFile);
            // Если метод вызван не из методов копирования и перемещения.
            if (mode == 1 || mode == 3)
            {
                FilesInDirectory = Directory.GetFiles(pathToChosenFile);

                // Конкатенация двух массивов.
                int Length = Directories.Length;
                Array.Resize(ref Directories, Length + FilesInDirectory.Length);
                for (int i = Length; i < Directories.Length; i++)
                {
                    Directories[i] = FilesInDirectory[i - Length];
                }
            }


        }

        /// <summary>
        /// Главынй метод программы: позволяет перемещаться по директориям во всех направлениях и возвращает путь к файлу.
        /// Возможет вызов подменю (при нажатии Tab).
        /// </summary>
        /// <param name="mode2">Режим работы (можно ли открывать подменю или нет)</param>
        private static void GetPathToDirectory(int mode2)
        {
            /*  
                Первым делом метод получает информацию о количестве дисков, потом о файлах.
                Это метод, в котором будут обрабатываться шаги по директориям
                При нажатии Tab, открывается подменю, вызывается (CallSubmenu) метод.
                При этом путь первого файла подтверждается автоматически, нажимать Enter не нужно.
                Если метод вызван из  методов изменения файлов, то путь подтверждается нажатием Enter.
                ---------------------------------------------------------------------------------------
                Графическая навигация по папкам:

                - Стрелочки вверх и вниз позволяют перемещаться между папками и файлами в рамках текущей директории.
                - Стрелочкой вправо можно перейти в выбранную директорию .
                  Для удобства пользователю, если выбранная директория не содержит файлов и поддиректорий, то перейти в нее нельзя.
                  Также стрелочкой вправо нельзя перейти в файл, что очевидно.
                - Стрелочкой влево можно возвращаться в предыдущую директорию, пока не достигните корневой папки.
            

               Метод возвращает путь к файлу:
               ------------------------------
               В зависимости от режима mode:
               1 - можно перемещаться по файлам и вызывать подменю.
               2 - можно только перемещаться по папкам(подтверждение выбора Enter)
               3 - можно пермещаться по папкам и файлам, подтверждение пути Enter.
             */

            string[] Directories;
            // Получение дисков.
            Directories = GetDrives();

            while (true)
            {
                // Если дирректория пустая, то оставить пользователя в текущей папке.
                if (Directories.Length == 0)
                {
                    dirIsEmpty = true;
                    MoveToParentDirectory(ref Directories, mode2);
                    dirIsEmpty = false;
                    try
                    {
                        pathToParentDirectory = Directory.GetParent(pathToParentDirectory).ToString();
                    }
                    catch (Exception ex)
                    {
                        Console.Clear();
                        Console.WriteLine("Ошибка: " + ex.Message);
                        // Красивый выход в начало через ESC.
                        return;
                    }
                    continue;
                }


                string[] FilesAndDirectoriesToPrint = new string[Directories.Length];
                // Создаем массив имен файлов (некий аналог frontend).
                for (int i = 0; i < Directories.Length; i++)
                {
                    try
                    {
                        FilesAndDirectoriesToPrint[i] = Path.GetFileName(Directories[i]);
                    }
                    catch (Exception ex)
                    {
                        Console.Clear();
                        Console.WriteLine("Ошибка: " + ex.Message);
                        // Красивый выход в начало через ESC.
                        return;

                    }
                }

            // Точка повторения, если при нажатии Tab (вызов подменю) пользователь навелся не на файл, а директорию.
            RepeatIfItIsNotFile:

                int choice;
                if (mode2 == 1)
                {
                    choice = GetUserchoice(FilesAndDirectoriesToPrint, 0);
                }
                else
                {
                    // Если метод получения пути вызван из методов копирования и перемещения файла.
                    choice = GetUserchoice(FilesAndDirectoriesToPrint, 2);
                }

                // Перейти в директорию на уровень выше.
                if (choice == -1)
                {
                    try
                    {
                        MoveToParentDirectory(ref Directories, mode2);
                    }
                    catch (System.Security.SecurityException ex)
                    {
                        Console.Clear();
                        Console.WriteLine("У вас нет прав на это действие: " + ex.Message);
                        return;
                    }
                    catch (Exception ex)
                    {
                        Console.Clear();
                        Console.WriteLine("Ошибка получения пути директории: " + ex.Message);
                        // Красивый выход в начало через ESC.
                        return;

                    }

                }
                // Перейти внуть директории.
                else if (choice >= 0 && choice < Directories.Length)
                {
                    try
                    {
                        // Для печати пользователю текщей директории.
                        pathToParentDirectory = Directory.GetParent(Directories[choice]).ToString();

                        //  Переход внутрь директории.
                        MoveInsideDirectory(ref Directories, choice, mode2);
                    }
                    catch (System.Security.SecurityException ex)
                    {
                        Console.Clear();
                        Console.WriteLine("У вас нет прав на это действие: " + ex.Message);
                        return;
                    }
                    catch (Exception ex)
                    {
                        Console.Clear();
                        Console.WriteLine("Ошибка: " + ex.Message);
                        // Красивый выход в начало через ESC.
                        return;
                    }
                }
                else
                {
                    if (mode2 == 1)
                    {
                        //  Пользователь нажал Tab.
                        if (File.Exists(Directories[indexInArrayToChosenFile]))
                        {
                            try
                            {
                                // Получение пути к выбранному файлу.
                                string pathToChosenFile1 = Path.GetFullPath(Directories[indexInArrayToChosenFile]);
                                CallSubmenu(pathToChosenFile1);
                                break;
                            }
                            catch (Exception ex)
                            {
                                Console.Clear();
                                Console.WriteLine("Ошибка получения пути: " + ex.Message);
                                // красивый выход в начало через ESC.
                                return;
                            }
                        }
                        else
                        {
                            // Если пользователь нажал Tab не на файле, а на директории, то возвращаемся в исходную директорию.
                            goto RepeatIfItIsNotFile;
                        }
                    }
                    else
                    {
                        // Если метод получения пути вызван из методов копирования и перемещения файла и был нажат Enter.
                        pathToDir = Path.GetFullPath(Directories[indexInArrayToChosenFile]);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Метод возвращает выбор пользователя (индекс в массиве с путями текщей директории).
        /// </summary>
        /// <param name="Directories">Массив путей к файлам и директориям.</param>
        /// <param name="mode1">Режим работы(можно вызывать подменю или нельзя, тогда подтверждение с помощью Enter).</param>
        /// <returns>Выбор пользователя.</returns>
        private static int GetUserchoice(string[] Directories, int mode1)
        {
            /* 
               Метод подсвечивает выбор пользователеля возвращает номер пункта выбранного меню.
               mode обозначает режим работы метода:
               0 - метод вызван из GetPathToDirectory с mode = 1.
               2 - метод вызван из GetPathToDirectory с mode = 0.
            */

            Console.CursorVisible = false;
            // Чтобы не было мигающего курсора.
            int choice = 0;
            while (true)
            {
                PrintDirectories(Directories, choice);
                // Режим выбора файла, без подменю(определенный метод уже вызван).
                if (mode1 == 2)
                {
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.UpArrow:
                            choice--;
                            break;
                        case ConsoleKey.DownArrow:
                            choice++;
                            break;
                        case ConsoleKey.Enter:
                            indexInArrayToChosenFile = choice;
                            return -10;
                        case ConsoleKey.RightArrow:
                            return choice;
                        case ConsoleKey.LeftArrow:
                            return -1;
                    }
                }
                // Режим с подменю.
                if (mode1 == 0)
                {
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.UpArrow:
                            choice--;
                            break;
                        case ConsoleKey.DownArrow:
                            choice++;
                            break;
                        case ConsoleKey.RightArrow:
                            return choice;
                        case ConsoleKey.LeftArrow:
                            return -1;
                        // Если нажать Tab, то вызовется подменю CallSubMenu.
                        case ConsoleKey.Enter:
                            indexInArrayToChosenFile = choice;
                            return -2;
                    }
                }

                // Зацикливаем выбор.
                if (Directories.Length != 0)
                {
                    choice = (choice + Directories.Length) % Directories.Length;
                }
                else
                {
                    return -1;
                }
            }
        }

       
        
        /// <summary>
        /// Метод вызывает отображение в консоль подменю, вызывает метод выбора пункта подменю.
        /// </summary>
        /// <param name="pathToFile">Метод получает путь файла, на котором был нажат Tab.</param>
        private static void CallSubmenu(string pathToFile)
        {
            // Вызов оторажения submenu (PrintSubMenu).
            string[] subMenuItems = { " Вывод содержимого \".txt\" файла в консоль ", " Копировать ", " Переместить ", " Удалить",
                " Создать файл \".txt\" ", " Конкатенация файлов \".txt\" ", " Получить информацию о файле ", " Вернуться к выбору файла (перемещение в корневую папку)" };

            headToPrint = "ВЫБЕРИТЕ ОПЕРАЦИЮ: (для подтверждения выбора нажмите Enter)";
            int item = GetUserItem(subMenuItems);
            // Переменная для хранения кодировки.
            Encoding encoding;
            // Свитчер выбора метода.
            switch (item)
            {
                case 0:
                    encoding = ChooseEncoding();
                    ConsoleOutputFileInSelectedEncoding(pathToFile, encoding);
                    break;
                case 1:
                    CopyFile(pathToFile);
                    break;
                case 2:
                    ReplaceFile(pathToFile);
                    break;
                case 3:
                    DeleteFile(pathToFile);
                    break;
                case 4:
                    encoding = ChooseEncoding();
                    CreateFileInSelectedEncodingMacOs(pathToFile, encoding);
                    break;
                case 5:
                    ConcatinationOfFiles(pathToFile);
                    break;
                case 6:
                    GetInformationAboutFile(pathToFile);
                    break;
                // Возможность выхода из подменю.
                case 7:
                    GetPathToDirectory(1);
                    break;
            }
        }
    }
}
