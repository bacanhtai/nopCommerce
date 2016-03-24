using Nop.Core;
using Nop.Plugin.LDTracker.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.LDTracker.Services
{
    public partial interface ILotteryCustomerService
    {
        /// <summary>
        /// Gets all customers
        /// </summary>
        IPagedList<LotteryCustomer> GetAllCustomers(int pageIndex = 0, int pageSize = int.MaxValue);

        IList<LotteryCustomerPrice> GetAllCustomerPrice(bool isActived);

        /// <summary>
        /// Gets a customer
        /// </summary>
        /// <param name="customerId">customer identifier</param>
        /// <returns>customer</returns>
        LotteryCustomer GetCustomerById(int customerId);

        /// <summary>
        /// Inserts customer
        /// </summary>
        /// <param name="customer">customer</param>
        void InsertCustomer(LotteryCustomer customer);

        /// <summary>
        /// Updates the customer
        /// </summary>
        /// <param name="customer">customer</param>
        void UpdateCustomer(LotteryCustomer customer);

        /// <summary>
        /// Delete customer
        /// </summary>
        /// <param name="customer">customer</param>
        void DeleteCustomer(LotteryCustomer customer);
    }
}
