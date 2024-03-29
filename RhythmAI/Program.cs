using Microsoft.OpenApi.Models;
using Microsoft.SemanticKernel;


var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var openAIKey = configuration["OpenAIKey"];
// Create Kernel 
var kernelBuilder = Kernel.CreateBuilder();
kernelBuilder.AddOpenAIChatCompletion("gpt-3.5-turbo", openAIKey!);
var kernel = kernelBuilder.Build();
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton(kernel);
builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});

app.Run();
