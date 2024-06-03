using TechMegStore.IOC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//LLAMAR EL METODO PARA LA DEPENDENCIA A LA BASE DE DATOS CON LA CADENA DE CONEXIONSQLSERVER
builder.Services.InjectDependency(builder.Configuration); //Llamamos al metodo y pasarle la configuracion de la aplicacion y asi acceder al appsettings

//ACTIVAMOS LOS CORSpara que las url tanto de la API y de ANGULAR
//No tengan confictos de comunicacion y de permisos ya que al ser distintas url
//son propensas a errores de conexion con el server
builder.Services.AddCors(options =>
{
    //Llamamos a las opciones donde agregaremos nuevas politicas y hacemos un llamado a la aplicacion
    options.AddPolicy("New Policy", app =>
    {
        //Configuramos los permisos de la aplicacion
        //Permitimos cualquier origen
        app.AllowAnyOrigin()
        //Permitir cualquier cabecera
        .AllowAnyHeader()
        //Permitir cualquier metodo el post,put, delete, get, etc.
        .AllowAnyMethod();
    });

});



var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Activamos toda la configuracion de los "Cors"
//Llamamos a nuestra aplicacion
app.UseCors("New Policy");//Dentro llamamos a la nueva politica que hemos creado


app.UseAuthorization();

app.MapControllers();

app.Run();
