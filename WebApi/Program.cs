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

var app = builder.Build();

app.MapOpenApi();

app.UseHttpsRedirection();

app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run(); 