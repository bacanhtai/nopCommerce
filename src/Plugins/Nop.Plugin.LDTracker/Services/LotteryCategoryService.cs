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
    public class LotteryCategoryService : ILotteryCategoryService
    {
        private readonly IRepository<LotteryCategory> _lotteryCategoryRepository;
        private readonly IEventPublisher _eventPublisher;
        private readonly IRepository<LotteryCustomerPrice> _lotteryCustomerPriceRepository;

        public LotteryCategoryService(IRepository<LotteryCategory> lotteryCategoryRepository, IEventPublisher eventPublisher, IRepository<LotteryCustomerPrice> lotteryCustomerPriceRepository)
        {
            _lotteryCategoryRepository = lotteryCategoryRepository;
            _eventPublisher = eventPublisher;
            _lotteryCustomerPriceRepository = lotteryCustomerPriceRepository;
        }

        public virtual void DeleteCategory(LotteryCategory category)
        {
            if(category == null)
                throw new ArgumentNullException("LotteryCategory");

            category.Active = false;
            UpdateCategory(category);

            //event notification
            _eventPublisher.EntityDeleted(category);
        }

        public virtual IPagedList<LotteryCategory> GetAllCategories(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var categories = _lotteryCategoryRepository.Table.ToList();

            //paging
            return new PagedList<LotteryCategory>(categories, pageIndex, pageSize);
        }

        public virtual LotteryCategory GetCategoryById(int categoryId)
        {
            if (categoryId == 0)
                return null;
                 
            var entity = _lotteryCategoryRepository.GetById(categoryId);
            return entity;
        }

        public virtual void InsertCategory(LotteryCategory category)
        {
            if(category == null)
                throw new ArgumentNullException("LotteryCategory");

            _lotteryCategoryRepository.Insert(category);

            //event notification
            _eventPublisher.EntityInserted(category);
        }

        public virtual void UpdateCategory(LotteryCategory category)
        {
            if (category == null)
                throw new ArgumentNullException("LotteryCategory");

            _lotteryCategoryRepository.Update(category);

            //event notification
            _eventPublisher.EntityUpdated(category);
        }

        #region Lottery Customer Price

        public virtual IPagedList<LotteryCustomerPrice> GetAllCustomerPriceByCustomerId(int customerId, int pageIndex = 0, int pageSize = int.MaxValue, bool isActived = true)
        {
            var query = from price in _lotteryCustomerPriceRepository.Table
                        from category in _lotteryCategoryRepository.Table
                        where price.CategoryId == category.Id
                            && price.Active == isActived
                            && price.CustomerId == customerId
                        orderby category.PriorityOrder
                        select price;

            var listPrice = new PagedList<LotteryCustomerPrice>(query, pageIndex, pageSize);
            return listPrice;
        }

        public virtual LotteryCustomerPrice GetCustomerPriceById(int customerPriceId)
        {
            if (customerPriceId == 0) return null;
            return _lotteryCustomerPriceRepository.GetById(customerPriceId);
        }

        public virtual void InsertCustomerPrice(LotteryCustomerPrice customerPrice)
        {
            if (customerPrice == null) throw new ArgumentNullException("lotteryCustomerPrice");
            _lotteryCustomerPriceRepository.Insert(customerPrice);

            //event notification
            _eventPublisher.EntityInserted(customerPrice);
        }

        public virtual void UpdateCustomerPrice(LotteryCustomerPrice customerPrice)
        {
            if (customerPrice == null) throw new ArgumentNullException("lotteryCustomerPrice");
            _lotteryCustomerPriceRepository.Update(customerPrice);

            //event notification
            _eventPublisher.EntityUpdated(customerPrice);
        }

        public virtual void DeleteCustomerPrice(LotteryCustomerPrice customerPrice)
        {
            if (customerPrice == null) throw new ArgumentNullException("lotteryCustomerPrice");
            customerPrice.Active = false;
            _lotteryCustomerPriceRepository.Update(customerPrice);

            //event notification
            _eventPublisher.EntityDeleted(customerPrice);
        }

        public virtual bool IsExistLotteryCustomerPrice(int customerId, int categoryId)
        {
            return _lotteryCustomerPriceRepository.Table.Any(o => o.CustomerId == customerId
                                                                && o.CategoryId == categoryId);
        }

        #endregion Lottery Customer Price
    }
}
