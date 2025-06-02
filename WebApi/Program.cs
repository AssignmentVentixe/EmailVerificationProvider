using WebApi.Services;
using Azure.Communication.Email;
using WebApi.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddMemoryCache();
builder.Services.AddSingleton(x => new EmailClient(builder.Configuration["ACS:ConnectionString"]));

builder.Services.AddScoped<IVerificationService, VerificationService>();
builder.Services.AddScoped<IBookingEmailService, BookingEmailService>();

builder.Services.AddCors(o =>
    o.AddPolicy("CorsPolicy", p =>
        p.WithOrigins("https://lively-hill-0b76ba003.6.azurestaticapps.net")
         .AllowAnyHeader()
         .AllowAnyMethod()
         .AllowCredentials()));

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapOpenApi();

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
