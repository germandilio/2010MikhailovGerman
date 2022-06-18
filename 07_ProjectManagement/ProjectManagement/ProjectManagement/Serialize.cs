using System;
using System.Collections.Generic;
using System.IO;
using ProjectLib;

namespace ProjectManagement
{
    /// <summary>
    /// В этом файле представлена реализация Serializeble.
    /// </summary>
    partial class MainClass
    {
        private static string projectsFilePath = "projects.bin";
        private static string usersFilePath = "users.bin";

        /// <summary>
        /// Записывает информацию в файл.
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="objectToWrite"></param>
        /// <param name="append"></param>
        public static void WriteToBinaryFile()
        {
            using (var file = new FileStream(projectsFilePath, FileMode.OpenOrCreate))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                if (projectsPool != null)
                    binaryFormatter.Serialize(file, projectsPool);
            }
            using (var file = new FileStream(usersFilePath, FileMode.OpenOrCreate))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                if (usersPool != null)
                    binaryFormatter.Serialize(file, usersPool);
            }
        }

        /// <summary>
        /// Считывает информацию с файла.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static void ReadFromBinaryFile()
        {
            using (var file = new FileStream(projectsFilePath, FileMode.OpenOrCreate))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                var projects = binaryFormatter.Deserialize(file) as List<Project>;

                if (projects is not null)
                {
                    projectsPool = projects;
                }

            }
            using (var file = new FileStream(usersFilePath, FileMode.OpenOrCreate))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                var users = binaryFormatter.Deserialize(file) as List<User>;

                if (users is not null)
                {
                    usersPool = users;
                }
            }
        }
    }
}
