using ApplicationLayer.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Validators
{
    public class MoneyTransferValidator : AbstractValidator<TransferDTO>
    {
        public MoneyTransferValidator()
        {
            RuleFor(x => x.FromAccountName)
                .NotEmpty().WithMessage("Gönderici hesap adı boş olamaz.")
                .MaximumLength(100).WithMessage("Hesap adı en fazla 100 karakter olabilir.");

            RuleFor(x => x.IBAN)
                .NotEmpty().WithMessage("Alıcı IBAN alanı boş olamaz.")
                .Length(26).WithMessage("IBAN tam olarak 26 karakter olmalıdır.")
                .Matches(@"^TR\d{24}$").WithMessage("IBAN formatı geçersiz. Örnek: TR000000000000000000000000");

            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Gönderilecek tutar 0'dan büyük olmalıdır.")
                .LessThanOrEqualTo(1_000_000).WithMessage("En fazla 1.000.000 birim para gönderilebilir.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Açıklama boş olamaz.")
                .MaximumLength(250).WithMessage("Açıklama en fazla 250 karakter olabilir.");
        }
    }
}
