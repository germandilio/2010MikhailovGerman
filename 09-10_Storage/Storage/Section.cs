using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage
{
    [Serializable]
    class Section
    {
        /// <summary>
        /// Секция = подраздел в классификаторе (только в бэкенде).
        /// </summary>
        /// <param name="name"></param>
        public Section(string name)
        {
            Name = name;
            childSections = new List<Section>();
        }

        private string name;
        public string Name { get => name; set { name = value; } }

        public List<Section> childSections;


    }
}
