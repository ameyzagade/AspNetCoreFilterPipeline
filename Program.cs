using AspNetCoreFilterPipeline.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    // By default, the GlobalControllerActionFilter might have ran first in the 
    // before stage of the action pipeline, however, the order argument changes the sequence
    // These filters registererd globally need not be registered with DI container 
    options.Filters.Add(typeof(GlobalControllerActionFilter), order: 100);
    options.Filters.Add<GlobalControllerAsyncActionFilter>();
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// registering the controller level and action method level action filters as scoped
builder.Services.AddScoped<ControllerActionFilter>();
builder.Services.AddScoped<ControllerAsyncActionFilter>();
builder.Services.AddScoped<ActionMethodActionFilter>();
builder.Services.AddScoped<ActionMethodAsyncActionFilter>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
