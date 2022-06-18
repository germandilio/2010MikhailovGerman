using System;
using System.Collections.Generic;

namespace ProjectLib
{
    [Serializable]
    public class Story : BaseTask, IAssignable
    {
        public Story(string name, DateTime creationDate) : base(name, creationDate)
        {
            users = new List<User>();
        }
        /// <summary>
        /// Список исполнителей.
        /// </summary>
        private List<User> users;
        public List<User> Users
        {
            get
            {
                return users;
            }
            set
            {
                users = value;
            }
        }

        /// <summary>
        /// Метод добавления пользователей.
        /// </summary>
        /// <param name="user"></param>
        public void AddUser(User user)
        {
            if (users is not null && !users.Exists(x => x.Name == user.Name))
            {
                users.Add(user);
            }
        }

        /// <summary>
        /// Убрать пользователей.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool RemoveUser(User user)
        {
            if (users is not null)
            {
                return users.Remove(user);
            }
            return false;
        }

        /// <summary>
        /// Показать список пользователей.
        /// </summary>
        public void ViewListOfUsers()
        {
            Console.WriteLine("Users: ");
            foreach (var user in users)
            {
                Console.WriteLine(user.Name);
            }
        }
    }
}
