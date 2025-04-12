using Ramayan_gita_app.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<UserDataAccess>();

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:5173")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

// 🔹 Enable Swagger (Always)
app.UseSwagger();
app.UseSwaggerUI();

// 🔹 Enable CORS
app.UseCors(MyAllowSpecificOrigins);

// 🔹 Optional HTTPS Redirection
// app.UseHttpsRedirection();

// 🔹 Authorization
app.UseAuthorization();

// 🔹 Map Controllers
app.MapControllers();

app.Run();
