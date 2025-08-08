using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace PixSignalRTestApi.Hubs
{
    public class PixHub : Hub
    {
        public async Task SimularPix()
        {
            var eventoPix = new {
                id = Guid.NewGuid().ToString(),
                valor = 50.0,
                pagador = "Usuário de Teste",
                data = DateTime.UtcNow
            };

            await Clients.All.SendAsync("PixStatusChanged", eventoPix);
        }
    }
}
