using ApiProjeKampi.WebUI.Handler;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddHttpClient();
builder.Services.AddTransient<AuthTokenHandler>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient("ApiHttpClient", client =>
{
//     API'nin temel adresini burada merkezi olarak belirleyebilirsin.
    client.BaseAddress = new Uri("http://localhost:5155/api/");
})
.AddHttpMessageHandler<AuthTokenHandler>();


builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Default}/{action=Index}/{id?}");

app.Run();

