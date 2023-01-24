using WebApi.Halper;
using Microsoft.EntityFrameworkCore;
using WebApi.AppDbContext;
using System.Text.Json.Serialization;
using System.Text.Json;
using WebApi.Middleware;
using WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

//DBContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection"), sqlOptions => sqlOptions.UseNetTopologySuite()));

//AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

// Add services to the container.
//Angular
builder.Services.AddControllers()
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;// pour être certain que les JSON renvoyés sont en camelCase
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

//Angular
string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var frontend_url = builder.Configuration.GetValue<string>("frondend_url");
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
      builder => builder
      .WithOrigins(frontend_url).AllowAnyHeader().AllowAnyMethod());
});
//-----------------------
//Auth
var domain = $"https://{builder.Configuration["Auth0:Domain"]}/";
var audiance = builder.Configuration["Auth0:Audience"];
builder.Services.AddAuthServices(domain, audiance);
builder.Services.AddApplicationServices();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDocumentation();


var app = builder.Build();
app.UseCors(MyAllowSpecificOrigins); //Angular

app.UseMiddleware<ExceptionMiddleware>(); //For Errors
app.UseStatusCodePagesWithReExecute("/errors/{0}"); //For Errors

app.UseSwaggerDocumentation();
app.UseHttpsRedirection();
app.UseAuthentication();//Auth0
app.UseAuthorization();
app.MapControllers();
app.Run();
