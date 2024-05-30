using TechMegStore.IOC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//LLAMAR EL METODO PARA LA DEPENDENCIA A LA BASE DE DATOS CON LA CADENA DE CONEXIONSQLSERVER
builder.Services.InjectDependency(builder.Configuration); //Llamamos al metodo y pasarle la configuracion de la aplicacion y asi acceder al appsettings

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
