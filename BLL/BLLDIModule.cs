using BLL.Operation.Implementations;
using BLL.Operation.Interface;
using BLL.Validators;
using DAL.Context;
using DAL.Implementations;
using DAL.Interface.Concrete;
using DTOs;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public static class BLLDIModule
    {
        public static void Inject(IServiceCollection services, IConfiguration configuration) //, IHostEnvironment environment)
        {
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITeacherRepository, TeacherRepository>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITeacherService, TeacherService>();
            services.AddScoped<IFileRepository, FileRepository>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddJwtBearer(options =>
            //    {
            //        options.TokenValidationParameters = new TokenValidationParameters
            //        {
            //            ValidateIssuer = true,
            //            ValidateAudience = true,
            //            ValidateLifetime = true,
            //            ValidateIssuerSigningKey = true,
            //            ValidIssuer = "https://localhost:7063/",
            //            ValidAudience = "https://localhost:7063/",
            //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("BuBenimAnahtarım"))

            //    };
            //    });
            //services.AddControllers().AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<StudentValidator>());

            //services.AddValidatorsFromAssemblyContaining<UserValidator>();


            services.AddValidatorsFromAssemblyContaining<StudentValidator>();
            services.AddValidatorsFromAssemblyContaining<UserValidator>();

            services.AddDbContext<RepositoryDbContext>(options =>
            options.UseSqlServer("Server=.\\SQLEXPRESS;Database=WorkingDatabase;Trusted_Connection=True;encrypt=false;"));

        }
    }
}
