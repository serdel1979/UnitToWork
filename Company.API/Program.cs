using Company.DB;
using Company.Repository.Repository;
using Company.Services.Services;
using Company.UnitOfWork.UOW;
using Company.Utils;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddAutoMapper(typeof(Map));

builder.Services.AddDbContext<ContextCompany>(option =>
{
    option.UseNpgsql(builder.Configuration.GetConnectionString("Conn"));
});


builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddTransient<IDepartmentService, DepartmentService>();

builder.Services.AddTransient<IUnitOfWork,UnitOfWork>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
