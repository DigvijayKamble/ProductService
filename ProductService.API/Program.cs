var builder = WebApplication.CreateBuilder(args);

// Register services BEFORE building the app
builder.Services.ConfigureDependencies(builder.Configuration);

// Add services to the container.  
var app = builder.Build();  

Console.WriteLine("Starting ConfigureMiddleware");
app.ConfigureMiddleware(builder.Configuration);
Console.WriteLine("Ends ConfigureMiddleware");  
app.Run();
 