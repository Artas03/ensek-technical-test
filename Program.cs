/*using Ensek_Remote_Technical_Test.Data;
using Microsoft.EntityFrameworkCore;*/

using Ensek_Remote_Technical_Test.Data;
using Ensek_Remote_Technical_Test.Repositories;
using Ensek_Remote_Technical_Test.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//Register AppDbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register customer services and repositories for DI
builder.Services.AddScoped<IMeterReadingUploadService, MeterReadingUploadService>();
builder.Services.AddScoped<IMeterReadingRepository, MeterReadingRepository>();

var app = builder.Build();

//Seed the db with Test_Accounts.csv
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();

    //Creating DB and applying pending migrations
    context.Database.Migrate();

    var csvPath = Path.Combine(Directory.GetCurrentDirectory(), "SeedFiles", "Test_Accounts.csv");
    SeedData.Initialize(context, csvPath);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    /*app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();*/
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
Console.WriteLine("App is running. Routes should now be mapped.");
app.Run();
