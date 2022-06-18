using System;
using System.Collections.Generic;

namespace ProjectLib
{
    [Serializable]
    public class Project
    {
        /// <summary>
        /// Ограниченное количество задач.
        /// </summary>
        private const int maxNumberOfTasks = 1000;

        /// <summary>
        /// Имя проекта.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Список задач.
        /// </summary>
        private List<BaseTask> tasks;

        public Project(string name)
        {
            Name = name;
            tasks = new List<BaseTask>(maxNumberOfTasks);
        }

        /// <summary>
        /// Добавить задачу.
        /// </summary>
        /// <param name="task"></param>
        public void AddTask(BaseTask task)
        {
            if (tasks.Count >= maxNumberOfTasks)
                throw new ArgumentOutOfRangeException("The maximum number of tasks has been exceeded");
            else
                tasks.Add(task);
        }

        /// <summary>
        /// Убрать задачу(по имени).
        /// </summary>
        /// <param name="taskname"></param>
        /// <returns></returns>
        public bool RemoveTask(string taskname)
        {
            if (tasks is not null)
            {
                return tasks.Remove(tasks.Find(task => task.Name == taskname));
            }
            return false;
        }

        /// <summary>
        /// Убрать задачу.
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public bool RemoveTask(BaseTask task)
        {
            if (task is not null)
            {
                return tasks.Remove(task);
            }
            return false;
        }

        /// <summary>
        /// Просмотреть информацию.ы
        /// </summary>
        /// <returns></returns>
        public string ViewInformation()
        {
            return $"Name: {Name}, number of tasks: {tasks?.Count}";            
        }

        /// <summary>
        /// Переименовть проект.
        /// </summary>
        /// <param name="newName"></param>
        public void Rename(string newName)
        {
            this.Name = newName;
        }

        /// <summary>
        /// Получение подзадач Epic.
        /// </summary
        /// <returns></returns>
        public List<BaseTask> GetTasks()
        {
            var localTasks = new List<BaseTask>();

            localTasks.AddRange(tasks.FindAll(task => task.TaskStatus == BaseTask.Status.Open));
            localTasks.AddRange(tasks.FindAll(task => task.TaskStatus == BaseTask.Status.InProgress));
            localTasks.AddRange(tasks.FindAll(task => task.TaskStatus == BaseTask.Status.Closed));

            return localTasks;
        }
    }
}
