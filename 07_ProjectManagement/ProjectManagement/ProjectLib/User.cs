using System;

namespace ProjectLib
{
    [Serializable]
    public class User
    {
        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string Name { get; set; }

        public User(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Переименовать пользователя.
        /// </summary>
        /// <param name="newName"></param>
        public void Rename(string newName)
        {
            Name = newName;
        }
    }
}
