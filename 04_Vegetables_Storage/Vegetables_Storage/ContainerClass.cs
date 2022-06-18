using System;
using System.Collections.Generic;

namespace Vegetables_Storage
{
    public class Container
    {
        protected List<Box> container = new List<Box>();

        protected static readonly Random random1 = new Random();

        private protected const int maxRandom = 1000;
        private protected const int minRandom = 50;

        protected double ContainerPrice { get; set; }
        protected double ContainerWeight { get; set; }

        private protected double MaxWeight = random1.NextDouble() * (maxRandom - minRandom) + minRandom;

        /// <summary>
        /// Property of Container.
        /// </summary>
        /// <returns></returns>
        internal double GetDamage()
        {
            Random random2 = new Random();
            var damage = 0.5 * random2.NextDouble();
            RecalculatePriceOfTheContainer(damage);
            return ContainerPrice;

        }

        /// <summary>
        /// Method calculate price after damaging.
        /// </summary>
        /// <param name="damage"></param>
        private void RecalculatePriceOfTheContainer(double damage)
        {
            ContainerPrice = 0.0;
            foreach (var item in container)
            {
                item.BoxPrice -= item.BoxPrice * damage;
                ContainerPrice += item.BoxPrice;
            }
        }

        /// <summary>
        /// Method handle input parameters.
        /// </summary>
        /// <param name="boxWeight"></param>
        /// <param name="boxPrice"></param>
        /// <param name="numberOfBox"></param>
        internal static void GetParametersOfBox(out double boxWeight, out double boxPrice, out string info, int numberOfBox)
        {
            Console.WriteLine($"Enter weight of the box {numberOfBox}:");
            if (!double.TryParse(Console.ReadLine(), out boxWeight) || boxWeight <= 0.0)
                throw new FormatException("Incorrect weight of box");

            Console.WriteLine($"Enter price of the box {numberOfBox}:");
            if (!double.TryParse(Console.ReadLine(), out boxPrice) || boxPrice < 0.0)
                throw new FormatException("Incorrect price of box");

            Console.WriteLine($"Enter info of the box {numberOfBox}:");
            info  = Console.ReadLine();
        }

        /// <summary>
        /// Method print info about container.
        /// </summary>
        /// <param name="index"></param>
        public void ShowContainerInfo(int index)
        {
            Console.WriteLine($" Price: {ContainerPrice:C1} Weight: {ContainerWeight:F3} {Environment.NewLine}");
            for (int i = 0; i < container.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"\t\tBox {i + 1}:");
                Console.ResetColor();
                container[i].ShowBoxInfo();
            }

        }

        /// <summary>
        /// Method print info about container.
        /// </summary>
        /// <param name="index"></param>
        public string ShowContainerInfoFile(int index)
        {
            string result = $" Price: {ContainerPrice:C1} Weight: {ContainerWeight:F3} {Environment.NewLine}";
            for (int i = 0; i < container.Count; i++)
            {
                result += $"\t\tBox {i + 1}:";
                result += container[i].ShowBoxInfoFile();
            }
            return result;

        }

        /// <summary>
        /// Constructor of the class.
        /// </summary>
        /// <param name="count"></param>
        /// <param name="prices"></param>
        /// <param name="weights"></param>
        public Container(int count, double[] prices, double[] weights, string[] info)
        {
            ContainerWeight = 0.0;
            ContainerPrice = 0.0;
            double maxContainerWeight = MaxWeight;
            for (int i = 0; i < count; i++)
            {
                Box box = new Box(prices[i], weights[i], info[i]);

                if (ContainerWeight + box.BoxWeight > maxContainerWeight)
                    continue;
                else
                {   
                    container.Add(box);
                    ContainerPrice += prices[i];
                    ContainerWeight += weights[i];
                }
            }
        }
    }
}
