using FluentValidation;
using OnlineMarket.SharedKernel.Contracts.Contracts.Requests.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.Application.Validators.Product
{
    public class UpdateProductValidator : AbstractValidator<UpdateProductRequest>
    {
        private static readonly string[] AllowedExtensions = [".jpg", ".jpeg", ".png", ".webp"];
        private static readonly string[] AllowedContentTypes = ["image/jpeg", "image/png", "image/webp"];

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
                .Must(file => file is not null && file.Length > 0).WithMessage("File must not be empty")
                .Must(file => file is not null && file.Length <= 5 * 1024 * 1024).WithMessage("File size must be under 5MB")
                .Must(file => file is not null && AllowedExtensions.Contains(Path.GetExtension(file.FileName).ToLowerInvariant()))
                .WithMessage("Allowed extensions: .jpg, .jpeg, .png, .webp")
                .Must(file => file is not null && AllowedContentTypes.Contains(file.ContentType, StringComparer.OrdinalIgnoreCase))
                .WithMessage("Allowed content types: image/jpeg, image/png, image/webp")
                .When(x => x.Photo is not null);
        }
    }
}
