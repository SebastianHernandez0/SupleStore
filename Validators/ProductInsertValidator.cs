using FluentValidation;
using SupleStore.DTOs;

namespace SupleStore.Validators
{
    public class ProductInsertValidator: AbstractValidator<ProductoInsertDto>
    {
        public  ProductInsertValidator()
        {
            RuleFor(x=>x.Name).NotEmpty().WithMessage("El nombre es requerido");
            RuleFor(x => x.Image).NotEmpty().WithMessage("La imagen es requerida");
            RuleFor(x => x.precio).NotEmpty().WithMessage("El precio es requerido");
            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("La categoria es requerida");
        }
    }
}
