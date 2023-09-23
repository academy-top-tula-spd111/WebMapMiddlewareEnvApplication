using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Reflection.PortableExecutable;
using WebMapMiddlewareEnvApplication;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseMyToken("12345");

app.Map("/date", (appBuilder) =>
{
    var date = DateTime.Now.ToShortDateString();

    appBuilder.Use(async (context, next) =>
    {
        Console.WriteLine($"Date: {date}");
        await next.Invoke();
    });

    appBuilder.Run(async context =>
    {
        await context.Response.WriteAsync($"Date: {date}");
    });
});
app.Map("/about", appBuilder =>
{
    appBuilder.Map("/all", AboutAll);
    appBuilder.Map("/boss", AboutBoss);

    appBuilder.Run(async context => await context.Response.WriteAsync("About page"));
});
app.Map("/contacts", () => "Contacts");

app.Run(async context =>
{
    await context.Response.WriteAsync($"Hello world");
});

app.Run();

void AboutAll(IApplicationBuilder app)
{
    app.Run(async context => await context.Response.WriteAsync("All collective"));
}

void AboutBoss(IApplicationBuilder app)
{
    app.Run(async context => await context.Response.WriteAsync("Our boss"));
}
