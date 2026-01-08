using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using TFBS.Data;
using TFBS.Exceptions;
using TFBS.Services;
using TFBS.Services.Interfaces;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<TfbsContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TFBS")));

builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<ITripService, TripService>();
builder.Services.AddScoped<IMaintenanceService, MaintenanceService>();
builder.Services.AddScoped<ILookupService, LookupService>();
builder.Services.AddScoped<IReportService, ReportService>();

// Add services to the container.
builder.Services.AddControllersWithViews();


var app = builder.Build();

// Global exception handling middleware
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        // Get the exception details
        var ex = context.Features.Get<IExceptionHandlerFeature>()?.Error;

        // Set response content type
        context.Response.ContentType = "application/json";

        // Handle ApiException and its derived types
        if (ex is ApiException apiEx)
        {
            context.Response.StatusCode = apiEx.StatusCode;
            await context.Response.WriteAsJsonAsync(new
            {
                errorCode = apiEx.ErrorCode,
                message = apiEx.Message
            });
            return;
        }

        // Handle other exceptions
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await context.Response.WriteAsJsonAsync(new
        {
            errorCode = "INTERNAL_ERROR",
            message = "Unexpected server error."
        });
    });
});


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
