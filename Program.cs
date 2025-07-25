using Microsoft.EntityFrameworkCore;
using WebApplicationBlueCollar.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// ✅ Configure EF Core with SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ✅ Add CORS policy here
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

var app = builder.Build();

// Use Swagger
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // 
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ✅ Use CORS middleware
app.UseCors("AllowReactApp");

app.UseAuthorization();

app.MapControllers();

app.Run();

