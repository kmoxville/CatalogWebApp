using CatalogWebApp.Services.CatalogService;
using CatalogWebApp.Services.EmailService;
using CatalogWebApp.Services.NotificationService;
using CatalogWebApp.Utils.Options;
using Quartz;
using Serilog;
using Serilog.Events;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddDbContext<CatalogContext>(opt => opt.UseInMemoryDatabase("catalog"));
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ICatalogService, CatalogService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddAutoMapper(typeof(MapperProfile));

builder.Services.AddQuartz(q =>
 {
     var jobKey = new JobKey("LiveNotificationService");

     q.UseMicrosoftDependencyInjectionJobFactory();

     q.AddJob<NotificationService>(cfg => cfg.WithIdentity(jobKey));
     q.AddTrigger(opts => opts
        .ForJob(jobKey)
        .WithIdentity("LiveNotificationService-trigger")
        .WithSimpleSchedule(sch =>
            sch.WithIntervalInMinutes(1)
            .RepeatForever()));
 });

builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

builder.Services.Configure<EmailOptions>(
    builder.Configuration.GetSection(EmailOptions.Position));

builder.Services.Configure<NotificationEmailOptions>(
    builder.Configuration.GetSection(NotificationEmailOptions.Position));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStatusCodePagesWithReExecute("/Errors/{0}");
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
