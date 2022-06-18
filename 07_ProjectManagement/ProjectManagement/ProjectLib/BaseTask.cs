using System;

namespace ProjectLib
{
    [Serializable]
    public abstract partial class BaseTask
    {
        /// <summary>
        /// Название задачи.
        /// </summary>
        public string Name { get; protected set; }
        /// <summary>
        /// Дата создания задачи.
        /// </summary>
        public DateTime CreationDate { get; set; }
        /// <summary>
        /// Статус задачи.
        /// </summary>
        public Status TaskStatus { get; protected set; }

        public BaseTask(string name, DateTime creationDate)
        {
            Name = name;
            CreationDate = creationDate;
            TaskStatus = Status.Open;
        }

        /// <summary>
        /// Смена статуса задачи.
        /// </summary>
        /// <param name="status"></param>
        public void ChangeStatus(Status status)
        {
            TaskStatus = status;
        }

        /// <summary>
        /// Переименовать.
        /// </summary>
        /// <param name="newName"></param>
        public void Rename(string newName)
        {
            this.Name = newName;
        }

        /// <summary>
        /// Информация об задаче.
        /// </summary>
        /// <returns></returns>
        public string Info()
        {
            return $"{Name}, Дата создания: {CreationDate}, Статус: {TaskStatus}";
        }
    }
}
