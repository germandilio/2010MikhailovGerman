using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage
{
    [Serializable]
    class Product
    {
        public string FullTreePath { get => path; set { path = value; } }
        public string ID { get => id; set { id = value; } }
        public string Name { get => name; set { name = value; } }
        public int Count
        {
            get => count; set
            {
                if (count < 0)
                    throw new ArgumentException("Amount of products cannot be less than 0");
                count = value;
            }
        }

        public double Price { get => price; set
            {
                if (price < 0.0)
                    throw new ArgumentException("Price cannot be less than 0");
                price = value;
            }
        }

        public string Description { get => description; set { description = value; } }

        [JsonConverter(typeof(ImageConverter))]
        public Image Image { get; set; }


        private string id;
        private double price;
        private string name;
        private int count;
        private string description;
        private string path;

        /// <summary>
        /// Продукт с своими характеристиками.
        /// </summary>
        /// <param name="fullTreePath"></param>
        /// <param name="name"></param>
        /// <param name="ID"></param>
        /// <param name="price"></param>
        /// <param name="count"></param>
        /// <param name="description"></param>
        /// <param name="image"></param>
        public Product(string fullTreePath,string name, string ID, double price, int count, string description, Image image)
        {   
            FullTreePath = fullTreePath;
            Name = name;
            this.ID = ID;
            Price = price;
            Count = count;
            Description = description;
            // Фотки в кодировке base64 (для сериализации).
            this.Image = image;
        }

        public void ChangeFullPaths(string oldNodePath, string changesInPath)
        {
            FullTreePath = FullTreePath.Replace(oldNodePath, changesInPath);
        }
    }
}
