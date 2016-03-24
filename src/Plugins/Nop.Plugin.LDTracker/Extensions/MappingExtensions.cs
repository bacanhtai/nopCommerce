using Nop.Plugin.LDTracker.Models;
using Nop.Plugin.LDTracker.Domain;
using Nop.Plugin.LDTracker.Extensions.Mapper;

namespace Nop.Plugin.LDTracker.Extensions
{
    public static class MappingExtensions
    {
        public static TDestination MapTo<TSource, TDestination>(this TSource source)
        {
            return AutoMapperConfiguration.Mapper.Map<TSource, TDestination>(source);
        }

        public static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
        {
            return AutoMapperConfiguration.Mapper.Map(source, destination);
        }

        public static LotteryCategoryModel ToModel(this LotteryCategory entity)
        {
            return entity.MapTo<LotteryCategory, LotteryCategoryModel>();
        }

        public static LotteryCategoryModel ToModel(this LotteryCategory entity, LotteryCategoryModel model)
        {
            return entity.MapTo(model);
        }

        public static LotteryCategory ToEntity(this LotteryCategoryModel model)
        {
            return model.MapTo<LotteryCategoryModel, LotteryCategory>();
        }

        public static LotteryCategory ToEntity(this LotteryCategoryModel model, LotteryCategory entity)
        {
            return model.MapTo(entity);
        }
    }
}
