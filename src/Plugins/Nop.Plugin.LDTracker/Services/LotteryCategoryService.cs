using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Plugin.LDTracker.Domain;
using Nop.Core.Data;

namespace Nop.Plugin.LDTracker.Services
{
    public class LotteryCategoryService : ILotteryCategoryService
    {
        private IRepository<LotteryCategory> _lotteryRepository;

        public LotteryCategoryService(IRepository<LotteryCategory> lotteryRepository)
        {
            _lotteryRepository = lotteryRepository;
        }

        public virtual void DeleteCategory(LotteryCategory category)
        {
            throw new NotImplementedException();
        }

        public virtual IPagedList<LotteryCategory> GetAllCategories(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var categories = _lotteryRepository.Table.ToList();

            //paging
            return new PagedList<LotteryCategory>(categories, pageIndex, pageSize);
        }

        public virtual LotteryCategory GetCategoryById(int categoryId)
        {
            throw new NotImplementedException();
        }

        public virtual void InsertCategory(LotteryCategory category)
        {
            throw new NotImplementedException();
        }

        public virtual void UpdateCategory(LotteryCategory category)
        {
            throw new NotImplementedException();
        }
    }
}
