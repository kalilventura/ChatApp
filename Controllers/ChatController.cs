using System;
using System.Threading.Tasks;
using ChatApp.Database;
using ChatApp.Hubs;
using ChatApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class ChatController : Controller
    {
        private IHubContext<ChatHub> _chat;

        public ChatController(IHubContext<ChatHub> hub)
        {
            _chat = hub;
        }

        [HttpPost("[action]/{connectionId}/{roomId}")]
        public async Task<IActionResult> JoinRoom(string connectionId, string roomId)
        {
            await _chat.Groups.AddToGroupAsync(connectionId, roomId);
            return Ok();
        }

        [HttpPost("[action]/{connectionId}/{roomId}")]
        public async Task<IActionResult> LeaveRoom(string connectionId, string roomId)
        {
            await _chat.Groups.AddToGroupAsync(connectionId, roomId);
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> SendMessage(string message,
                                                    int roomId,
                                                    [FromServices] AppDbContext _context)
        {
            var Message = new Message
            {
                ChatId = roomId,
                Text = message,
                Name = User.Identity.Name,
                Timestamp = DateTime.Now
            };

            _context.Messages.Add(Message);
            await _context.SaveChangesAsync();

            await _chat.Clients.Group(roomId.ToString()).SendAsync("ReceiveMessage", new
            {
                Text = Message.Text,
                Name = Message.Name,
                Timestamp = Message.Timestamp.ToString("dd/MM/yyyy hh:mm:ss")
            });

            return Ok();
        }
    }
}