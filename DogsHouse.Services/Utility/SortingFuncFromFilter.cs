using DogsHouse.Database.Model;
using DogsHouse.Services.DataPresentation;

namespace DogsHouse.Services.Utility
{
    public static class SortingFuncFromFilter
    {
        public static Func<Dog, object> GetFunc(DogsSortingFilter filter)
        {
            //default sorting by name
            Func<Dog, object> dogsSortingFunc = dog => dog.Name;

            var dogProp = typeof(Dog).GetProperties()
                                     .FirstOrDefault(prop => prop.Name.Equals(filter.Attribute, StringComparison.CurrentCultureIgnoreCase));

            if (dogProp != null)
            {
                dogsSortingFunc = dog => dogProp.GetValue(dog, null);
            }

            return dogsSortingFunc;
        }
    }
}
