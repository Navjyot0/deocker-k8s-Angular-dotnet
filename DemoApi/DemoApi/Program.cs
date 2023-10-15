namespace DemoApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddCors();
            //builder.Services.AddCors(options =>
            //{
            //    options.AddPolicy("AllowAngularDevClient",
            //      builder =>
            //      {
            //          builder
            //          .WithOrigins("http://localhost:4200")
            //          .AllowAnyHeader()
            //          .AllowAnyMethod();
            //      });
            //    options.AddPolicy("AllowAngularClient",
            //      builder =>
            //      {
            //          builder
            //          .WithOrigins("http://localhost")
            //          .AllowAnyHeader()
            //          .AllowAnyMethod();
            //      });
            //});

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseCors(builder =>
            {
                builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            });
            //app.UseCors("AllowAngularDevClient");
            //app.UseCors("AllowAngularClient");

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}