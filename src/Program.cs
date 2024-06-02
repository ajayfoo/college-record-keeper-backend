using CRK.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

builder
    .Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    });
var connectionString =
    builder.Configuration.GetConnectionString("College")
    + builder.Configuration["CollegeDbPassword"];

builder.Services.AddDbContext<CollegeDbContext>(options => options.UseNpgsql(connectionString));
builder
    .Services.AddIdentityApiEndpoints<IdentityUser>()
    .AddEntityFrameworkStores<CollegeDbContext>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = "main";
});
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(
        "oauth2",
        new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
        }
    );
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    _ = app.UseSwagger();
    _ = app.UseSwaggerUI();
}
app.UseCors(options =>
    options
        .WithOrigins("http://localhost:8080")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials()
);

app.MapIdentityApi<IdentityUser>();
app.MapPost(
        "/logout",
        async (SignInManager<IdentityUser> signInManager) =>
        {
            await signInManager.SignOutAsync().ConfigureAwait(false);
        }
    )
    .RequireAuthorization();
app.MapGet(
        "/companies",
        async (CollegeDbContext ctx) =>
        {
            return await ctx.Companies.ToListAsync();
        }
    )
    .RequireAuthorization();
app.MapGet(
        "/achievementTypes",
        async (CollegeDbContext ctx) =>
        {
            return await ctx.AchievementTypes.ToListAsync();
        }
    )
    .RequireAuthorization();
app.MapGet(
        "/achievementLevels",
        async (CollegeDbContext ctx) =>
        {
            return await ctx.AchievementLevels.ToListAsync();
        }
    )
    .RequireAuthorization();
app.UseAuthorization();
app.MapControllers();
app.Run();

public partial class Program { }
