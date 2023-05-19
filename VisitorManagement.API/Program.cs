using Microsoft.EntityFrameworkCore;
using VisitorManagement.DataAccess;
using VisitorManagement.API.Common;
using VisitorManagement.Service.IRepoInfo;
using VisitorManagement.Service.service;
using Serilog.AspNetCore;
using Serilog.Expressions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#region CoRS Origin for Sharing the resource in cross platform like mobile,ioT so the we need to config here.

builder.Services.AddCors(Options =>
{
    Options.AddPolicy("CustomPolicy", x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});
#endregion
#region Db connection

var connectionstring = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<VisitorCategoryDBContext>(options => options.UseSqlServer(connectionstring, b => b.MigrationsAssembly("VisitorManagement.API")));

#endregion
#region serilog config for log files
builder.Host.UseSerilog((context, config) =>
{
    config.WriteTo.File("ApiLogs/log.txt", rollingInterval: RollingInterval.Day);
    if (context.HostingEnvironment.IsProduction() == false)
    {
        config.WriteTo.Console();
    }
});
#endregion
#region Configure Mapping
builder.Services.AddAutoMapper(typeof(MappingProfile));
#endregion
#region  // configure service and interfacerepo
builder.Services.AddTransient<ICategoryRepository, VisitorCategoryService>();
builder.Services.AddTransient<IVisitorRepository, VisitorDetailService>();
builder.Services.AddTransient<IResidentRepository, ResidentDetailService>();
builder.Services.AddTransient<IVisitorLogRepository, VisitorLogService>();
#endregion
#region injecting Generic type for Pagination
builder.Services.AddTransient(typeof(IPaginationServices<,>),typeof(PaginationServices<,>));
#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("CustomPolicy");   //pipeline connection cors so that other platform can access // gateway

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
