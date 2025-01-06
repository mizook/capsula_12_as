var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Middleware para simular fallas aleatorias
// app.Use(async (context, next) =>
// {
//     var random = new Random();
//     if (random.Next(0, 3) == 0) // 33% de probabilidades de fallar
//     {
//         Console.WriteLine("API A: Fallo simulado en la solicitud.");
//         context.Response.StatusCode = 500;
//         await context.Response.WriteAsync("Simulated failure");
//         return;
//     }

//     await next.Invoke();
// });


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
