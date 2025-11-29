using System;
using aladang_server_api.Configuration;
using aladang_server_api.Interface;
using aladang_server_api.Models;

namespace aladang_server_api.Services
{
	public class ProductImageService : IProductImage
	{
        private readonly IConfiguration _configuration;
        private AppDBContext _contex;
        double pageResult = 10;


        public ProductImageService(AppDBContext dbConnection, IConfiguration configuration)
        {
            _contex = dbConnection;
            _configuration = configuration;
             
        }

        public double Count()
        {
            return _contex.productImages!.Count();
        }

        public double PageCount()
        {
            return Math.Ceiling((double)_contex.productImages!.Count() / pageResult)!;
        }


        public List<ProductImage> GetAll()
        {
            var privacies = _contex.productImages!
                    .OrderByDescending(d => d.id)
                    .ToList();
            if (privacies != null)
            {
                return privacies!;
            }

            return null!;
        }


        public List<ProductImage> GetProductImages(int page)
        {
            if (page != 0)
            {
                var location = _contex.productImages!
                    .OrderByDescending(d => d.id)
                    .Skip((page - 1) * (int)pageResult)
                    .Take((int)pageResult)
                    .ToList();
                return location!;
            }

            return null!;
        }


        public ProductImage GetProductImageById(int id)
        {
            ProductImage productImage = _contex.productImages!.Where(l => l.id == id).SingleOrDefault()!;
            if (productImage != null)
            {
                return productImage;
            }
            return null!;

        }

        public ProductImage CreateNew(ProductImage req)
        {
            _contex.Add(req);
            _contex.SaveChanges();
            ProductImage result = _contex.productImages!.Where(u => u.id == req.id).FirstOrDefault()!;
            return result;

        }

        public ProductImage Update(ProductImage req)
        {
            ProductImage productImage = _contex.productImages!.FirstOrDefault(c => c.id == req.id)!;
            productImage.productid = req.productid;
            productImage.productimage = req.productimage;
            _contex.SaveChanges();
            ProductImage result = _contex.productImages!.Where(u => u.id == req.id).FirstOrDefault()!;
            if (result != null)
            {
                return result;
            }
            return null!;

        }
    }
}

