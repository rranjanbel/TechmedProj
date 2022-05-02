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

var builder = WebApplication.CreateBuilder(args);

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
var jwtTokenConfig = builder.Configuration.GetSection("jwtTokenConfig").Get<JwtTokenConfig>();
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
builder.Services.Configure<TwilioSettings>(
    settings =>
    {
        settings.AccountSid = builder.Configuration.GetValue<string>("Twilio:TwilioAccountSid");
        settings.ApiSecret = builder.Configuration.GetValue<string>("Twilio:TwilioApiSecret");
        settings.ApiKey = builder.Configuration.GetValue<string>("Twilio:TwilioApiKey");
    })
    .AddTransient<IVideoService, VideoService>();

builder.Services.AddSignalR();
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

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => { builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); });
});
var app = builder.Build();
var log = new LoggerFactory();
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

app.UseMiddleware<ExceptionMiddleware>();

app.UseRouting();

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.ContentRootPath, "MyStaticFiles")),
    RequestPath = "/MyFiles"
});

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<NotificationHub>("/notificationHub");
});

log.AddSerilog();

app.MapControllers();

app.Run();
