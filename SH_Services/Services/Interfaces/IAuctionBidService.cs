using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH_Services.Services.Interfaces
{
    public interface IAuctionBidService
    {
        Task<(bool Success, string Message)> PlaceBidAsync(Guid auctionPlantId, string userId, decimal bidAmount);
        void Dispose();
    }
}
