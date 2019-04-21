using AutoMapper;

namespace ShoppingEcommerce.Mapper
{
    public interface IComplexMapping : IMapping
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mapperConfigurationExpression"></param>
        void CreateMap(IMapperConfigurationExpression mapperConfigurationExpression);
    }
}