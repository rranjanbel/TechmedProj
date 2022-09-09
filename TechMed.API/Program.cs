using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;
using TechMed.BL.Mapper;
using TechMedAPI.JwtInfra;
using AutoMapper;
using TechMed.DL.Models;
using Microsoft.EntityFrameworkCore;
using TechMed.API.Services;
using TechMed.BL.Repository.Interfaces;
using TechMed.BL.Repository.BaseClasses;
using TechMed.API.Middleware;
using Microsoft.Extensions.FileProviders;
using TechMed.BL.TwilioAPI.Model;
using TechMed.BL.TwilioAPI.Service;
using TechMed.API.NotificationHub;
using TechMed.BL.ModelMaster;
using TechMed.DL.ViewModel;
using TechMed.BL.CommanClassesAndFunctions;


var builder = WebApplication.CreateBuilder(args);

// Add services for logger.
var logger = new LoggerConfiguration()
  .ReadFrom.Configuration(builder.Configuration)
  .Enrich.FromLogContext()
  .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

// Add services to the container.

builder.Services.AddControllers();
var connectionString = builder.Configuration.GetConnectionString("TeliMedConn");
builder.Services.AddDbContext<TeleMedecineContext>(
    options => options.UseSqlServer(connectionString)
    );
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(MappingMaster));
builder.Services.AddSwaggerGen();
//builder.Services.AddCors(o => o.AddPolicy("AllowAll", builder =>
//{
//    builder
//    .WithOrigins("http://localhost:4200")
//    .AllowAnyMethod()
//    .AllowAnyHeader()
//    .AllowCredentials();

//}));
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => { builder.SetIsOriginAllowed(origin => true).AllowAnyMethod().AllowAnyHeader().AllowCredentials(); });
});
var jwtTokenConfig = builder.Configuration.GetSection("jwtTokenConfig").Get<JwtTokenConfig>();

//builder.Services.AddSingleton(typeof(IConverter),
//   new SynchronizedConverter(new PdfTools()));
builder.Services.AddSingleton(jwtTokenConfig);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = true;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = jwtTokenConfig.Issuer,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtTokenConfig.Secret)),
        ValidAudience = jwtTokenConfig.Audience,
        ValidateAudience = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.FromMinutes(1)
    };
});
builder.Services.AddSingleton<IJwtAuthManager, JwtAuthManager>();
builder.Services.AddHostedService<JwtRefreshTokenCache>();
var mailSettings = builder.Configuration.GetSection("MailSettings").Get<MailSettings>();
builder.Services.AddSingleton(mailSettings);
var smsSettings = builder.Configuration.GetSection("SMSSettings").Get<SMSSetting>();
builder.Services.AddSingleton(smsSettings);
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IPHCRepository, PHCRepository>();
builder.Services.AddScoped<ISpecializationRepository, SpecializationRepository>();
builder.Services.AddScoped<IPatientCaseRepository, PatientCaseRepository>();
builder.Services.AddScoped<IDashBoardRepository, DashBoardRepository>();
builder.Services.AddScoped<IMISRepository, MISRepository>();
builder.Services.AddScoped<ICaseFileStatusMasterRpository, CaseFileStatusMasterRpository>();
builder.Services.AddScoped<IVideoCallTransactionRespository, VideoCallTransactionRespository>();
builder.Services.AddScoped<IEquipmentUptimeReport, EquipmentUptimeReportRepositry>();
builder.Services.AddScoped<IDigonisisRepository, DigonisisRepository>();
builder.Services.AddScoped<IDrugsRepository, DrugsRepository>();
builder.Services.AddScoped<ITwilioMeetingRepository, TwilioMeetingRepository>();
builder.Services.AddScoped<IHolidayRepository, HolidayRepository>();
builder.Services.AddScoped<ICDSSGuidelineRepository, CDSSGuidelineRepository>();
builder.Services.AddScoped<IMasterRepository, MasterRepository>();
builder.Services.AddScoped<ISettingMaster, SettingMaster>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<ISystemHealthRepository, SystemHealthRepository>();

builder.Services.AddScoped<IMailService, MailService>();
builder.Services.AddScoped<ISnomedRepository, SnomedRepository>();
builder.Services.Configure<TwilioSettings>(
    settings =>
    {
        settings.AccountSid = builder.Configuration.GetValue<string>("Twilio:TwilioAccountSid");
        settings.ApiSecret = builder.Configuration.GetValue<string>("Twilio:TwilioApiSecret");
        settings.ApiKey = builder.Configuration.GetValue<string>("Twilio:TwilioApiKey");
        settings.AuthToken = builder.Configuration.GetValue<string>("Twilio:TwilioAuthToken");
    })
    .AddTransient<ITwilioVideoSDKService, TwilioVideoSDKService>();
// services.AddSingleton(Configuration.GetSection("myConfiguration").Get<MyConfiguration>());
var applicationRootUrl = builder.Configuration.GetSection("ApplicationRootUrl").Get<ApplicationRootUri>();
builder.Services.AddSingleton(applicationRootUrl);
builder.Services.AddSignalR();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TechMed API", Version = "v1" });

    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "JWT Authentication",
        Description = "Enter JWT Bearer token **_only_**",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer", // must be lower case
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };
    c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {securityScheme, new string[] { }}
                });
});

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAll",
//        builder => { builder.SetIsOriginAllowed(origin => true).AllowAnyMethod().AllowAnyHeader().AllowCredentials(); });
//});
var app = builder.Build();
//var log = new LoggerFactory();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

    // app.UseSwagger();
    // app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}
// need to remove on production deployment
app.UseSwagger();
app.UseSwaggerUI();

app.UseDefaultFiles();

app.UseMiddleware<ExceptionMiddleware>();
app.UseCors("AllowAll");
app.UseRouting();

//app.UseCors("AllowAll");


app.MapHub<SignalRBroadcastHub>("/notificationHub");

app.UseHttpsRedirection();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.ContentRootPath, "MyStaticFiles")),
    RequestPath = "/MyFiles"
});

app.UseAuthentication();

app.UseAuthorization();

//log.AddSerilog();

app.MapControllers();

app.Run();
