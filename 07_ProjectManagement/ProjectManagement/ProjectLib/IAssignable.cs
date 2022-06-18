using System.Collections.Generic;

namespace ProjectLib
{
    /// <summary>
    /// Интерфейс - обязывающий реализацию методов добавления и удаления пользователей.
    /// </summary>
    public interface IAssignable
    {
        public List<User> Users { get; set; }

        public void AddUser(User user);
        public bool RemoveUser(User user);
        public void ViewListOfUsers();
    }
}
