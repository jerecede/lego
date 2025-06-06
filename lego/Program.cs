using lego.Logic;
using lego.model;

namespace lego
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var context = new LegoContext();
            var colorRepo = new LegoRepository<LegoColor>(context);
            var colors = colorRepo.GetAll();
            foreach (var color in colors)
            {
                Console.WriteLine($"Id: {color.Id}, Name: {color.Name}, RGB: {color.Rgb}, IsTrans: {color.IsTrans}");
            }
            var inventoryRepo = new LegoRepository<LegoInventory>(context);
            var inventories = inventoryRepo.GetAll();
            foreach (var inventory in inventories)
            {
                Console.WriteLine($"Inventory Id: {inventory.Id}, Name: {inventory.Version}");
            }

        }
    }
}
