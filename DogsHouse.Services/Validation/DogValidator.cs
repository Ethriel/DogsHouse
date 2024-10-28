using DogsHouse.Services.Model;
using FluentValidation;

namespace DogsHouse.Services.Validation
{
    public class DogValidator : AbstractValidator<DogDTO>
    {
        public DogValidator()
        {
            RuleFor(dog => dog.Name).NotEmpty().WithMessage("Name is required")
                                    .Length(3, 30).WithMessage("Name should be between 3 and 30 charachters");
            RuleFor(dog => dog.Color).NotEmpty().WithMessage("Color is required")
                                     .Length(3, 30).WithMessage("Color should be between 3 and 30 charachters");

            Action<int, ValidationContext<DogDTO>> numberValidation = (value, context) =>
            {
                var propName = context.PropertyPath;
                if (value < 0 || propName == nameof(DogDTO.Weight) && value <= 0)
                {
                    context.AddFailure($"{propName} should be greater than zero!");
                }
                else if (propName == nameof(DogDTO.TailLength) && !(value >= 0))
                {
                    context.AddFailure($"{propName} should be greater or equal to zero!");
                }
            };

            RuleFor(dog => dog.TailLength).Custom(numberValidation);
            RuleFor(dog => dog.Weight).Custom(numberValidation);
        }
    }
}
