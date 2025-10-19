
using Commander.Data;
using Commander.Profiles;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;

namespace Commander
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            var connectionString = builder.Configuration.GetConnectionString("PostgreSqlConnection");
            builder.Services.AddDbContext<CommanderContext>(opt => opt.UseNpgsql(connectionString));

            builder.Services.AddControllers().AddNewtonsoftJson(s =>
            {
                s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            
            builder.Services.AddScoped<ICommanderRepo, SqlCommanderRepo>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseMiddleware<ValidationMappingMiddleware>();
            app.MapControllers();

            app.Run();
        }
    }
}
