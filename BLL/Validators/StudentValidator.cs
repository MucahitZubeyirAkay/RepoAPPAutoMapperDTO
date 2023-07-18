using AutoMapper;
using DTOs;
using FluentValidation;
using MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Validators
{
    public class StudentValidator: AbstractValidator<StudentDTO>
    {

        public StudentValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Öğrenci adı boş geçilemez!").MaximumLength(50).WithMessage("Öğrenci adı 0-50 karakter arasında olmalı!");
            RuleFor(x => x.Surname).NotNull().NotEmpty().WithMessage("Öğrenci Soyadı boş geçilemez!").MaximumLength(50).WithMessage("Öğrenci adı 0-50 karakter arasında olmalı!");
            RuleFor(x => x.Age).Must(a => a >= 0).WithMessage("Öğrenci yaşı 0 dan küçük olamaz");
            //RuleFor(x => x.Age).Must((dto, a) => int.TryParse(a, out int age) && age >= 0).WithMessage("Öğrenci yaşı geçersiz veya 0'dan küçük olamaz");
            //Eğer modelde int tanımlayıp DTO da string tanımlarsak yukarıdaki dönüşüm methoduyla beraber dönüştürüp tanımlayabiliriz.

            /// istediğimi eklerim

        }
    }
}
