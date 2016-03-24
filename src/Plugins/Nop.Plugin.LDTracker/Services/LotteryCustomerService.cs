using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Plugin.LDTracker.Domain;
using Nop.Core.Data;
using Nop.Services.Events;

namespace Nop.Plugin.LDTracker.Services
{
    public class LotteryCustomerService : ILotteryCustomerService
    {
        private readonly IRepository<LotteryCustomer> _lotteryRepository;
        private readonly IEventPublisher _eventPublisher;

        public LotteryCustomerService(IRepository<LotteryCustomer> lotteryRepository, IEventPublisher eventPublisher)
        {
            _lotteryRepository = lotteryRepository;
            _eventPublisher = eventPublisher;
        }

        public virtual void DeleteCustomer(LotteryCustomer customer)
        {
            if(customer == null)
                throw new ArgumentNullException("LotteryCustomer");

            customer.Active = false;
            UpdateCustomer(customer);

            //event notification
            _eventPublisher.EntityDeleted(customer);
        }

        public virtual IPagedList<LotteryCustomer> GetAllCustomers(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var customers = _lotteryRepository.Table.ToList();

            //paging
            return new PagedList<LotteryCustomer>(customers, pageIndex, pageSize);
        }

        public IList<LotteryCustomerPrice> GetAllCustomerPrice(bool isActived = true)
        {

            throw new NotImplementedException();
        }

        public virtual LotteryCustomer GetCustomerById(int customerId)
        {
            if (customerId == 0)
                return null;
                 
            var entity = _lotteryRepository.GetById(customerId);
            return entity;
        }

        public virtual void InsertCustomer(LotteryCustomer customer)
        {
            if(customer == null)
                throw new ArgumentNullException("LotteryCustomer");

            _lotteryRepository.Insert(customer);

            //event notification
            _eventPublisher.EntityInserted(customer);
        }

        public virtual void UpdateCustomer(LotteryCustomer customer)
        {
            if (customer == null)
                throw new ArgumentNullException("LotteryCustomer");

            _lotteryRepository.Update(customer);

            //event notification
            _eventPublisher.EntityUpdated(customer);
        }
    }
}
