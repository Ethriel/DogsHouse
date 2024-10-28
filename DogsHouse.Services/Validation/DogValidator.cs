using DogsHouse.Database.Model;
using DogsHouse.Services.Model;
using FluentValidation;

namespace DogsHouse.Services.Validation
{
    public class DogValidator : AbstractValidator<DogDTO>
    {
        public DogValidator()
        {
            RuleFor(dog => dog.Name).NotEmpty().Length(3, 30);
            RuleFor(dog => dog.Color).NotEmpty().Length(3, 30);

            Action<int, ValidationContext<DogDTO>> numberValidation = (value, context) =>
            {
                var propName = context.PropertyPath;
                if (value < 0 || propName == nameof(DogDTO.Weight) && value <= 0)
                {
                    context.AddFailure($"{propName} should be greater than zero!");
                }
                else if (propName == nameof(Dog.TailLength) && !(value >= 0))
                {
                    context.AddFailure($"{propName} should be greater or equal to zero!");
                }
            };

            RuleFor(dog => dog.TailLength).Custom(numberValidation);
            RuleFor(dog => dog.Weight).Custom(numberValidation);
        }
    }
}
