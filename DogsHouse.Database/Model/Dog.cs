using System.ComponentModel.DataAnnotations;

namespace DogsHouse.Database.Model
{
    public class Dog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public int TailLength { get; set; }
        public int Weight { get; set; }
    }
}
