using System;

namespace Indexers
{
    interface IInventory
    {
        Product this[int id, string blegh] { get; set; }
    }
    class Product
    {

    }
    class Inventory : IInventory
    {
        public Product this[int id, string blegh]
        {
            get { Console.WriteLine("Getting {0}", id); return null; }
            set { Console.WriteLine("Setting {0} to {1}", id, value); }
        }

        //public Product this[double test]
        //{
        //    get { }
        //    set { }
        //}
    }
    class Program
    {
        static void Main(string[] args)
        {
            var inventory = new Inventory();
            var product = inventory[20, "whoa"];
            inventory[10, "hey"] = new Product();
        }
    }
}
