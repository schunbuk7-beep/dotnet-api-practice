using Microsoft.EntityFrameworkCore;
using DotnetApiPractice.Data;
using DotnetApiPractice.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<StudentDbContext>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<StudentService>();

var app = builder.Build();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();
