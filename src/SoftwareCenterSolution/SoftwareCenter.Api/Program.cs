using Marten;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(); // Going to eat some of the time to start this API, and use some memory.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// above this line is configuring services and opting in to the .NET features

// Ask my envrionment for the connection string to my database
var connectionString = builder.Configuration.GetConnectionString("software") ?? 
    throw new Exception("No software connection string found!");

// look a lot of places - and it always looks in all the places, even if it already found it.
// 1. appsettings.json
// 2. appsettings.{ASPNET_CORE_ENVIRONMENT}.json
// 3. looks in the "secrets" in visual studio. Not showing this.
// 4. look in an environment variable on the machine its running on
//      In this example it would look for connectionstrings_software
// 5. it will look on the command line when you do "dotnet run"

// Setup my "service" that will connect to the databse
// builder.Services.AddDbContext<MyDbContext>(c => {...});
builder.Services.AddMarten(config =>
{
    config.Connection(connectionString);
}).UseLightweightSessions();
// It will provide an object that implements a context class.
// IDocument Session

var app = builder.Build();
// after this line is configuring HTTP "middleware" - how are the actual requess and responses generated.

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();

app.MapControllers(); // This uses .NET reflection to scan your application and read
// the routing attributes and create teh "routing table" - like a phone book.
// Current Route Table:
// GET requests to /vendors
//  - Create an instance of the VendorsController
//  - Call the GetAllVendors method.
// POST /vendors
//  - create an instance of the vendors controller
//  - call the addvendor method
//  - but this is going to need an IDocumentSession
// GET requests to /vendors/{id} where id loooks like a Guid
//  - create the VendorsController
//  - call the GetVendorById method with that id from the url.

app.Run(); // Kestrel Web Server

public partial class Program; // willing to explain later if you want. .NET 10 (next week) won't require this.
