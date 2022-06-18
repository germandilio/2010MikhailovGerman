using System;
namespace Vegetables_Storage
{
    public class Box
    {
        internal double BoxPrice { get; set; }
        internal double BoxWeight { get; set; }
        internal string Info { get; set; }

        public Box(double priceBox, double weight, string info)
        {
            BoxPrice = priceBox;
            BoxWeight = weight;
            Info = info;
        }

        /// <summary>
        /// Method print info about box.
        /// </summary>
        public void ShowBoxInfo()
        {
            Console.WriteLine($" Price: {BoxPrice:C1} Weight {BoxWeight:F3} Info: {Info}");
        }

        /// <summary>
        /// Method print info about box.
        /// </summary>
        public string ShowBoxInfoFile()
        {
            string result = $" Price: {BoxPrice:C1} Weight {BoxWeight:F3} Info: {Info} {Environment.NewLine}";
            return result;
        }
    }
}
