using LibraryManagement.Business;
using LibraryManagement.Data;
using Microsoft.AspNetCore.HttpOverrides;
using NpgsqlTypes;
using Serilog;
using Serilog.Core;
using Serilog.Sinks.PostgreSQL;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddPersestenceServices(connectionString: builder.Configuration.GetConnectionString("PostgreSQL"));
builder.Services.AddBusinessServices();
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders =
        ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});

// Create a Serilog logger configuration
Logger log = new LoggerConfiguration()
    .WriteTo.Console() // Write log events to the console
    .WriteTo.PostgreSQL(builder.Configuration.GetConnectionString("PostgreSQL"), "logs", // Write log events to PostgreSQL database
        needAutoCreateTable: true, // Automatically create the logs table if it doesn't exist
        columnOptions: new Dictionary<string, ColumnWriterBase>
        {
            { "message", new RenderedMessageColumnWriter((NpgsqlDbType.Text)) }, // Specify column for the log message
            { "message_template", new MessageTemplateColumnWriter((NpgsqlDbType.Text)) }, // Specify column for the log message template
            { "level", new LevelColumnWriter(true, NpgsqlDbType.Varchar) }, // Specify column for the log level
            { "raise_date", new TimestampColumnWriter(NpgsqlDbType.TimestampTz) }, // Specify column for the log timestamp
            { "exception", new ExceptionColumnWriter((NpgsqlDbType.Text)) }, // Specify column for the exception details
            { "properties", new LogEventSerializedColumnWriter(NpgsqlDbType.Jsonb) } // Specify column for additional log event properties
        })
    .MinimumLevel.Information() // Set the minimum log level to Information
    .CreateLogger(); // Create the logger

builder.Host.UseSerilog(log); // Use Serilog logger with the host

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true); // Enable legacy timestamp behavior for Npgsql
var app = builder.Build(); // Build the application

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error"); // Use exception handler middleware in non-development environment
    app.UseHsts(); // Use HTTP Strict Transport Security (HSTS) middleware
}

//app.UseHttpsRedirection(); // Redirect HTTP requests to HTTPS
app.UseStaticFiles(); // Serve static files

app.UseRouting();

app.UseAuthentication(); // Use authentication middleware
app.UseAuthorization(); // Use authorization middleware

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Book}/{action=Index}/{id?}"); // Map default controller route

app.UseMiddleware<LibraryManagement.Core.Middleware.ExceptionHandlerMiddleware>(); // Use custom exception handler middleware

app.Run(); // Run the application
