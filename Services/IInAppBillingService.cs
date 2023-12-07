using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MblexApp.Services
{
    public interface IInAppBillingService
    {
        Task<bool> PurchaseSubscriptionAsync(string productId);
        Task<bool> UnsubscribeAsync(string productId);
    }
}
