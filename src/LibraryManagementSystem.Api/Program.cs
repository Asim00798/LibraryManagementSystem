
using LibraryManagementSystem.Host;
using LibraryManagementSystem.Host.Extensions;
using LibraryManagementSystem.Host.Middlewares;
using LibraryManagementSystem.Host.Seeder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add Swagger/OpenAPI support
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHostServices(builder.Configuration);
builder.Services.AddApiServices(builder.Configuration);
var app = builder.Build();
// -----------------------------
// Seed runtime data
// -----------------------------
using (var scope = app.Services.CreateScope())
{
    var scopedServices = scope.ServiceProvider;
    await StartupSeeder.SeedAsync(scopedServices);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); // default: /swagger
}
// -----------------------------
// Middleware
// -----------------------------
app.UseHttpsRedirection();
app.UseGlobalExceptionHandler();
app.UseCorrelationId();

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();



//// -----------------------------
//// API Program.cs
//// -----------------------------
//using LibraryManagementSystem.Host;
//using LibraryManagementSystem.Host.Extensions;
//using LibraryManagementSystem.Host.Middlewares;
//using LibraryManagementSystem.Host.Seeder;


//var builder = WebApplication.CreateBuilder(args);

//// -----------------------------
//// Add services
//// -----------------------------

//builder.Services.AddHostServices(builder.Configuration);   // Infrastructure + Application
//builder.Services.AddEndpointsApiExplorer();                // Required for Swagger/OpenAPI
//builder.Services.AddApiServices(builder.Configuration);    // JWT + Swagger
//builder.Services.AddControllers();

//// -----------------------------
//// Build app
//// -----------------------------
//var app = builder.Build();

//// -----------------------------
//// Swagger UI
//// -----------------------------
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI(c =>
//    {
//        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Library Management API v1");
//        c.RoutePrefix = string.Empty; // Swagger at root
//    });
//}

//// -----------------------------
//// Seed runtime data
//// -----------------------------
//using (var scope = app.Services.CreateScope())
//{
//    var scopedServices = scope.ServiceProvider;
//    await StartupSeeder.SeedAsync(scopedServices);
//}

//// -----------------------------
//// Middleware
//// -----------------------------
//app.UseHttpsRedirection();
//app.UseGlobalExceptionHandler();
//app.UseCorrelationId();

//app.UseAuthentication();
//app.UseAuthorization();

//// -----------------------------
//// Map controllers
//// -----------------------------
//app.MapControllers();
//// -----------------------------
//// Run app
//// -----------------------------
//app.Run();
