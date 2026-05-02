using FluentValidation;
using OnlineMarket.SharedKernel.Contracts.Contracts.Requests.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.Application.Validators.Product
{
    public class UpdateProductValidator : AbstractValidator<UpdateProductRequest>
    {
        public UpdateProductValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .Matches(@"^[\p{L}\p{N}\s\-\(\)\/\.,&'""]+$").WithMessage("Name must be valid")
                .MaximumLength(100).WithMessage("Name must be under 100 symbols");

            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("Description must be under 1000 symbols");

            RuleFor(x => x.Category)
                .IsInEnum().WithMessage("Invalid category");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0")
                .PrecisionScale(10, 2, false).WithMessage("Max 2 decimal places");

            RuleFor(x => x.Photo)
                .Must(file => file!.Length <= 5 * 1024 * 1024).WithMessage("File size must be under 5MB")
                .Must(file =>
                {
                    var allowed = new[] { ".jpg", ".jpeg", ".png" };
                    var ext = Path.GetExtension(file!.FileName).ToLower();
                    return allowed.Contains(ext);
                }).WithMessage("Allowed extensions: .jpg, .jpeg, .png");
        }
    }
}
