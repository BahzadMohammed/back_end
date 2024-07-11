using Microsoft.EntityFrameworkCore;
using news_api.Data;
// using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using BCrypt.Net;
using news_api.Interfaces;
using news_api.Repository;
using news_api.Controllers;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using news_api.Data.Seed;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<INewsRepository, NewsRepository>();
builder.Services.AddScoped<IGenreRepository, GenreRepository>();
// var key = Encoding.ASCII.GetBytes("Jwt:Key"); // Use a secret key stored in a secure place

builder.Services.AddControllers().AddJsonOptions(
    options => {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        options.JsonSerializerOptions.DictionaryKeyPolicy = null;
    }
);



builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/auth/login";
        options.LogoutPath = "/auth/logout";
        options.AccessDeniedPath = "/auth/accessdenied";
    });

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "News API",
        Version = "v1"
    });
});


builder.Services.AddEndpointsApiExplorer();

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000") // React app URL
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});



// ---------------------------------------------------------------------------
var app = builder.Build();



// Seed the database.
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedData.Initialize(services);
}



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseRouting();
app.UseCors("AllowReactApp");
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();

app.Run();









// var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);
// var jwtKey = builder.Configuration["Jwt:Key"];
// var jwtIssuer = builder.Configuration["Jwt:Issuer"];
// var jwtAudience = builder.Configuration["Jwt:Audience"];

// if (string.IsNullOrEmpty(jwtKey) || string.IsNullOrEmpty(jwtIssuer) || string.IsNullOrEmpty(jwtAudience))
// {
//     throw new InvalidOperationException("JWT settings are not configured properly in appsettings.json.");
// }

// var key = Encoding.ASCII.GetBytes(jwtKey);

// builder.Services.AddSwaggerGen(option =>
// {
//     option.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Demo API", Version = "v1" });
//     option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//     {
//         In = ParameterLocation.Header,
//         Description = "Please enter a valid token",
//         Name = "Authorization",
//         Type = SecuritySchemeType.Http,
//         BearerFormat = "JWT",
//         Scheme = "Bearer"
//     });
//     option.AddSecurityRequirement(new OpenApiSecurityRequirement
//     {
//         {
//             new OpenApiSecurityScheme
//             {
//                 Reference = new OpenApiReference
//                 {
//                     Type=ReferenceType.SecurityScheme,
//                     Id="Bearer"
//                 }
//             },
//             new string[]{}
//         }
//     });
// });



// builder.Services.AddAuthentication(options =>
// {
//     options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//     options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
// })
// .AddJwtBearer(options =>
// {
//     options.RequireHttpsMetadata = false;
//     options.SaveToken = true;
//     options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
//     {
//         ValidateIssuer = true,
//         ValidateAudience = true,
//         ValidateLifetime = true,
//         ValidateIssuerSigningKey = true,
//         ValidIssuer = jwtIssuer,
//         ValidAudience = jwtAudience,
//         IssuerSigningKey = new SymmetricSecurityKey(key)
//     };
// });





// // Seed the database.
// using (var scope = app.Services.CreateScope())
// {
//     var services = scope.ServiceProvider;
//     try
//     {
//         SeedData.Initialize(services);
//     }
//     catch (Exception ex)
//     {
//         var logger = services.GetRequiredService<ILogger<Program>>();
//         logger.LogError(ex, "An error occurred seeding the DB.");
//     }
// }