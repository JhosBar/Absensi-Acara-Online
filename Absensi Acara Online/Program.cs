using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Filters;

var builder = WebApplication.CreateBuilder(args);

ConfigureConfiguration(builder.Configuration, builder.Services);

// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.Run();

static void ConfigureConfiguration(ConfigurationManager config, IServiceCollection services)
{
	services.AddAuthentication(options =>
	{
		options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
		options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
		options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
	}).AddCookie(options =>
	{
		options.ExpireTimeSpan = TimeSpan.FromHours(1);
		options.SlidingExpiration = true;
		options.AccessDeniedPath = "/Account/Login";
		options.Cookie.Name = "User";
		options.LoginPath = "/Account/Login";
	});

	//services.AddMvc(options => { options.Filters.Add(new ActionFilter()); });

	services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
	services.AddMemoryCache();

	services.AddHttpClient();
}
