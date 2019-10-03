using System.Linq;
using AutoMapper;
using Marsen.Business.Logic.Entities;
using Marsen.Business.Logic.Interface;
using Marsen.NetCore.DA.Models;

namespace Marsen.NetCore.DA.Storage
{
    /// <summary>
    /// ProductStorage
    /// </summary>
    /// <seealso cref="IProductStorage" />
    public class ProductStorage:IProductStorage
    {
        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductStorage" /> class.
        /// </summary>
        public ProductStorage(IMapper mapper)
        {
            this._mapper = mapper;
        }

        /// <summary>Reads the specified identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ProductEntity </returns>
        public ProductEntity Read(long id)
        {
            using (var context=new MARSContext())
            {
                var result = context.Product.FirstOrDefault(x => x.ProductId == id);
                return this._mapper.Map<ProductEntity>(result);
            }
        }
    }
}
