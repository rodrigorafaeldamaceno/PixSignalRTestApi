using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PixSignalRTestApi.Hubs;

namespace PixSignalRTestApi.Controllers;

[ApiController]
[Route("[controller]")]
public class SimulacaoController : ControllerBase
{
    private readonly IHubContext<PixHub> _hubContext;

    public SimulacaoController(IHubContext<PixHub> hubContext)
    {
        _hubContext = hubContext;
    }

    [HttpPost("disparar")]
    public async Task<IActionResult> DispararPix()
    {
        var eventoPix = new {
            id = Guid.NewGuid().ToString(),
            valor = 200.0,
            pagador = "Pagador via API",
            data = DateTime.UtcNow
        };

        await _hubContext.Clients.All.SendAsync("PagamentoRecebido", eventoPix);

        return Ok("Evento enviado");
    }
}
