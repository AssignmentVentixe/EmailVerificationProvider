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
        p.WithOrigins(
            "https://auth-api-ventixe-b9dyccgkh4egdtd8.swedencentral-01.azurewebsites.net",
            "https://booking-api-ventixe-e5hydeevg6htf7br.swedencentral-01.azurewebsites.net"
        )
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials()
    )
);

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
