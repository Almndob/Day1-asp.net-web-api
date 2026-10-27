using Microsoft.EntityFrameworkCore;
using Day1.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        Title = "School Management",
        Description = "API to manage School Students, Courses, and Enrollments",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "BN-ALMNDOB",
            Email = "alsabry615@gmail.com"
        }
    });
    options.IncludeXmlComments("F:\\API-C#\\Day1\\Day1\\mydoc.xml");
});

// Register DbContext
builder.Services.AddDbContext<SchoolContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("SchoolContext")
    )
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
