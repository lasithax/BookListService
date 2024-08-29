using Bookshop_be.src.domain.Entities;
using Bookshop_be.src.infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure CORS policy to allow specific origins (such as BookDetailsService)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBookDetailsService",
        builder =>
        {
            builder.WithOrigins("https://localhost:7210", "http://localhost:5009", "http://localhost:3000")
                   .AllowAnyMethod();
        });
});

// Set base path for configuration
builder.Configuration.SetBasePath(Directory.GetCurrentDirectory() + "/src/API")
                     .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Register your DbContext here, before building the app
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Register MediatR
builder.Services.AddMediatR(typeof(Program).Assembly);

var app = builder.Build();  // Build the app after registering services

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Enable CORS with the defined policy
app.UseCors("AllowBookDetailsService");

app.UseAuthorization();
app.MapControllers();

app.Run();