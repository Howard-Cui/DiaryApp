
using DiaryApp.Data;
using Microsoft.EntityFrameworkCore;

/**
 iunitialization the whole project
 
 */
var builder = WebApplication.CreateBuilder(args);

// Add services to the DI container.
builder.Services.AddControllersWithViews();
// Add Database Context, db context is acutally a service or dependency that need to inject into the ioc container
builder.Services.AddDbContext<ApplicationDbContext>(options => 
    // becuase we install the sql server package, hence we can you this line of code to connect with the sql server
    options.UseSqlServer(
        /*
         the builder.Configuration will access our setting in the appsettings.json file and read schema of it
        hence we can get the connection string that we want to get
         */
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

// compile all the services to our application
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // use the global controller(in mvc case, it is using the the Error Pages)
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    // when in production, we need to use the https to secure our app
    app.UseHsts();
}

//redirecting all http => https
app.UseHttpsRedirection();
//allow to use those of the file that hosted in the wwwroot
app.UseStaticFiles();
// routing request to the right controller
app.UseRouting();
// verifies that users have permission to access request resources (Like Guard in Nest.js or API Gateway Authentication)
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
