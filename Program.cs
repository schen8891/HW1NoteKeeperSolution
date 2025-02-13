namespace HW1NoteKeeper
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen();  // Add Swagger generation service

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();  // Enable Swagger to generate API documentation
                app.UseSwaggerUI(c =>
                {
                    // Update Swagger documentation URL to use Azure's URL
                    c.SwaggerEndpoint("https://hw1notekeeperapp-ftcsd0h8axfsbgcc.eastus-01.azurewebsites.net/swagger/v1/swagger.json", "HW1NoteKeeper API V1");
                    c.RoutePrefix = string.Empty;  // Set Swagger UI as the homepage
                });
            }

            app.UseHttpsRedirection();  // Redirect HTTP requests to HTTPS
            app.UseAuthorization();  // Enable authorization
            app.MapControllers();  // Map API controllers

            app.Run();  // Run the application
        }
    }
}