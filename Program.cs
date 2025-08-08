using PixSignalRTestApi.Hubs;

var builder = WebApplication.CreateBuilder(args);

// 🔊 (Opcional) Forçar Kestrel a ouvir em 0.0.0.0:5018
builder.WebHost.UseUrls("http://0.0.0.0:5018");

// Services
builder.Services.AddSignalR();
builder.Services.AddControllers();

// CORS liberado para testes (com credenciais)
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy
            .AllowAnyHeader()
            .AllowAnyMethod()
            .SetIsOriginAllowed(_ => true) // qualquer origem (somente DEV!)
            .AllowCredentials();
    });
});

// (Opcional) Se criou Swagger:
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors();

// (Opcional) Em dev, desligue HTTPS redirection pra evitar dor de cabeça no simulador
// app.UseHttpsRedirection();

// (Opcional) Swagger
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

// Rotas
app.MapControllers();
app.MapHub<PixHub>("/pixhub");

// Health-check simples (GET /)
app.MapGet("/", () => "Pix SignalR Test API rodando.");

app.Run();
