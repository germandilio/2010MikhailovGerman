using CsvHelper.Configuration.Attributes;
using System;

namespace Storage
{
    class CSVProduct
    {
        /// <summary>
        /// Объекты с упрощенными данными для CSV-отчета (CSVHealper).
        /// </summary>
        /// <param name="fullpath"></param>
        /// <param name="article"></param>
        /// <param name="name"></param>
        /// <param name="count"></param>
        public CSVProduct(string fullpath, string article, string name, string count)
        {
            fullTreePath = fullpath.Replace(@"\\", @"\");
            id = article;
            this.name = name;
            this.count = count;

        }

        [Name("Путь классификатора")]
        [Index(0)]
        public string fullTreePath;

        [Name("Артикул")]
        [Index(1)]
        public string id;

        [Name("Имя товара")]
        [Index(2)]
        public string name;

        [Name("Количество")]
        [Index(3)]
        public string count;
    }
}
