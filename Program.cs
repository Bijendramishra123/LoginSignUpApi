using Ramayan_gita_app.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Add controller services
builder.Services.AddControllers();

// 🔹 Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 🔹 Dependency Injection for UserDataAccess (custom service)
builder.Services.AddScoped<UserDataAccess>();

// 🔹 Define named CORS policy
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:5173") // Frontend URL
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

// 🔹 Enable Swagger in Development environment
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 🔹 Enable CORS
app.UseCors(MyAllowSpecificOrigins);

// 🔹 (Optional) HTTPS Redirection — Uncomment in production
// app.UseHttpsRedirection();

// 🔹 Enable routing and authorization
app.UseAuthorization();

// 🔹 Map attribute-based controllers (e.g., /api/auth)
app.MapControllers();

// 🔹 Start the application
app.Run();
