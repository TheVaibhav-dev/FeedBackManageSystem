using FeedBackManageSystem.Data;
using FeedBackManageSystem.HelperClasses;
using FeedBackManageSystem.Repositories.Interface;
using FeedBackManageSystem.Repositories.Repository;
using FeedBackManageSystem.Services.Interface;
using FeedBackManageSystem.Services.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Find other services and repositories here
// Repositories start
builder.Services.AddScoped<IFeedbackRepository, FeedbackRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserBlogRepository, UserBlogRepository>();
// Repositories end

// Services start
builder.Services.AddScoped<IFeedbackService, FeedbackService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<PasswordService>();
builder.Services.AddScoped<IUserBlogService, UserBlogService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();

// Services end

// Out services and repositories here

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseSession();
app.UseAuthorization();
app.UseAuthentication();
app.MapStaticAssets();
ProjectSession.Configure(app.Services.GetRequiredService<IHttpContextAccessor>());

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Visitor}/{action=MyVisitor}/{id?}")
    .WithStaticAssets();


app.Run();
