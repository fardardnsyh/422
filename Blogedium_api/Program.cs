using System.Text;
using Blogedium_api.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Blogedium_api.Interfaces.Repository;
using Blogedium_api.Repositories;
using Blogedium_api.Interfaces.Services;
using Blogedium_api.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

var configuration = builder.Configuration;
var allowedOrigin = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>();

// db connection configurations
var connectionString = builder.Configuration.GetConnectionString("DefaultValue");

builder.Services.AddDbContext<ApplicationDbContext>(options => 
    options.UseSqlServer(connectionString));

// cors config
builder.Services.AddCors(options =>
{
    options.AddPolicy("myAppCors", policy =>
    {
        policy.WithOrigins(allowedOrigin)
                .AllowAnyHeader()
                .AllowAnyMethod();
    });
});
// jwt configuration
builder.Services.AddAuthentication(options =>
{
    // Without these lines, the application wouldn't know how to handle 
    // authentication requests or challenges, leading to failed authentication attempts
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    // options.TokenValidationParameters -> Specifies the parameters for validating the JWT token.
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true, //  Ensures that the token's issuer matches the expected issuer.
        ValidateAudience = true, //  Ensures that the token's audience matches the expected audience.
        ValidateLifetime = true, // Ensures that the token has not expired and is still valid.
        ValidateIssuerSigningKey = true, //  Ensures that the token's signing key matches the expected signing key.
        ValidIssuer = builder.Configuration["JWT:Issuer"], // Specifies the expected issuer of the token (retrieved from configuration).
        ValidAudience = builder.Configuration["JWT:Audience"], //  Specifies the expected audience of the token (retrieved from configuration).
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigningKey"])) // Specifies the key used to sign the token, which must match the signing key used to generate the token.
    };
});

// ValidateIssuer: Without this, any issuer could be accepted, compromising security.
// ValidateAudience: Without this, any audience could be accepted, potentially allowing tokens intended for different services.
// ValidateLifetime: Without this, expired tokens could still be accepted, compromising security.
// ValidateIssuerSigningKey: Without this, any key could be used to validate the token, compromising security.
// ValidIssuer and ValidAudience: These ensure that the token is issued by a trusted authority and intended for this application.
// IssuerSigningKey: Ensures that the token is validated with the correct secret key. Without it, token validation would fail.

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IBlogRepository, BlogRepository>();
builder.Services.AddScoped<IBlogService, BlogService>();

builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<ICommentService, CommentService>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("myAppCors");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();
