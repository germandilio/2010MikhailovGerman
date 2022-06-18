using System;
using System.Collections.Generic;

namespace Storage
{
    [Serializable]
    class Order
    {
        /// <summary>
        /// Список продуктов в заказе.
        /// </summary>
        public List<Product> products;
        /// <summary>
        /// ID заказа.
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Итоговая цена заказа.
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// Дата заказа.
        /// </summary>
        public DateTime OrderDate { get; set; }
        /// <summary>
        /// Клиент, сделавший заказ.
        /// </summary>
        public Client Client { get; set; }
        /// <summary>
        /// Статус заказа.
        /// </summary>
        public Status Status { get; set; }

        public Order(List<Product> products, double price, Client client)
        {
            if (products == null)
                throw new ArgumentException("Путой список товаров");
            if (price < 0)
                throw new ArgumentException("Итог не может быть меньше нуля");
            if (client == null)
                throw new ArgumentException("Отсутствует клиент");

            this.products = products;
            Price = price;
            Client = client;

            Random random = new Random();
            ID = random.Next(100000, 1000000);
            OrderDate = DateTime.Now;

            Status = Status.Default;
        }
    }
}
