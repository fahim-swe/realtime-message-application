using System.Text.Json.Serialization;
using api.Extensions;
using repository.Imp.Seed;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDataBaseServices(builder.Configuration);
builder.Services.AddControllers()
        .AddJsonOptions(options => {
            // Ignore self reference loop
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
          //  options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        await Seed.AddRoleMookData(services);
        
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

app.Run();
