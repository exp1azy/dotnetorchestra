using DotNetOrchestra.Server.Startup;
using DynamicDI;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.RegisterDbContexts();
builder.Services.RegisterServices();
builder.Services.AddCors();

string port = builder.Configuration["ClientPort"]!;
var app = builder.Build();
app.UseRouting();
app.UseCors(b => b.WithOrigins($"http://localhost:{port}").AllowAnyHeader().AllowAnyMethod());
app.MapControllers();
app.UseMiddleware<ExceptionHandlerMiddleware>();
app.Run();