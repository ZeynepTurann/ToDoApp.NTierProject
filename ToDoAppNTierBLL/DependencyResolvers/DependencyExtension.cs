using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ToDoAppNTierBLL.AutoMapperMapping;
using ToDoAppNTierBLL.Interfaces;
using ToDoAppNTierBLL.Services;
using ToDoAppNTierBLL.ValidationRules;
using ToDoAppNTierDataAccess.Contexts;
using ToDoAppNTierDataAccess.UnitOfWork;
using ToDoAppNTierDTO;


namespace ToDoAppNTierBLL.DependencyResolvers
{
    public static class DependencyExtension
    {
        /// <summary>
        /// it can be added this middleware into Startup by writing DependencyExtension for IServiceCollection
        /// </summary>
        /// <param name="services"></param>
        public static void AddDependencies(this IServiceCollection services)
        { 
            services.AddDbContext<ToDoContext>(opt =>
            {
                opt.UseSqlServer("server =.\\SQLEXPRESS; database=ToDoAppProjectDB ; integrated security = true;");
            });

            var configuration = new MapperConfiguration(opt =>
            {
                opt.AddProfile(new ToDoProfile());
                opt.AddProfile(new UserProfile());
            });

            var mapper = configuration.CreateMapper();
            services.AddSingleton(mapper);
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IToDoService, ToDoService>();
            services.AddScoped<IUserService, UserService>();
            services.AddTransient<IValidator<ToDoCreateDto>, ToDoCreateDtoValidator>();
            services.AddTransient<IValidator<ToDoUpdateDto>, ToDoUpdateDtoValidator>();
            services.AddTransient<IValidator<UserRegisterDto>, UserRegisterDtoValidator>();
            services.AddTransient<IValidator<UserEditDto>, UserEditDtoValidator>();


        }

    }
}
