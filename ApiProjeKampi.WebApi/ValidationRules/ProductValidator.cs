using ApiProjeKampi.WebApi.Entities;
using FluentValidation;

namespace ApiProjeKampi.WebApi.ValidationRules
{
    public class ProductValidator:AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(x => x.ProductName).NotEmpty().WithMessage(" Ürün Adını Boş Geçmeyin");
            RuleFor(x => x.ProductName).MinimumLength(2).WithMessage("En az 2 karakter girişi yapın!");
            RuleFor(x => x.ProductName).MaximumLength(50).WithMessage("En fazla 50 karakter girişi yapın");

            RuleFor(x => x.Price).NotEmpty().WithMessage("Ürün fiyatı boş geçilemez").LessThan(1000).WithMessage("Ürün Fiyatı Bu Kadar Yüksek Olamaz").GreaterThan(0).WithMessage("Ürün Fiyatı Negatif Olamaz");

            RuleFor(x => x.ProductDescription).NotEmpty().WithMessage("Ürün açıklaması boş geçilemez!");

        }
    }
}
