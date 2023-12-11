using MblexApp.Models;
using Plugin.InAppBilling;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MblexApp.StaticMethods
{
   public static class InAppBilling
    {
        public static async Task<ObservableCollection<Models.InAppBillingProduct>> GetProductInfoAsync(string[] productIds)
        {
            var productsCollection = new ObservableCollection<Models.InAppBillingProduct>();
            var connected = await CrossInAppBilling.Current.ConnectAsync();
            if(connected)
            {
                // Check if there are pending orders, if so then subscribe
                var purchasesInfo = await CrossInAppBilling.Current.GetProductInfoAsync(ItemType.Subscription, productIds);
                foreach (var item in purchasesInfo)
                {
                    //item info here.
                    // Assuming InAppBillingProduct has a constructor that takes relevant parameters
                    var product = new Models.InAppBillingProduct(item.ProductId, item.Description, item.Name, item.LocalizedPrice /* other parameters */);

                    // Add the product to the list
                    productsCollection.Add(product);
                }
                CrossInAppBilling.Current.DisconnectAsync();
                return productsCollection;
            }
           else 
            {              
                return productsCollection; }
        }
    }
}
