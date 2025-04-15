using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH_Services.Services.Hubs
{
    public class AuctionHub : Hub
    {
        public async Task JoinAuction(Guid auctionId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, auctionId.ToString());
        }

        public async Task LeaveAuction(Guid auctionId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, auctionId.ToString());
        }
    }
}
