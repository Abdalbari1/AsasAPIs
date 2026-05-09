using Asas.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AsasContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AsasContext")
    ?? throw new InvalidOperationException("Connection string 'AsasContext' not found.")));

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddCors(options => {
    options.AddPolicy("_myAllowSpecificOrigins", p => {
        p.AllowAnyOrigin()
         .AllowAnyMethod()
         .AllowAnyHeader();
    });
});
// init Hash password 
builder.Services.AddScoped<Asas.AsasHash.Asas.Contracts.IAsasHashPassword, Asas.AsasHash.AsasHash>();

var app = builder.Build();



if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Asas APIs v1");
        options.RoutePrefix = "swagger";
    });
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseCors("_myAllowSpecificOrigins");

app.UseAuthorization();

app.MapControllers();

app.Run();