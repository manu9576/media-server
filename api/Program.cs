using System.Reflection;
using Microsoft.OpenApi.Models;

internal class Program
{
	private static void Main(string[] args)
	{
		WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

		// Add services to the container.
		// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen();
		builder.Services.AddControllersWithViews();

		builder.Services.AddSwaggerGen(c =>
		{
			c.SwaggerDoc("v1", new OpenApiInfo
			{
				Title = "Media API",
				Version = "v1",
				Description = "Get the media"
			});

			// Path to the XML documentation file
			string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
			string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
			c.IncludeXmlComments(xmlPath);
		});

		WebApplication app = builder.Build();

		// Configure the HTTP request pipeline.
		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.DocumentTitle = "Media API - Swagger docs";
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Address API v1");
				c.EnableDeepLinking();
				c.DefaultModelsExpandDepth(0);
			});
		}
		
		app.UseHttpsRedirection();
		
		app	.MapControllers()
			.WithOpenApi();

		app.Run();
	}
}