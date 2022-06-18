using CsvHelper.Configuration;

namespace Storage
{
    sealed class ObjectMap : ClassMap<CSVProduct>
    {
        /// <summary>
        /// Mapping для CSV файла: какие поля как называются и какими по счету идут (CSVHealper).
        /// </summary>
        public ObjectMap()
        {
            Map(m => m.fullTreePath).Name("Classifier path").Index(0);
            Map(m => m.id).Name("Vendor code").Index(1);
            Map(m => m.name).Name("Name").Index(2);
            Map(m => m.count).Name("Amount").Index(3);
        }
    }
}
