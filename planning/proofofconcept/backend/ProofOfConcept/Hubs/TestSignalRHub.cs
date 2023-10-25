using Microsoft.AspNetCore.SignalR;
using ProofOfConcept.Entities;

namespace ProofOfConcept.Hubs
{
    /// <summary>
    /// Test SignalR Hub.
    /// </summary>
    public class TestSignalRHub : Hub
    {
        /// <inheritdoc/>
        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("UserJoined", this.Context.ConnectionId);
        }

        /// <summary>
        /// Send a message using 2 strings
        /// </summary>
        /// <param name="user">User string.</param>
        /// <param name="message">Message string.</param>
        /// <returns>An asynchronous task.</returns>
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        /// <summary>
        /// Send a message using TestMessage
        /// </summary>
        /// <param name="testMessage">The test message.</param>
        /// <returns>A response</returns>
        public async Task<TestResponse> TestMessage(TestMessage testMessage)
        {
            return await Task.FromResult(new TestResponse() { Message = "Hellow World" });
        }
    }
}
