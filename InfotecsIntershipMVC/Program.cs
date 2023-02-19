using InfotecsIntershipMVC.DAL.DbContexts;
using InfotecsIntershipMVC.DAL.Models;
using InfotecsIntershipMVC.DAL.Repositories;
using InfotecsIntershipMVC.Services;
using InfotecsIntershipMVC.Services.Converting;
using InfotecsIntershipMVC.Services.CSV;
using InfotecsIntershipMVC.Services.Filtering;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddDbContext<InfotecsDBContext>
    (options => options.UseSqlServer(builder.Configuration.GetConnectionString("InfotecsDB")));


builder.Services.AddScoped<ICsvService, CsvService>();
builder.Services.AddScoped<IConvertingService, ConvertingService>();
builder.Services.AddScoped<IFilteringService, FilteringService>();
/*builder.Services.AddScoped<IGenericAsyncRepository<FileEntity>, FilesRepository>();
builder.Services.AddScoped<IGenericRepository<FileEntity>, FilesRepository>();*/
builder.Services.AddTransient<MainService>();
builder.Services.AddTransient<FilesRepository>();
builder.Services.AddTransient<ResultsRepository>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "My title",
        Description = "My description",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Example Contact",
            Url = new Uri("https://example.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
