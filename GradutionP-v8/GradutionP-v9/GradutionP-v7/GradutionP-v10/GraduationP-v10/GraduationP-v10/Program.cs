using GradutionP.Services;
using GradutionP.Data;
using Microsoft.EntityFrameworkCore;
using GradutionP.Interface;
using GradutionP.Service;
using System;

namespace GradutionP
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ? 1?? ????? ??????? ?????? ????????
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // ? 2?? ????? ??????? (Dependency Injection)
            builder.Services.AddScoped<IFavoriteService, FavoriteService>();
            builder.Services.AddScoped<IPatientService, PatientService>();
            builder.Services.AddScoped<IDoctorService, DoctorService>();
            builder.Services.AddScoped<IFeedbackService, FeedbackService>();


            // ? 3?? ????? ??? CORS (??? ?? ???? Frontend ???? ???? API)
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            // ? 4?? ????? ??? Controllers ? Swagger
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // ? 5?? ????? Swagger ??? ????? ???????
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // ? 6?? ????? HTTPS
            app.UseHttpsRedirection();

            // ? 7?? ????? CORS
            app.UseCors("AllowAll");

            // ? 8?? ??? ??? Routing & Authorization
            app.UseRouting();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
