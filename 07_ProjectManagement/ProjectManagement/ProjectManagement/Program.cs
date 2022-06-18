using System;
using System.Collections.Generic;
using System.Threading;
using ProjectLib;

namespace ProjectManagement
{
    partial class MainClass
    {
        /// <summary>
        /// Список пользователей.
        /// </summary>
        private static List<User> usersPool;
        /// <summary>
        /// Список проектов.
        /// </summary>
        private static List<Project> projectsPool;
        /// <summary>
        /// Подсказка(заголовок) пользователю.
        /// </summary>
        private static string headToPrint;

        /// <summary>
        /// Заголовки.
        /// </summary>
        private static readonly string mainMenuHead = "ГЛАВНОЕ МЕНЮ:";
        private static readonly string usersMenuHead = "ПОЛЬЗОВАТЕЛИ:";
        private static readonly string projectMenuHead = "ПРОЕКТЫ:";
        private static readonly string taskMenuHead = "РЕДАКТИРОВАНИЕ ПРОЕКТА:";
        private static readonly string taskSubMenuHead = "РЕДАКТИРОВАНИЕ ЗАДАЧИ:";
        private static readonly string epicSubMenuHead = "РЕДАКТИРОВАНИЕ EPIC ЗАДАЧИ:";

        /// <summary>
        /// Пункты меню.
        /// </summary>
        private static readonly string[] mainMenuTips = { "Пользователи", "Проекты", "Выход" };
        private static readonly string[] usersMenuTips = { "Просмотр списка", "Создать", "Удалить", "Назад" };
        private static readonly string[] projectMenuTips = { "Создать проект", "Посмотреть проекты", "Назад" };
        private static readonly string[] taskSubMenuTips = { "Назначить исполнителя", "Удалить исполинтеля", "Изменить статус", "Переименовать", "Удалить", "Назад" };
        private static readonly string[] epicSubMenuTips = { "Посмотреть подзадачи", "Добавить подзадачу", "Изменить статус", "Переименовать", "Удалить", "Назад" };
        private static readonly string[] taskMenuTips = { "Добавить Epic", "Добавить Story", "Добавить Task", "Добавить Bug", "Посмотреть задачи", "Переименовать", "Удалить", "Назад" };

        /// <summary>
        /// Основной метод, запускающий приложение.
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            try
            {
                PrintRules();
                if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                {
                    usersPool = new List<User>();
                    projectsPool = new List<Project>();
                    // Считывание информации с файла.
                    ReadFromBinaryFile();

                    Timer timer = new Timer(TimerCallback, null, 0, 5000);

                    MainMenu();
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine(ex.Message);
                Thread.Sleep(2000);
            }
        }

        /// <summary>
        /// Сериализация по таймеру.
        /// </summary>
        /// <param name="obj"></param>
        private static void TimerCallback(Object obj)
        {
            WriteToBinaryFile();
        }

        /// <summary>
        /// Основное меню.
        /// </summary>
        private static void MainMenu()
        {
            headToPrint = mainMenuHead;
            switch (GetUserItem(mainMenuTips))
            {
                case 0:
                    UserMenu();
                    break;
                case 1:
                    ProjectMenu();
                    break;
                default:
                    Environment.Exit(0);
                    break;
            }
        }

        /// <summary>
        /// Меню проекта.
        /// </summary>
        private static void ProjectMenu()
        {
            headToPrint = projectMenuHead;
            switch (GetUserItem(projectMenuTips))
            {
                case 0:
                    AddProject();
                    break;
                case 1:
                    ViewProjects();
                    break;
                default:
                    MainMenu();
                    break;
            }
        }

        /// <summary>
        /// Просмотр списка проектов.
        /// </summary>
        private static void ViewProjects()
        {
            string[] projectNames = new string[projectsPool.Count + 1];
            for (int i = 0; i < projectsPool.Count; i++)
                projectNames[i] = projectsPool[i].ViewInformation();

            projectNames[projectsPool.Count] = "Назад";
            int res = GetUserItem(projectNames);
            if (res == projectsPool.Count)
                ProjectMenu();
            else
            {
                headToPrint = taskMenuHead;
                int outputUser = GetUserItem(taskMenuTips);
                switch (outputUser)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                        Console.Clear();
                        Console.WriteLine("Введите новое название задачи:");
                        string name = Console.ReadLine();
                        AddTask(outputUser, res, name);
                        break;
                    case 4:
                        ViewTasks(res);
                        break;
                    case 5:
                        RenameProject(res);
                        break;
                    case 6:
                        projectsPool.RemoveAt(res);
                        ViewProjects();
                        break;
                    default:
                        ViewProjects();
                        break;
                }
            }            
        }

        /// <summary>
        /// Просмотр задач проекта.
        /// </summary>
        /// <param name="index"></param>
        private static void ViewTasks(int index)
        {
            List<BaseTask> tasksList = projectsPool[index].GetTasks();
            List<string> taskNames = new List<string>();

            for (int i = 0; i < tasksList.Count; i++)
                taskNames.Add(tasksList[i].Info());

            taskNames.Add("Назад");
            headToPrint = "ПРОСМОТР ЗАДАЧ";
            int res = GetUserItem(taskNames.ToArray());
            // Обработка "Назад".
            if (res == taskNames.Count - 1)
                ViewProjects();
            else
            {
                var task = tasksList[res];
                if (task is Epic)
                    EpicTask(task, index);
                else
                    GeneralTask(task, index);
            }

        }

        /// <summary>
        /// Меню обычной задачи.
        /// </summary>
        /// <param name="task"></param>
        /// <param name="index"></param>
        private static void GeneralTask(BaseTask task, int index)
        {
            headToPrint = taskSubMenuHead;
            IAssignable basetask = (IAssignable)task;

            switch (GetUserItem(taskSubMenuTips))
            {
                case 0:
                    var referense1 = ChooseUser();
                    if (referense1 is not null)
                    {
                        basetask.AddUser(referense1);
                    }
                    ViewTasks(index);
                    break;
                case 1:
                    var referense2 = ChooseUser();
                    if (referense2 is not null)
                    {
                        basetask.RemoveUser(referense2);
                    }
                    ViewTasks(index);
                    break;
                case 2:
                    task.ChangeStatus(GiveStatus());
                    ViewTasks(index);
                    break;
                case 3:
                    Console.Clear();
                    Console.WriteLine("Введите новое имя");
                    string newName = Console.ReadLine();
                    task.Rename(newName);
                    ViewTasks(index);
                    break;
                case 4:
                    projectsPool[index].RemoveTask(task);
                    ViewTasks(index);
                    break;
                default:
                    ViewProjects();
                    break;
            }
        }

        /// <summary>
        /// Выбор пользователя из списка.
        /// </summary>
        /// <returns></returns>
        private static User ChooseUser()
        {
            if (usersPool.Count != 0)
            {
                string[] userNames = new string[usersPool.Count + 1];
                for (int i = 0; i < usersPool.Count; i++)
                {
                    userNames[i] = usersPool[i].Name;
                }
                userNames[usersPool.Count] = "Назад";
                headToPrint = "ВЫБОР ПОЛЬЗОВАТЕЛЯ";
                int index = GetUserItem(userNames);

                if (index == usersPool.Count)
                    return null;
                else
                    return usersPool[index];
            }
            else
                return null;
        }

        /// <summary>
        /// Получение статуса задачи.
        /// </summary>
        /// <returns></returns>
        private static BaseTask.Status GiveStatus()
        {
            headToPrint = "ВЫБЕРИТЕ СТАТУС";
            BaseTask.Status status;
            int res = GetUserItem(new string[] { "Open", "InProgress", "Closed" });
            if (res == 0)
                status = BaseTask.Status.Open;
            else if (res == 1)
                status = BaseTask.Status.InProgress;
            else
                status = BaseTask.Status.Closed;
            return status;
        }

        /// <summary>
        /// Обработка задачи epic.
        /// </summary>
        /// <param name="task"></param>
        /// <param name="index"></param>
        private static void EpicTask(BaseTask task, int index)
        {
            headToPrint = epicSubMenuHead;
            Epic epic = (Epic)task;

            switch (GetUserItem(epicSubMenuTips))
            {
                case 0:
                    GetSubTask(task, index, epic);
                    break;
                case 1:
                    headToPrint = "ВЫБЕРИТЕ ТИП ЗАДАЧИ";
                    if (GetUserItem(new string[] { "Task", "Story" }) == 0)
                    {
                        Console.Clear();
                        Console.WriteLine("Введите название новой задачи:");
                        epic.AddSubtask(new Task(Console.ReadLine(), DateTime.Now));
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Введите название новой задачи:");
                        epic.AddSubtask(new Story(Console.ReadLine(), DateTime.Now));
                    }
                    ViewTasks(index);
                    break;
                case 2:
                    task.ChangeStatus(GiveStatus());
                    ViewTasks(index);
                    break;
                case 3:
                    Console.Clear();
                    Console.WriteLine("Введите новое имя:");
                    string newName = Console.ReadLine();
                    task.Rename(newName);
                    ViewTasks(index);
                    break;
                case 4:
                    //
                    projectsPool[index].RemoveTask(task);
                    ViewTasks(index);
                    break;
                default:
                    ViewProjects();
                    break;
            }
        }

        /// <summary>
        /// Получение подзадач epic.
        /// </summary>
        /// <param name="task"></param>
        /// <param name="index"></param>
        /// <param name="epic"></param>
        private static void GetSubTask(BaseTask task, int index, Epic epic)
        {
            var subtasks = epic.GetSubTasks();
            string[] names = new string[subtasks.Count + 1];
            for (int i = 0; i < subtasks.Count; i++)
            {
                names[i] = $" | {subtasks[i].Info()}";
            }
            names[subtasks.Count] = "Назад";

            int res = GetUserItem(names);
            if (res == subtasks.Count)
                ViewTasks(index);
            else
            {
                GeneralTask(subtasks[res], index, epic);
            }
        }

        /// <summary>
        /// Метод обрабатывает обычные задачи.
        /// </summary>
        /// <param name="task"></param>
        /// <param name="index"></param>
        /// <param name="epic"></param>
        private static void GeneralTask(BaseTask task, int index, Epic epic)
        {
            headToPrint = taskSubMenuHead;
            IAssignable basetask = (IAssignable)task;

            switch (GetUserItem(taskSubMenuTips))
            {
                case 0:
                    var referense1 = ChooseUser();
                    if (referense1 is not null)
                    {
                        basetask.AddUser(referense1);
                    }
                    ViewTasks(index);
                    break;
                case 1:
                    // выбор юзера из списка
                    var referense2 = ChooseUser();
                    if (referense2 is not null)
                    {
                        basetask.RemoveUser(referense2);
                    }
                    ViewTasks(index);
                    break;
                case 2:
                    task.ChangeStatus(GiveStatus());
                    ViewTasks(index);
                    break;
                case 3:
                    Console.Clear();
                    Console.WriteLine("Введите новое имя");
                    string newName = Console.ReadLine();
                    task.Rename(newName);
                    ViewTasks(index);
                    break;
                case 4:
                    epic.RemoveSubtasks(task);
                    ViewTasks(index);
                    break;
                default:
                    ViewProjects();
                    break;
            }
        }

        /// <summary>
        /// Добавление задач.
        /// </summary>
        /// <param name="taskIndex"></param>
        /// <param name="projectIndex"></param>
        /// <param name="name"></param>
        private static void AddTask(int taskIndex, int projectIndex, string name)
        {
            try
            {
                if (taskIndex == 0)
                    projectsPool[projectIndex].AddTask(new Epic(name, DateTime.Now));
                else if (taskIndex == 1)
                    projectsPool[projectIndex].AddTask(new Story(name, DateTime.Now));
                else if (taskIndex == 2)
                    projectsPool[projectIndex].AddTask(new Task(name, DateTime.Now));
                else
                    projectsPool[projectIndex].AddTask(new Bug(name, DateTime.Now));
            }
            catch (ArgumentException ex)
            {
                Console.Clear();
                Console.WriteLine(ex);
                Thread.Sleep(2000);
            }
            finally
            {
                ViewProjects();
            }
        }

        /// <summary>
        /// Переименовать проект.
        /// </summary>
        /// <param name="index"></param>
        private static void RenameProject(int index)
        {
            Console.Clear();
            Console.WriteLine("Введите новое название проекта:");
            string name = Console.ReadLine();

            if (projectsPool.Exists(proj => proj.Name == name))
            {
                Console.WriteLine("Проект с таким именем уже существует.");
                Thread.Sleep(2000);
            }
            else
            {
                projectsPool[index].Rename(name);
            }
            ViewProjects();
        }

        /// <summary>
        /// Добавить проект.
        /// </summary>
        private static void AddProject()
        {
            Console.Clear();
            Console.WriteLine("Введите название проекта:");
            string name = Console.ReadLine();

            if (projectsPool.Exists(proj => proj.Name == name))
            {
                Console.WriteLine("Проект с таким именем уже существует.");
                Thread.Sleep(2000);
                ProjectMenu();
            }
            else
            {
                Project project = new Project(name);
                projectsPool.Add(project);
                ProjectMenu();
            }
        }

        /// <summary>
        /// Меню пользователей.
        /// </summary>
        private static void UserMenu()
        {
            headToPrint = usersMenuHead;
            switch (GetUserItem(usersMenuTips))
            {
                case 0:
                    ViewUsers();
                    break;
                case 1:
                    AddUser();
                    break;
                case 2:
                    DeleteUser();
                    break;
                default:
                    MainMenu();
                    break;
            }
        }

        /// <summary>
        /// Удаление пользователя.
        /// </summary>
        private static void DeleteUser()
        {
            var referense = ChooseUser();
            if (referense is not null)
            {
                bool result = usersPool.Remove(referense);
                if (!result)
                {
                    Console.WriteLine("Пользователя с таким именем не существует!");
                    Thread.Sleep(2000);
                }
            }
            UserMenu();
        }

        /// <summary>
        /// Добавить пользователя.
        /// </summary>
        private static void AddUser()
        {
            Console.Clear();
            Console.WriteLine("Введите имя пользователя:");
            string name = Console.ReadLine();

            if (usersPool.Exists(usr => usr.Name == name))
            {
                Console.WriteLine("Пользователь с таким именем уже существует.");
                Thread.Sleep(2000);
            }
            else
            {
                User user = new User(name);
                usersPool.Add(user);
            }
            UserMenu();
        }

        /// <summary>
        /// Посмотреть список пользователей.
        /// </summary>
        private static void ViewUsers()
        {
            Console.Clear();
            foreach (var user in usersPool)
            {
                Console.WriteLine(user.Name);
            }
            Console.WriteLine("Нажмите любую клавишу чтобы вернуться назад");
            Console.ReadKey();
            UserMenu();
        }

        /// <summary>
        /// Печать правил.
        /// </summary>
        private static void PrintRules()
        {
            // Вызов подсказок в самом начале, как пользоваться программой.
            Console.WriteLine();
            Console.WriteLine("     Добро пожаловать Менеджер проектов !!!                                                           ");
            Console.WriteLine(" -----------------------------------------------------------------------------------------------------");
            Console.WriteLine(" ПОЖАЛУЙСТА!!! Для коректной работы программы, откройте терминал в отдельной вкладке на весь экран    ");
            Console.WriteLine();
            Console.WriteLine(" Навигация реализована с помощью следующих физических клавиш:                                         ");
            Console.WriteLine(" -----------------------------------------------------------------------------------------------------");
            Console.WriteLine(" Стрелочки вниз и вверх - позволяют перемещаться по пунктам меню.                                     ");
            Console.WriteLine(" Enter - позволяет вызвать подменю, связанное с выбранным проектом.                                   ");
            Console.WriteLine(" Навигация в подменю аналогичная: перемещение стрелочками, для выбора нажмите - Enter                 ");
            Console.WriteLine(" -----------------------------------------------------------------------------------------------------");
            Console.WriteLine(" В приложении реализован весь стандартный функционал, а также интерфейс IAssignable.                  ");
            Console.WriteLine(" -----------------------------------------------------------------------------------------------------");
            Console.WriteLine(" Нажмите Enter, чтобы продолжить ...                                                                  ");
            Console.WriteLine(" Или любую другую клавишу, чтобы выйти.                                                               ");
        
        }

        /// <summary>
        /// Метод подсвечивает выбор пользователеля возвращает номер выбранного пункта.
        /// </summary>
        /// <param name="Items">Пункты меню.</param>
        /// <returns>Выбор пользователя.</returns>
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
                // Зацикливаем выбор.
                choice = (choice + items.Length) % items.Length;
            }
        }

        /// <summary>
        /// Метод печатает передоваемое меню, подсвечивая красным цветом текущий выбор пользователя.
        /// </summary>
        /// <param name="Directories">Массив отображаемых названий.</param>
        /// <param name="choice">Выобор пользователя.</param>
        private static void PrintFunctions(string[] items, int choice)
        {
            Console.Clear();
            // Печать шапки, которая подсказывает что делать.
            Console.WriteLine(headToPrint);
            Console.WriteLine();

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

    }
}
