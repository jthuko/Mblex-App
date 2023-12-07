using MblexApp.Platforms.iOS;
using MblexApp.Services;
using Plugin.InAppBilling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: Dependency(typeof(InAppBillingService))]
namespace MblexApp.Platforms.iOS
{
    public class InAppBillingService : IInAppBillingService
    {
        public async Task<bool> PurchaseSubscriptionAsync(string productId)
        {
            try
            {
                var billing = CrossInAppBilling.Current;

                // Connect to the billing service
                var connected = await billing.ConnectAsync();

                if (!connected)
                {
                    // Failed to connect to the billing service
                    return false;
                }

                // Query the product info for the specified productId
                var products = await billing.GetProductInfoAsync(ItemType.Subscription, new string[] { productId });

                var product = products.FirstOrDefault();

                if (product == null)
                {
                    // Product information not available
                    return false;
                }

                // Attempt to purchase the subscription
                var purchase = await billing.PurchaseAsync(product.ProductId, ItemType.Subscription, "payload");

                if (purchase == null)
                {
                    // Purchase failed
                    return false;
                }

                if (purchase.State == PurchaseState.Purchased)
                {
                    // The purchase was successful
                    return true;
                }
                else
                {
                    // Purchase was not successful
                    return false;
                }
            }
            catch (InAppBillingPurchaseException ex)
            {
                // Handle any in-app billing exceptions
                return false;
            }
        }

        Task<bool> IInAppBillingService.UnsubscribeAsync(string productId)
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<InAppBillingProduct>> GetProductInfoAsync(ItemType itemType, params string[] productIds)
        {
            try
            {
                var billing = CrossInAppBilling.Current;

                // Connect to the billing service
                var connected = await billing.ConnectAsync();

                if (!connected)
                {
                    // Failed to connect to the billing service
                    return null;
                }

                // Query the product info for the specified productIds
                var products = await billing.GetProductInfoAsync(itemType, productIds);

                return products;
            }
            catch (InAppBillingPurchaseException ex)
            {
                // Handle any in-app billing exceptions
                return null;
            }
        }
    }
}
