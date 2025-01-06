using Blog.API.Data;
using Blog.API.Mapping;
using Blog.API.Repository;
using Blog.API.Repository.Interface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<BlogPostDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("BlogProjectConnectionString"));
});
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<IBlogPostRepository, BlogPostsRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(options =>
{
    options.AllowAnyHeader();
    options.AllowAnyMethod();
    options.AllowAnyOrigin();
});

app.UseAuthorization();

app.MapControllers();

app.Run();
