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
        private readonly IRepository<LotteryCategory> _lotteryRepository;
        private readonly IEventPublisher _eventPublisher;

        public LotteryCategoryService(IRepository<LotteryCategory> lotteryRepository, IEventPublisher eventPublisher)
        {
            _lotteryRepository = lotteryRepository;
            _eventPublisher = eventPublisher;
        }

        public virtual void DeleteCategory(LotteryCategory category)
        {
            if(category == null)
                throw new ArgumentNullException("LotteryCategory");

            category.Active = false;
            UpdateCategory(category);
        }

        public virtual IPagedList<LotteryCategory> GetAllCategories(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var categories = _lotteryRepository.Table.ToList();

            //paging
            return new PagedList<LotteryCategory>(categories, pageIndex, pageSize);
        }

        public virtual LotteryCategory GetCategoryById(int categoryId)
        {
            if (categoryId == 0)
                return null;
                 
            var entity = _lotteryRepository.GetById(categoryId);
            return entity;
        }

        public virtual void InsertCategory(LotteryCategory category)
        {
            if(category == null)
                throw new ArgumentNullException("LotteryCategory");

            _lotteryRepository.Insert(category);

            //event notification
            _eventPublisher.EntityInserted(category);
        }

        public virtual void UpdateCategory(LotteryCategory category)
        {
            if (category == null)
                throw new ArgumentNullException("LotteryCategory");

            _lotteryRepository.Update(category);

            //event notification
            _eventPublisher.EntityUpdated(category);
        }
    }
}
