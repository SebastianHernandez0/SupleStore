using FluentValidation;
using SupleStore.DTOs;

namespace SupleStore.Validators
{
    public class ProductUpdateValidator: AbstractValidator<ProductoUpdateDto>
    {
        public ProductUpdateValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("El id es requerido");
            RuleFor(x => x.Name).NotEmpty().WithMessage("El nombre es requerido");
            RuleFor(x => x.Image).NotEmpty().WithMessage("La imagen es requerida");
            RuleFor(x => x.precio).NotEmpty().WithMessage("El precio es requerido");
            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("La categoria es requerida");
        }
    }
}
