using FashionShop.Repositories;
using FashionShop.Data;
using FashionShop.Models.Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;

using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register Database
builder.Services.AddDbContext<FashionShopDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Register Controller
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITokenRepository, TokenRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

//Register service authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option =>
{
    option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]
            ))
    };
});

// Config identity user
builder.Services.AddIdentityCore<User>()
    .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<User>>("FashionShop")
    .AddEntityFrameworkStores<FashionShopDBContext>()
    .AddDefaultTokenProviders();


// Config password register
builder.Services.Configure<IdentityOptions>(option =>
{
    // Thiết lập về Password
    option.Password.RequireDigit = false; // Không bắt phải có số
    option.Password.RequireLowercase = false; // Không bắt phải có chữ thường
    option.Password.RequireNonAlphanumeric = false; // Không bắt ký tự đặc biệt
    option.Password.RequireUppercase = false; // Không bắt buộc chữ in
    option.Password.RequiredLength = 3; // Số ký tự tối thiểu của password
    option.Password.RequiredUniqueChars = 0; // Số ký tự riêng biệt
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
