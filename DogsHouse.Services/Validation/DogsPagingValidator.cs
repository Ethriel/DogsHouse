using DogsHouse.Services.DataPresentation;
using FluentValidation;

namespace DogsHouse.Services.Validation
{
    public class DogsPagingValidator : AbstractValidator<DogsPagination>
    {
        public DogsPagingValidator()
        {
            RuleFor(pagination => pagination.Page).NotEmpty();
            RuleFor(pagination => pagination.PageSize).NotEmpty();
        }
    }
}
