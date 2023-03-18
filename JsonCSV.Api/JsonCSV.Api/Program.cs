using JsonCSV.Api.DbContexts;
using JsonCSV.Api.Models;
using JsonCSV.Api.Services;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Serilog;

Log.Logger = new LoggerConfiguration().MinimumLevel.Fatal().WriteTo.Console()
	.WriteTo.File("logs/cityError.txt", rollingInterval: RollingInterval.Day).
	CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();	
// Add services to the container.
builder.Services.AddMvc(options =>
{
	options.SuppressAsyncSuffixInActionNames = false;
});

builder.Services.AddControllers(options =>
{
	options.ReturnHttpNotAcceptable = true; // Making an error if the return format is not supported
}).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters(); // Making th return format of xml
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<FileExtensionContentTypeProvider>();
#if DEBUG
builder.Services.AddTransient<IMailService, EmailService>();
#else
builder.Services.AddTransient<IMailService, CloudEmailService>();
#endif
builder.Services.AddSingleton<CitiesDataStore>();
builder.Services.AddDbContext<CityInfoContext>(dbContextOptions => dbContextOptions.UseSqlite(
	builder.Configuration["ConnectionStrings:DataSourceConection"]));

builder.Services.AddScoped<ICityRepository, CityInfoRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

//// Configure the HTTP request pipeline.
/// App Enviroment verify if the app is executing with the idle for use the swagger
if (app.Environment.IsDevelopment()) // This will not function if the enviroment isn't development
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

// Methods for make routing
app.UseHttpsRedirection();

app.UseRouting();	//change


app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
	endpoints.MapControllers(); // change
});


app.Run();
