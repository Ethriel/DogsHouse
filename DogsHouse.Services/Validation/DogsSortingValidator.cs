using DogsHouse.Services.DataPresentation;
using DogsHouse.Services.Model;
using FluentValidation;

namespace DogsHouse.Services.Validation
{
    public class DogsSortingValidator : AbstractValidator<DogsSortingFilter>
    {
        public DogsSortingValidator()
        {
            var dogProps = typeof(DogDTO).GetProperties();

            Action<string, ValidationContext<DogsSortingFilter>> validateAttributes = (value, context) =>
            {
                var attrName = context.PropertyPath.ToLower();

                if (!dogProps.Any(prop => prop.Name.Equals(value, StringComparison.CurrentCultureIgnoreCase)))
                {
                    context.AddFailure($"Attribute {attrName} is not valid. Dog does not have attribute named {value}!");
                }
            };

            var validOrders = new string[] { "Asc", "Desc" };

            Action<string, ValidationContext<DogsSortingFilter>> validateOrder = (value, context) =>
            {
                var attrName = context.PropertyPath.ToLower();

                if (!validOrders.Any(order => order.Equals(value, StringComparison.CurrentCultureIgnoreCase)))
                {
                    context.AddFailure($"Attribute {attrName} is not valid. {value} is not a valid sorting order!");
                }
            };

            RuleFor(filter => filter.Attribute).NotEmpty().Length(3, 10).Custom(validateAttributes);
            RuleFor(filter => filter.Order).NotEmpty().Length(3, 4).Custom(validateOrder);

        }
    }
}
