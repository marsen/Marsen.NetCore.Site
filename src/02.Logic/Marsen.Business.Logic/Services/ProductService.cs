using Marsen.Business.Logic.Entities;
using Marsen.Business.Logic.Interface;

namespace Marsen.Business.Logic.Services
{
    public partial class ProductService 
    {
        /// <summary>
        /// The Product Service
        /// </summary>
        private readonly IProductStorage _productStorage;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductService" /> class.
        /// </summary>
        public ProductService(IProductStorage productStorage)
        {
            this._productStorage = productStorage;
        }

        /// <summary>
        /// Reads the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ProductEntity</returns>
        public ProductEntity Read(long id)
        {
            return this._productStorage.Read(id);
        }
    }
}