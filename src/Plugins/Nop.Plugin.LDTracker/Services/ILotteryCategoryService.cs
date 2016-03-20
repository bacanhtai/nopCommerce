using Nop.Core;
using Nop.Plugin.LDTracker.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.LDTracker.Services
{
    public partial interface ILotteryCategoryService
    {
        /// <summary>
        /// Gets all categories
        /// </summary>
        IPagedList<LotteryCategory> GetAllCategories(int pageIndex = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// Gets a category
        /// </summary>
        /// <param name="categoryId">Category identifier</param>
        /// <returns>Category</returns>
        LotteryCategory GetCategoryById(int categoryId);

        /// <summary>
        /// Inserts category
        /// </summary>
        /// <param name="category">Category</param>
        void InsertCategory(LotteryCategory category);

        /// <summary>
        /// Updates the category
        /// </summary>
        /// <param name="category">Category</param>
        void UpdateCategory(LotteryCategory category);

        /// <summary>
        /// Delete category
        /// </summary>
        /// <param name="category">Category</param>
        void DeleteCategory(LotteryCategory category);
    }
}
