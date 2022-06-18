using System;
using System.Collections.Generic;

namespace Storage
{
    [Serializable]
    class Storage
    {
        /// <summary>
        /// Текущий склад.
        /// </summary>
        public Storage()
        {
            AllTreeSections = new List<Section>();
            Allproducts = new List<Product>();
            Clients = new List<Client>();
            AllOrders = new List<Order>();
        }
        /// <summary>
        /// Все секции TreeView.
        /// </summary>
        public List<Section> AllTreeSections;
        /// <summary>
        /// Все продукты.
        /// </summary>
        public List<Product> Allproducts { get; set; }
        /// <summary>
        /// Все клиенты.
        /// </summary>
        public List<Client> Clients { get; set; }
        /// <summary>
        /// Все заказы.
        /// </summary>
        public List<Order> AllOrders { get; set; }
        /// <summary>
        /// Проерка существования клиента.
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public bool CleintAlreadyExist(string login)
        {
            if (Clients == null)
            {
                Clients = new List<Client>();
                return false;
            }

            if (Clients.Exists(client => client.EMail == login))
                return true;
            else
                return false;

        }
    }
}
