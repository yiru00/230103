using EmployeeService.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeService
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the DI container.
			
			var NorthwindconnectionString = builder.Configuration.GetConnectionString("Northwind") ?? throw new InvalidOperationException("Connection string 'Northwind' not found.");
			builder.Services.AddDbContext<NorthwindContext>(options =>options.UseSqlServer(NorthwindconnectionString));

			//設定策略
			builder.Services.AddControllers();
			string MyAllowOrigins = "AllowAny";
			builder.Services.AddCors(options =>
			{
				options.AddPolicy(
					name: MyAllowOrigins,
					policy => policy.WithOrigins("*").WithMethods("*"));
			});
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			var app = builder.Build();

			

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			//不傳入參數=>個別啟用cors策略
			app.UseCors();

			app.UseAuthorization();

			//	var summaries = new[]
			//	{
			//	"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
			//};

			//	app.MapGet("/weatherforecast", (HttpContext httpContext) =>
			//	{
			//		var forecast = Enumerable.Range(1, 5).Select(index =>
			//			new WeatherForecast
			//			{
			//				Date = DateTime.Now.AddDays(index),
			//				TemperatureC = Random.Shared.Next(-20, 55),
			//				Summary = summaries[Random.Shared.Next(summaries.Length)]
			//			})
			//			.ToArray();
			//		return forecast;
			//	})
			//	.WithName("GetWeatherForecast");
			app.MapControllers();
			app.Run();
		}
	}
}