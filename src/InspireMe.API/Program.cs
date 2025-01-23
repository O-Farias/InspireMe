using InspireMe.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(); // Add this line to support controllers
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register services
builder.Services.AddSingleton<IQuotesService, QuotesService>(); // Add this line
builder.Services.AddSingleton<ICategoriesService, CategoriesService>(); // Add this line

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization(); // Add this line

app.MapControllers(); // Add this line to map controller routes

app.Run();