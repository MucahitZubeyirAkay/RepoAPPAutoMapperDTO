using FluentValidation;
using MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.UserName).NotNull().NotEmpty().WithMessage("Kullanıcı adı boş geçilemez!");
            RuleFor(x => x.UserName).MinimumLength(5).MaximumLength(50).WithMessage("Kullanıcı adı 5-50 karakter arasında olmalı!");


            RuleFor(x => x.UserPassword).NotNull().WithMessage("Kullanıcı şifresi boş geçilemez!");
            RuleFor(x => x.UserPassword).MinimumLength(4).MaximumLength(25).WithMessage("Kullanıcı şifresi 4-25 karakter arasında olmal!");

            RuleFor(x => x.Role).MaximumLength(25).WithMessage("Rol 25 karakterden fazla olamaz");

        }
    }
}
