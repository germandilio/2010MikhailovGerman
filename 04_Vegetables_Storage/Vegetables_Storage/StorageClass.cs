using System;
using System.Collections.Generic;
using System.IO;

namespace Vegetables_Storage
{
    public class Storage
    {
        protected List<Container> storage;

        internal int MaxSpace { get; set; }
        internal protected double PriceStorage { get; set; }

        /// <summary>
        /// Constructor of the class.
        /// </summary>
        /// <param name="maxSpace"></param>
        /// <param name="priceStorage"></param>
        public Storage(int maxSpace, double priceStorage)
        {
            PriceStorage = priceStorage;
            MaxSpace = maxSpace;
            storage = new List<Container>();
        }

        /// <summary>
        /// Method add container to the storage if it's cost-effective.
        /// </summary>
        public void AddContainer()
        {
            int numberOfBoxexInContainer;
            double[] prices, weights;
            string[] info;
            GetInformationAboutBoxes(out numberOfBoxexInContainer, out prices, out weights, out info);

            Container container = new Container(numberOfBoxexInContainer, prices, weights, info);
            if (container.GetDamage() > PriceStorage)
                if (storage.Count < MaxSpace)
                    storage.Add(container);
                else
                {
                    for (int i = 0; i < storage.Count - 1; i++)
                    {
                        storage[i] = storage[i + 1];
                    }
                    storage[^1] = container;
                }
        }

        /// <summary>
        /// Method add container to the storage if it's cost-effective (from file).
        /// </summary>
        /// <param name="numberOfBoxexInContainer"></param>
        /// <param name="prices"></param>
        /// <param name="weights"></param>
        public void AddContainer(int numberOfBoxexInContainer, double[] prices , double[] weights, string[] info)
        {
            Container container = new Container(numberOfBoxexInContainer, prices, weights, info);

            if (container.GetDamage() > PriceStorage)
                if (storage.Count < MaxSpace)
                    storage.Add(container);
                else
                {
                    for (int i = 0; i < storage.Count - 1; i++)
                    {
                        storage[i] = storage[i + 1];
                    }
                    // Last index of the List.
                    storage[^1] = container;
                }
            else
                Console.WriteLine("The container was not added due to unprofitability.");
        }

        /// <summary>
        /// Method handle users input.
        /// </summary>
        /// <param name="numberOfBoxexInContainer"></param>
        /// <param name="prices"></param>
        /// <param name="weights"></param>
        private static void GetInformationAboutBoxes(out int numberOfBoxexInContainer, out double[] prices, out double[] weights, out string[] info)
        {
            Console.WriteLine("Enter quantity of boxes");

            if (!int.TryParse(Console.ReadLine(), out numberOfBoxexInContainer) || numberOfBoxexInContainer < 1)
                throw new FormatException("Incorrect quantity of boxes");

            double boxWeight, boxPrice;
            string infos;
            prices = new double[numberOfBoxexInContainer];
            weights = new double[numberOfBoxexInContainer];
            info = new string[numberOfBoxexInContainer];

            for (int i = 0; i < numberOfBoxexInContainer; i++)
            {
                Container.GetParametersOfBox(out boxWeight, out boxPrice,out infos, i + 1);
                info[i] = infos;
                prices[i] = boxPrice;
                weights[i] = boxWeight;
            }
        }

        /// <summary>
        /// Method remove the container by it's index.
        /// </summary>
        /// <param name="index"></param>
        public void RemoveContainer(int index)
        {
            // Сheck for correctness of the index.
            if (index < storage.Count && index >= 0)
            {
                storage.RemoveAt(index);
            }
        }

        /// <summary>
        /// Method return the capacity of storage.
        /// </summary>
        /// <returns>Capasity.</returns>
        public int GetCapacity() => storage.Count;

        /// <summary>
        /// Method return info of vagetable storage.
        /// </summary>
        public void ShowInfo()
        {
            Console.Clear();
            Console.WriteLine($"{Environment.NewLine}VEGETABLES STORAGE:{Environment.NewLine} Max capacity: {MaxSpace} {Environment.NewLine} Сontainer storage cost: ${PriceStorage:F1} {Environment.NewLine}");
            for (int i = 0; i < storage.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write($"\tСontainer {i + 1}:");
                Console.ResetColor();
                storage[i].ShowContainerInfo(i);
            }
        }

        /// <summary>
        /// Method return info of vagetable storage.
        /// </summary>
        public void ShowInfo(string outputFileName)
        {
            try
            {
                string result = $"{Environment.NewLine}VEGETABLES STORAGE:{Environment.NewLine} Max capacity: {MaxSpace} {Environment.NewLine} Сontainer storage cost: ${PriceStorage:F1} {Environment.NewLine}";
                for (int i = 0; i < storage.Count; i++)
                {
                    result += $"\tСontainer {i + 1}:";
                    result += storage[i].ShowContainerInfoFile(i);
                }
                File.WriteAllText(outputFileName, result);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
