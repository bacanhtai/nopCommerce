using AutoMapper;
using Nop.Plugin.LDTracker.Domain;
using Nop.Plugin.LDTracker.Models;

namespace Nop.Plugin.LDTracker.Extensions.Mapper
{
    /// <summary>
    /// AutoMapper configuration
    /// </summary>
    public static class AutoMapperConfiguration
    {
        private static MapperConfiguration _mapperConfiguration;
        private static IMapper _mapper;

        /// <summary>
        /// Initialize mapper
        /// </summary>
        public static void Init()
        {
            _mapperConfiguration = new MapperConfiguration(cfg =>
            {
                //TODO remove 'CreatedOnUtc' ignore mappings because now presentation layer models have 'CreatedOn' property and core entities have 'CreatedOnUtc' property (distinct names)

                //address
                //cfg.CreateMap<Address, AddressModel>()
                //    .ForMember(dest => dest.AddressHtml, mo => mo.Ignore())
                //    .ForMember(dest => dest.CustomAddressAttributes, mo => mo.Ignore())
                //    .ForMember(dest => dest.FormattedCustomAddressAttributes, mo => mo.Ignore())
                //    .ForMember(dest => dest.AvailableCountries, mo => mo.Ignore())
                //    .ForMember(dest => dest.AvailableStates, mo => mo.Ignore())
                //    .ForMember(dest => dest.FirstNameEnabled, mo => mo.Ignore())
                //    .ForMember(dest => dest.FirstNameRequired, mo => mo.Ignore())
                //    .ForMember(dest => dest.LastNameEnabled, mo => mo.Ignore())
                //    .ForMember(dest => dest.LastNameRequired, mo => mo.Ignore())
                //    .ForMember(dest => dest.EmailEnabled, mo => mo.Ignore())
                //    .ForMember(dest => dest.EmailRequired, mo => mo.Ignore())
                //    .ForMember(dest => dest.CompanyEnabled, mo => mo.Ignore())
                //    .ForMember(dest => dest.CompanyRequired, mo => mo.Ignore())
                //    .ForMember(dest => dest.CountryEnabled, mo => mo.Ignore())
                //    .ForMember(dest => dest.StateProvinceEnabled, mo => mo.Ignore())
                //    .ForMember(dest => dest.CityEnabled, mo => mo.Ignore())
                //    .ForMember(dest => dest.CityRequired, mo => mo.Ignore())
                //    .ForMember(dest => dest.StreetAddressEnabled, mo => mo.Ignore())
                //    .ForMember(dest => dest.StreetAddressRequired, mo => mo.Ignore())
                //    .ForMember(dest => dest.StreetAddress2Enabled, mo => mo.Ignore())
                //    .ForMember(dest => dest.StreetAddress2Required, mo => mo.Ignore())
                //    .ForMember(dest => dest.ZipPostalCodeEnabled, mo => mo.Ignore())
                //    .ForMember(dest => dest.ZipPostalCodeRequired, mo => mo.Ignore())
                //    .ForMember(dest => dest.PhoneEnabled, mo => mo.Ignore())
                //    .ForMember(dest => dest.PhoneRequired, mo => mo.Ignore())
                //    .ForMember(dest => dest.FaxEnabled, mo => mo.Ignore())
                //    .ForMember(dest => dest.FaxRequired, mo => mo.Ignore())
                //    .ForMember(dest => dest.CountryName,
                //        mo => mo.MapFrom(src => src.Country != null ? src.Country.Name : null))
                //    .ForMember(dest => dest.StateProvinceName,
                //        mo => mo.MapFrom(src => src.StateProvince != null ? src.StateProvince.Name : null))
                //    .ForMember(dest => dest.CustomProperties, mo => mo.Ignore());                
               
                //Lottery Category
                cfg.CreateMap<LotteryCategory, LotteryCategoryModel>();
                cfg.CreateMap<LotteryCategoryModel, LotteryCategory>();
            });
            _mapper = _mapperConfiguration.CreateMapper();
        }

        /// <summary>
        /// Mapper
        /// </summary>
        public static IMapper Mapper
        {
            get
            {
                return _mapper;
            }
        }
        /// <summary>
        /// Mapper configuration
        /// </summary>
        public static MapperConfiguration MapperConfiguration
        {
            get
            {
                return _mapperConfiguration;
            }
        }
    }
}