using lego.model;

namespace lego
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var context = new LegoContext();
            var parts = context.LegoParts.ToList();
            foreach (var part in parts)
            {
                Console.WriteLine($"Part ID: {part.PartNum}, Name: {part.Name}");
            }
        }
    }
}
