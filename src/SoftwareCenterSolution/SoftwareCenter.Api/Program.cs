var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(); // Going to eat some of the time to start this API, and use some memory.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// above this line is configuring services and opting in to the .NET features
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
// GET Requests to /vendors
//  - Create an instance of VendorsController
//  - Call the GetAllVendorsAsync method

app.Run(); // Kestrel Web Server

public partial class Program; // willing to explain later if you want. .NET 10 (next week) won't require this.
