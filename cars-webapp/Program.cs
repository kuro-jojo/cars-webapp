var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizePage("/Update");
    options.Conventions.AuthorizePage("/Create");
    options.Conventions.AuthorizePage("/Sell");
    options.Conventions.AllowAnonymousToPage("/Login");
    options.Conventions.AllowAnonymousToPage("/List");
    options.Conventions.AllowAnonymousToFolder("/Private/PublicPages");

});

builder.Services.AddAuthentication().AddCookie("BaseCookie", options =>
{
    options.Cookie.Name = "BaseCookie";
    options.LoginPath = "/Login";
    options.LogoutPath = "/Logout";
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

System.Globalization.CultureInfo.CurrentCulture = new System.Globalization.CultureInfo("fr-FR");
System.Globalization.CultureInfo.CurrentUICulture = new System.Globalization.CultureInfo("fr-FR");
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapGet("/", context =>
{
    context.Response.Redirect("/List");
    return Task.CompletedTask;
});

app.MapRazorPages();

app.Run();
