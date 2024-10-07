using BookStore.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<BookStoreDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("BookStoreDbContext"));
});

var app = builder.Build();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();



app.Run();
