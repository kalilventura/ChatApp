using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace ChatApp.Models
{
    public enum ChatType
    {
        Room,
        Private
    }
    public class Chat
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ChatType Type { get; set; }
        public ICollection<Message> Messages { get; set; } = new List<Message>();
        public ICollection<ChatUser> Users { get; set; } = new List<ChatUser>();
    }

    public class Message
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public DateTime Timestamp { get; set; }

        public int ChatId { get; set; }
        public virtual Chat Chat { get; set; }
    }

    public class User : IdentityUser
    {
        public ICollection<ChatUser> Chats { get; set; }
    }
}