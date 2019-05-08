using ShoppingEcommerce.Core.Repository;
using ShoppingEcommerce.DataAccess;
using ShoppingEcommerce.Services.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingEcommerce.Services.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductUnitOfWork _productUnitOfWork;
        private readonly IRepository<Product> _repository;
        private readonly ILoggingService _loggingService;

        public string CurrentCulture { get; set; }
    }
}
