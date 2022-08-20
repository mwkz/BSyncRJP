using BS.Accounts.Core.DTO;
using BS.Accounts.Core.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.Accounts.Infrastructure.Validators
{
    public class AddAccountRequestValidator : AbstractValidator<AddAccountRequest>
    {        
        public AddAccountRequestValidator(IAccountsUnitOfWork accountsUnitOfWork)         
        {
            RuleFor(a => a.AccountNo).NotEmpty()
                   .WithMessage("Account no cannot be empty");

            RuleFor(a => a.Name).NotEmpty()
                .WithMessage("Account name cannot be empty.");

            RuleFor(a => a.CustomerId).NotEqual(0)
                .WithMessage("Valid customer id is required.");

            RuleFor(a => a.AccountNo).MustAsync(async (no, token) => await accountsUnitOfWork.Accounts.AccountExists(no, token))
                .WithMessage("Account already exists.");

        }
    }
}
