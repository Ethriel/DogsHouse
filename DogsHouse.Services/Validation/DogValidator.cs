using DogsHouse.Database.Model;
using FluentValidation;

namespace DogsHouse.Services.Validation
{
    public class DogValidator : AbstractValidator<Dog>
    {
        public DogValidator()
        {
            RuleFor(dog => dog.Name).NotEmpty().Length(3, 10);
            RuleFor(dog => dog.Color).NotEmpty().Length(3, 10);

            Action<int, ValidationContext<Dog>> numberValidation = (x, context) =>
            {
                var propName = context.PropertyPath;
                if (x < 0 || propName == nameof(Dog.Weight) && x <= 0)
                {
                    context.AddFailure($"{propName} should be greater than zero!");
                }
                else if (propName == nameof(Dog.TailLength) && !(x >= 0))
                {
                    context.AddFailure($"{propName} should be greater or equal to zero!");
                }
            };

            RuleFor(dog => dog.TailLength).Custom(numberValidation);
            RuleFor(dog => dog.Weight).Custom(numberValidation);
        }
    }
}
