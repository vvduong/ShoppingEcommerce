using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using AutoMapper;

namespace ShoppingEcommerce.Web
{
    public static class MapperConfig
    {
        public static void RegisterMapping()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
               // cfg.AddProfile<DepartmentProfile>();
               // cfg.AddProfile<DepartmentTypeProfile>();
            
            });
        }

    }
}