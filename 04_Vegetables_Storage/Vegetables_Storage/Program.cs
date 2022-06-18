using System;
using System.IO;

namespace Vegetables_Storage
{
    class Program
    {
        // Helper string for user.
        private static string headToPrint;
        private static int capacity;
        private static double price;
        private static Storage storage;

        /// <summary>
        /// Method create the single storage.
        /// </summary>
        private static void CreateStorage()
        {
            storage = new Storage(capacity, price);
            // Forces to collect garbage from previous start.
            GC.Collect();
        }

        /// <summary>
        /// Method get's the max capasity and storage.
        /// </summary>
        private static void ConsoleInput()
        {
            try
            {
                Console.Clear();
                do
                {
                    Console.WriteLine("Enter max capacity of the storage:");
                } while (!int.TryParse(Console.ReadLine(), out capacity) || capacity < 1);
                do
                {
                    Console.WriteLine("Enter container storage cost:");
                } while (!double.TryParse(Console.ReadLine(), out price) || price <= 0);
            }
            catch (OutOfMemoryException ex)
            {
                Console.WriteLine("Exeption:" + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exeption:" + ex.Message);
            }
        }

        /// <summary>
        /// Method execute user's actions.
        /// </summary>
        private static void Actions()
        {
            int actions = 0;
            try
            {
                do
                {
                    Console.WriteLine("Enter the number of actions with the storage:");
                } while (!int.TryParse(Console.ReadLine(), out actions) || actions < 0);
            }
            catch (OutOfMemoryException ex)
            {
                Console.WriteLine("Exeption:" + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exeption:" + ex.Message);
            }

            // Execute the actions.
            for (int i = 0; i < actions; i++)
            {
                // Items to print.
                string[] items = new string[] { " Add container ", " Remove container " };

                headToPrint = "Choose the option";
                if (GetUserItem(items) == 0)
                {
                    Console.Clear();
                    try
                    {
                        storage.AddContainer();
                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine("Exeption: " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Exeption: " + ex.Message);
                    }
                }
                else
                {
                    int index;
                    Console.Clear();
                    do
                    {
                        Console.WriteLine($"Enter the number of the container to be deleted from 1 to {storage.GetCapacity()}.");
                    } while (!int.TryParse(Console.ReadLine(), out index) || index < 1);

                    Console.Clear();
                    try
                    {
                        if (storage.GetCapacity() != 0)
                        {
                            // Remove container (index starts from 0).
                            storage.RemoveContainer(index - 1);
                        }
                    }
                    catch (ArgumentOutOfRangeException ex)
                    {
                        Console.WriteLine("Exeption: " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Exeption: " + ex.Message);
                    }
                }
            }
        }

        /// <summary>
        /// Method works with user's files. Read paramenters of storage, actions with containers.
        /// </summary>
        private static void FileInput()
        {
            try
            {   // Read storage parameters.
                string[] parameters = File.ReadAllText("StorageParameters.txt").Split(' ');
                if (!double.TryParse(parameters[0].Trim(), out price) || !int.TryParse(parameters[1].Trim(), out capacity) || capacity < 1 || price <= 0.0)
                {
                    Console.WriteLine("Error: Can't reading the file: File must contain 2 parameters separated by a space.");
                    return;
                }
                // Create single storage.
                CreateStorage();
                // Read the actions.
                string[] actions = File.ReadAllLines("ActionsFile.txt");
                int counter = 0;
                foreach (var command in actions)
                {
                    string[] action;
                    // Get second parameter from the line.
                    int numberOfBoxes = GetNumberOfBoxes(command, out action);
                    if (numberOfBoxes == 0)
                        return;

                    switch (action[0])
                    {
                        case "Add container":

                            double[] price;
                            double[] weight;
                            string[] info;
                            // Read boxes from file.
                            if (GetBoxes(File.ReadAllLines("ContainersFile.txt"), counter, numberOfBoxes, out price, out weight, out info))
                            {
                                
                                storage.AddContainer(numberOfBoxes, price, weight, info);
                                counter++;
                            }
                            else
                                Console.WriteLine("Invalid box format (wrong number or invalid values or invalid separators).");

                            break;
                        case "Remove container":
                            try
                            {   // Remove container (index starts from 0).
                                storage.RemoveContainer(numberOfBoxes - 1);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Index out of range:" + ex.Message);
                            }
                            break;
                        default:
                            Console.WriteLine("Action recognition error.");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading information from files:" + ex.Message);
            }

        }

        /// <summary>
        /// Method gets the boxes from the file.
        /// </summary>
        /// <param name="containers">Input string with boxes.</param>
        /// <param name="counter">Current action line in file.</param>
        /// <param name="numberOfBoxes">Number of boxes to validate input.</param>
        /// <param name="price">Array of box prices.</param>
        /// <param name="weight">Array of box weights.</param>
        /// <returns>True, if input is correct, otherwise false.</returns>
        private static bool GetBoxes(string[] containers, int counter, int numberOfBoxes, out double[] price, out double[] weight, out string[] info)
        {
            // Initialize the arrays.
            info = new string[numberOfBoxes];
            price = new double[numberOfBoxes];
            weight = new double[numberOfBoxes];
            // Split to get boxes.
            string[] boxes = containers[counter].Trim().Split(';');
            if (boxes.Length != numberOfBoxes)
                return false;

            for (int i = 0; i < boxes.Length; i++)
            {   // Split to get price and weight of the box.
                info[i] = boxes[i].Split('/')[1].Trim();
                string[] parameters = boxes[i].Split('/')[0].Trim().Split(' ');
                if (parameters.Length != 2)
                    return false;
                if (!double.TryParse(parameters[0], out price[i]) || price[i] < 0.0)
                    return false;
                if (!double.TryParse(parameters[1], out weight[i]) || weight[i] < 0.0)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Method give the nuber of boxes.
        /// </summary>
        /// <param name="command">Input line</param>
        /// <param name="action">Array of current action(two parameters)</param>
        /// <returns></returns>
        private static int GetNumberOfBoxes(string command, out string[] action)
        {
            int numberOfBoxes = 0;
            // Split to get number of boxes.
            action = command.Trim().Split(';');
            if (action.Length == 2)
            {
                action[1] = action[1].Trim();
                if (!int.TryParse(action[1], out numberOfBoxes))
                    return 0;
                else
                    return numberOfBoxes;
            }
            else
                return 0;
        }

        /// <summary>
        /// Method runs the program.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // Print helper note.
            Help();
            if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                do
                {   // Items to print.
                    string[] items = new string[] { "Console input", "File input" };
                    headToPrint = "Choose the item (Press Enter to confirm))";
                    if (GetUserItem(items) == 0)
                    {
                        // Handle the input.
                        ConsoleInput();
                        CreateStorage();
                        // Hndle the actions.
                        Actions();

                        string[] items2 = new string[] { "Console output", "File output" };
                        headToPrint = "Choose the item (Press Enter to confirm)";
                        if (GetUserItem(items2) == 0)
                            storage.ShowInfo();
                        else
                            storage.ShowInfo("OutputFile.txt");
                    }
                    else
                    {
                        // Handle all input from the files.
                        FileInput();

                        string[] items2 = new string[] { "Console output", "File output" };
                        headToPrint = "Choose the item (Press Enter to confirm)";
                        if (GetUserItem(items2) == 0)
                            storage.ShowInfo();
                        else
                        {
                            storage.ShowInfo("OutputFile.txt");
                            Console.WriteLine("Output information located in file \"OutputFile.txt\"");
                        }
                    }

                    Console.WriteLine($"{Environment.NewLine} Press Esc to exit, Enter to repeat...");
                } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
            else
                Environment.Exit(0);


        }

        /// <summary>
        /// Method highlights the user's choice and returns the number of the selected item.
        /// </summary>
        /// <param name="Items">Menu items.</param>
        /// <returns>User choise.</returns>
        private static int GetUserItem(string[] items)
        {
            Console.CursorVisible = false;
            int choice = 0;
            while (true)
            {
                PrintFunctions(items, choice);

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
                // Looping the selection.
                choice = (choice + items.Length) % items.Length;
            }
        }

        /// <summary>
        /// Method prints the transmitted menu, highlighting the current user selection in red.
        /// </summary>
        /// <param name="Directories">Array of items.</param>
        /// <param name="choice">User choise.</param>
        private static void PrintFunctions(string[] items, int choice)
        {
            Console.Clear();
            // Helper string for user.
            Console.WriteLine(headToPrint + Environment.NewLine);

            for (int i = 0; i < items.Length; i++)
            {
                if (i == choice)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(items[i]);
                    Console.ResetColor();
                }
                else
                    Console.WriteLine(items[i]);

            }
        }

        /// <summary>
        /// Method print all rules.
        /// </summary>
        private static void Help()
        {
            Console.WriteLine();
            Console.WriteLine("     Добро пожаловать в \"Склад овощей\" !!!                                                     ");
            Console.WriteLine(" --------------------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine(" ПОЖАЛУЙСТА!!! Для корректной работы программы, откройте терминал в отдельной вкладке на весь экран.  ");
            Console.WriteLine();
            Console.WriteLine(" !В качестве доп. функционала реализованы: возможность выбора ввода и вывода с помощью стрелочек, у каждого ящика добавлен параметр \"Info\",");
            Console.WriteLine(" в который необходимо записывать описание, что хранит ящик.");
            Console.WriteLine(" Навигация устроена следующим образом:                                                                ");
            Console.WriteLine(" --------------------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine(" Выбор режима ввода данных осуществляется с помощью стрелочек \"вверх\" и \"вниз\" на клавиатуре.                ");
            Console.WriteLine(" Для подтверждения выбора нажмите Enter.                                                              ");
            Console.WriteLine(" --------------------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine(" При выборе режима ввода с файла, используйте следующий формат: " + Environment.NewLine);
            Console.WriteLine(" Файл StorageParameters.txt : в первой строке через пробел вводятся 2 значения \"Цена за хранение контейнера\" и \"Вместительность\"");
            Console.WriteLine(" Файл ActionsFile.txt : поддерживается 2 вида команд:");
            Console.WriteLine("     Add container;[количество ящиков в контейнере]");
            Console.WriteLine("     Remove container;[индекс удаляемого контейнера(от 1 до кол-ва контейнеров)]" + Environment.NewLine);
            Console.WriteLine(" Файл ContainersFile.txt : в каждой строке вводится один контейнер, формат ввода:");
            Console.WriteLine("     [цена ящика](пробел)[масса] /[Информация о ящике]/;  !Ящики между собой разделются точкой с запятой!  ");
            Console.WriteLine(" При выборе режима ввода с консоли, следуйте инструкциям на экране." + Environment.NewLine);
            Console.WriteLine(" --------------------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine(" Press Esc to exit, Enter to start...                                                        ");
        }
    }
}
