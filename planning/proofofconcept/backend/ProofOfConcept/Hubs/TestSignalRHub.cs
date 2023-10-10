using Microsoft.AspNetCore.SignalR;
using ProofOfConcept.Entities;

namespace ProofOfConcept.Hubs
{
    public class TestSignalRHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task<TestResponse> TestMessage(TestMessage testMessage)
        {
            return await Task.FromResult(new TestResponse() { Message = "Hellow World" });
        }
    }
}
