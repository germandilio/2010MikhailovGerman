using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Storage
{
    [Serializable]
    class Client
    {
        /// <summary>
        /// Имя клиента.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Фамилия клиента.
        /// </summary>
        public string Surname { get; set; }
        /// <summary>
        /// Отчество клиента.
        /// </summary>
        public string Patronymic { get; set; }
        /// <summary>
        /// Телефонный номер.
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// Email - login.
        /// </summary>
        public string EMail { get; set; }
        /// <summary>
        /// Адрес.
        /// </summary>
        public string Adress { get; set; }
        /// <summary>
        /// Пароль.
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Список заказов.
        /// </summary>
        public List<Order> Orders { get; set; }
        /// <summary>
        /// Текущая корзина.
        /// </summary>
        public List<Tuple<int, Product>> Basket { get; set; }

        public Client(string surname, string name, string patronymic, string phone, string email, string adress, string password)
        {
            if (string.IsNullOrEmpty(surname))
                throw new ArgumentException("Фамилия - обязательное поле");
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Имя - обязательное поле");
            if (patronymic == null)
                patronymic = String.Empty;

            // validate phone number.

            if (!Client.IsValidEmail(email))
                throw new ArgumentException("Некорректный email");
            if (string.IsNullOrEmpty(adress))
                throw new ArgumentException("Адрес не может быть пустым");
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("Пароль не может быть пустым");

            // validate password for length and complexity.

            Surname = surname;
            Name = name;
            Patronymic = patronymic;
            PhoneNumber = phone;
            EMail = email;
            Adress = adress;
            Password = password;
            Basket = new List<Tuple<int, Product>>();
            Orders = new List<Order>();
        }

        /// <summary>
        /// Проверка email.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        private static bool IsValidEmail(string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email))
                    return false;

                return new EmailAddressAttribute().IsValid(email);
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// <param name="amount"></param>
        public void AddToBasket(Product product, int amount)
        {
            if (product.Count <= amount)
            {
                amount = product.Count;
                // Оповестить что количество меньше, чем хочется.
            }
            Basket.Add(new Tuple<int, Product>(amount, product));
        }
    }
}
