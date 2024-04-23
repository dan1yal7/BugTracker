using Bug_Tracker.Authclass;
using Bug_Tracker.BackroundServices;
using Bug_Tracker.Controllers;
using Bug_Tracker.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens; 
var builder = WebApplication.CreateBuilder(args);

//Add services to the container. 

 builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
   .AddJwtBearer(options =>
   {
       options.TokenValidationParameters = new TokenValidationParameters
       {
           // Указывает будет ли валидироваться издатель при валидации токена 
           ValidateIssuer = true,

           // строка представляющая потребителя 
           ValidIssuer = AuthOptions.ISSUER,

           // указывать будет ли валидироваться потребитель  
           ValidateAudience = true,

           // Установка потребителя токена 
           ValidAudience = AuthOptions.AUDIENCE,

           //Будет ли валидироваться время существования 
           ValidateLifetime = true,

           // установка ключа безопасности
           IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),

           //валидация ключа безопасности
           ValidateIssuerSigningKey = true,
       };
   });

builder.Services.AddControllersWithViews();

//DI 
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection")));
builder.Services.AddScoped<IBugRepositorycs, BugRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();

var app = builder.Build();




// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "bug",
    pattern: "{controller = Bug}/{action=Index}/{id}");

app.MapControllerRoute(
    name: "user",
    pattern: "{controller = User}/{action=Index}/{id}");

app.MapControllerRoute(
    name: "comment",
    pattern: "{controller = Comment}/{action=Index}/{id}");

app.Run();
