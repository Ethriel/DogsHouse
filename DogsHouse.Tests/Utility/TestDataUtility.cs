using DogsHouse.Database.Model;
using DogsHouse.Services.Model;

namespace DogsHouse.Tests.Utility
{
    public static class TestDataUtility
    {
        public static List<Dog> GetTestDogs()
        {
            return new List<Dog>()
            {
                new Dog
                {
                    Id = 1,
                    Color = "black&white",
                    Name = "Jessy",
                    TailLength = 10,
                    Weight = 15
                },
                new Dog
                {
                    Id = 2,
                    Color = "brown&tan",
                    Name = "Max",
                    TailLength = 8,
                    Weight = 16
                },
                new Dog
                {
                    Id = 1,
                    Color = "red",
                    Name = "Molly",
                    TailLength = 12,
                    Weight = 18
                }
            };
        }

        public static List<DogDTO> GetTestDogDTOs()
        {
            return new List<DogDTO>()
            {
                new DogDTO
                {
                    Id = 1,
                    Color = "black&white",
                    Name = "Jessy",
                    TailLength = 10,
                    Weight = 15
                },
                new DogDTO
                {
                    Id = 2,
                    Color = "brown&tan",
                    Name = "Max",
                    TailLength = 8,
                    Weight = 16
                },
                new DogDTO
                {
                    Id = 1,
                    Color = "red",
                    Name = "Molly",
                    TailLength = 12,
                    Weight = 18
                }
            };
        }

        public static Dog GetTestDog(int id = 4, string name = "test", string color = "white", int tailLength = 10, int weight = 15)
        {
            return new Dog
            {
                Id = id,
                Name = name,
                Color = color,
                TailLength = tailLength,
                Weight = weight
            };
        }

        public static DogDTO GetTestDogDTO(int id = 4, string name = "test", string color = "white", int tailLength = 10, int weight = 15)
        {
            return new DogDTO
            {
                Id = id,
                Name = name,
                Color = color,
                TailLength = tailLength,
                Weight = weight
            };
        }
    }
}
