using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using TFBS.Data;
using TFBS.Exceptions;
using TFBS.Services;
using TFBS.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// DbContext
builder.Services.AddDbContext<TfbsContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TFBS")));

// Services
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<ITripService, TripService>();
builder.Services.AddScoped<IMaintenanceService, MaintenanceService>();
builder.Services.AddScoped<ILookupService, LookupService>();
builder.Services.AddScoped<IReportService, ReportService>();

// Controllers (API only)
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Global exception handling (JSON only)
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var ex = context.Features.Get<IExceptionHandlerFeature>()?.Error;

        context.Response.ContentType = "application/json";

        if (ex is ApiException apiEx)
        {
            context.Response.StatusCode = apiEx.StatusCode;
            await context.Response.WriteAsJsonAsync(new
            {
                errorCode = apiEx.ErrorCode,
                message = apiEx.Message,
                traceId = context.TraceIdentifier
            });
            return;
        }

        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await context.Response.WriteAsJsonAsync(new
        {
            errorCode = "INTERNAL_ERROR",
            message = "Unexpected server error.",
            traceId = context.TraceIdentifier
        });
    });
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TFBS API v1");
        c.RoutePrefix = "swagger"; // UI at /swagger
    });
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
