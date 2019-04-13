using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;

namespace ShoppingEcommerce.Web
{
    public class ChatHub : Hub
    {
        public ChatHub()
        {
            SystemConstants.CurrentChatHub = this;
        }

        public Task Send(string roomName, string userId, string userName, string departmentName, string message)
        {
            return Clients.Group(roomName).addMessage(userId, userName, departmentName, message, DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
        }

        public Task Send(string roomName, string userId, string message)
        {
            return Clients.Group(roomName).addMessage(userId, message, DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
        }

        public Task SendFile(string roomName, string userId, string userName, string departmentName, string message, string fileId, string fileExt)
        {
            return Clients.Group(roomName).addFile(userId, userName, departmentName, message, fileId, fileExt, DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
        }

        public Task Connect(string roomName)
        {
            return Groups.Add(Context.ConnectionId, roomName);
        }

        public Task Disconnect(string roomName)
        {
            return Groups.Remove(Context.ConnectionId, roomName);
        }

        public Task SendNotification(string userId, Guid notificationID, Guid senderID, string senderFullName, string action, string summary)
        {
            return Clients.User(userId).addNotification(notificationID, senderID, senderFullName, action, summary, DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
        }
    }

    public class UserIdProvider : IUserIdProvider
    {
        public string GetUserId(IRequest request)
        {
            var userId = HttpContext.Current.User.Identity.Name;
            return userId.ToString();
        }
    }
}
