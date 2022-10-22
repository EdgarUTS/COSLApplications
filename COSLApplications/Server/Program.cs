using COSLApplications.Server.Date;
using COSLApplications.Server.Services.Incident;
using COSLApplications.Server.Services.User;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddHttpClient();

//builder.Services.AddDbContext<ApplicationDbContext>(
//options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<ApplicationDbContext>(
//	options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// For DI registration
builder.Services.AddTransient<IIncident, IncidentService>();
builder.Services.AddTransient<IUser, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseWebAssemblyDebugging();
}
else
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
//app.UseStaticFiles(new StaticFileOptions
//{
//	FileProvider = new PhysicalFileProvider(
//  Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "HSEDoc")),
//	RequestPath = "/files"
//});

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
