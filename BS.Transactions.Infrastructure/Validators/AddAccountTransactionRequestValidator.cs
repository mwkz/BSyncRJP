using BS.Transactions.Core.DTO;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Transactions.Infrastructure.Validators
{
    public class AddAccountTransactionRequestValidator : AbstractValidator<AddAccountTransactionRequest>
    {
        public AddAccountTransactionRequestValidator()
        {
            RuleFor(r => r.Value).NotEqual(0).WithMessage("Transaction balanace must different than 0.");
        }
    }
}
