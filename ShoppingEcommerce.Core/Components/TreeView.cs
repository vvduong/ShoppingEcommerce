using System;
using AutoMapper;
using ShoppingEcommerce.DataAccess;
using ShoppingEcommerce.Mapper;

namespace ShoppingEcommerce.Core.Components
{
    public class TreeView : IComplexMapping
    {
        public string Id { get; set; }

        public string ParentId { get; set; }

        public string Text { get; set; }

        public bool? HasChild { get; set; }

        public bool ShouldSerializeParentId() => ParentId != null && ParentId != Guid.Empty.ToString();

        public bool ShouldSerializeHasChild() => HasChild == true;

        // JUST FOR TESTING
        public void CreateMap(IMapperConfigurationExpression configuration)
        {
            //configuration
            //    .CreateMap<CategorizeDocument, TreeView>()
            //    .ForMember(
            //        destinationMember => destinationMember.Id, 
            //        memberOptions => memberOptions.MapFrom(sourceMember => sourceMember.ID.ToString()))
            //    .ForMember(
            //        destinationMember => destinationMember.ParentId,
            //        memberOptions => memberOptions.MapFrom(sourceMember => sourceMember.ParentID.ToString()))
            //    .ForMember(
            //        destinationMember => destinationMember.Text,
            //        memberOptions => memberOptions.MapFrom(sourceMember => sourceMember.Name));
        }
    }
}
