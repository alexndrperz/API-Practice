var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(); 
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
