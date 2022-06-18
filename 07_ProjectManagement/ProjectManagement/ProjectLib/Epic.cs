using System;
using System.Collections.Generic;

namespace ProjectLib
{
    [Serializable]
    public class Epic : BaseTask
    {
        private List<BaseTask> subtasks;

        public Epic(string name, DateTime creationDate) : base(name, creationDate)
        {
            subtasks = new List<BaseTask>();
        }

        /// <summary>
        /// Добавить подзадачу.
        /// </summary>
        /// <param name="task"></param>
        public void AddSubtask(BaseTask task)
        {
            if (task is Epic || task is Bug)
            {
                throw new ArgumentException("Invalid subtask type!");
            }
            else
            {
                subtasks.Add(task);
            }
        }

        /// <summary>
        /// Убрать подзадачу(по имени).
        /// </summary>
        /// <param name="taskName"></param>
        /// <returns></returns>
        public bool RemoveSubtasks(string taskName)
        {
            if (subtasks is not null && subtasks.Exists(task => task.Name == taskName))
            {
                subtasks.Remove(subtasks.Find(task => task.Name == taskName));
            }
            return false;
        }

        /// <summary>
        /// Убрать задачу.
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public bool RemoveSubtasks(BaseTask task)
        {
            if (subtasks is not null)
            {
                subtasks.Remove(task);
            }
            return false;
        }

        /// <summary>
        /// Получение подзадач.
        /// </summary>
        /// <returns></returns>
        public List<BaseTask> GetSubTasks()
        {
            var localTasks = new List<BaseTask>();

            localTasks.AddRange(subtasks.FindAll(task => task.TaskStatus == BaseTask.Status.Open));
            localTasks.AddRange(subtasks.FindAll(task => task.TaskStatus == BaseTask.Status.InProgress));
            localTasks.AddRange(subtasks.FindAll(task => task.TaskStatus == BaseTask.Status.Closed));

            return localTasks;
        }
    }
}
