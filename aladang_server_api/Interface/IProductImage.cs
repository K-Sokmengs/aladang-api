using System;
using aladang_server_api.Models;

namespace aladang_server_api.Interface
{
	public interface IProductImage
	{
        public double Count();
        public double PageCount();

        public List<ProductImage> GetAll();
        public List<ProductImage> GetProductImages(int page);
        public ProductImage GetProductImageById(int id);
        public ProductImage Update(ProductImage obj);
        public ProductImage CreateNew(ProductImage req);
    }
}

