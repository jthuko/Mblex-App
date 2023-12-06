using MblexApp.Platforms.Android;
using MblexApp.Services;
using Plugin.InAppBilling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: Dependency(typeof(InAppBillingService))]
namespace MblexApp.Platforms.Android
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

        public async Task<bool> UnsubscribeAsync(string productId)
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

                // Check if the user is subscribed to this product
                var subscriptions = await billing.GetPurchasesAsync(ItemType.Subscription);
                var subscribedProduct = subscriptions.FirstOrDefault(p => p.ProductId == product.ProductId);

                if (subscribedProduct == null)
                {
                    // The user is not subscribed to this product
                    return false;
                }

                // Attempt to consume or cancel the subscription
                var unsubscribed = await billing.ConsumePurchaseAsync(subscribedProduct.PurchaseToken, subscribedProduct.TransactionIdentifier);

                if (unsubscribed)
                {
                    // The subscription was successfully canceled
                    return true;
                }
                else
                {
                    // Subscription cancellation failed
                    return false;
                }
            }
            catch (InAppBillingPurchaseException ex)
            {
                // Handle any in-app billing exceptions
                return false;
            }
        }

    }
}
