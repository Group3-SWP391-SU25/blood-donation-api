using BloodDonation.Application;
using BloodDonation.Application.Services;
using BloodDonation.Infrastructures;
using BloodDonation.WebApi;
using BloodDonation.WebApi.Middlewares;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

builder.Services.AddCors(opt =>
{
    opt.AddPolicy(name: "Blood-Donation-Support-System",
                      policy =>
                      {
                          policy.AllowAnyOrigin()
                          .AllowAnyMethod().AllowAnyHeader();
                      });
});

//var config = builder.Configuration.Get<AppSetting>() ?? throw new InvalidOperationException("Configuration Is Null");*/

builder.Services.AddSingleton<AppSetting>(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    var setting = new AppSetting();
    config.GetSection("ConnectionStrings").Bind(setting.ConnectionStrings);
    config.GetSection("FirebaseConfig").Bind(setting.FirebaseConfig);
    return setting;
});

builder.Services.AddSingleton<EmailSettings>(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    var setting = new EmailSettings();
    config.GetSection("EmailSettings").Bind(setting);
    return setting;
});
//builder.Services.AddSingleton(config);
builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});

builder.Services.AddCoreServices(builder.Configuration);
builder.Services.AddControllers().AddJsonOptions(o =>
        {
            o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            o.JsonSerializerOptions.MaxDepth = 0;
        }); ;
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();

builder.Services.AddHostedService<BloodDonationRequestCheckService>();
builder.Services.AddHostedService<BloodStorageCheckService>();
builder.Services.AddHostedService<BloodDonationReminderService>();


var app = builder.Build();

ApplyMigrations();
app.UseSwagger();
app.UseSwaggerUI();
app.UseSession();

app.UseCors("Blood-Donation-Support-System");
app.UseMiddleware<GlobalErrorHandlingMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapControllers();

app.Run();

void ApplyMigrations()
{
    using var scope = app!.Services.CreateScope();
    var _db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    if (_db.Database.GetPendingMigrations().Count() > 0)
    {
        _db.Database.Migrate();
    }
}
