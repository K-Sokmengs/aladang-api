/*using System;
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

*/

using System;
using System.Collections.Generic;
using System.Linq;
using aladang_server_api.Configuration;
using aladang_server_api.Interface;
using aladang_server_api.Models;
using Microsoft.Extensions.Configuration;

namespace aladang_server_api.Services
{
    public class ProductImageService : IProductImage
    {
        private readonly IConfiguration _configuration;
        private AppDBContext _context;
        double pageResult = 10;

        public ProductImageService(AppDBContext dbConnection, IConfiguration configuration)
        {
            _context = dbConnection;
            _configuration = configuration;
        }

        public double Count()
        {
            return _context.productImages?.Count() ?? 0;
        }

        public double PageCount()
        {
            var count = _context.productImages?.Count() ?? 0;
            return Math.Ceiling(count / pageResult);
        }

        public List<ProductImage> GetAll()
        {
            var productImages = _context.productImages?
                    .OrderByDescending(d => d.id)
                    .ToList();

            return productImages ?? new List<ProductImage>();
        }

        public List<ProductImage> GetProductImages(int page)
        {
            if (page <= 0) return new List<ProductImage>();

            var productImages = _context.productImages?
                .OrderByDescending(d => d.id)
                .Skip((page - 1) * (int)pageResult)
                .Take((int)pageResult)
                .ToList();

            return productImages ?? new List<ProductImage>();
        }

        public ProductImage GetProductImageById(int id)
        {
            var productImage = _context.productImages?
                .FirstOrDefault(l => l.id == id);

            return productImage;
        }

        public ProductImage CreateNew(ProductImage req)
        {
            _context.Add(req);
            _context.SaveChanges();
            var result = _context.productImages?
                .FirstOrDefault(u => u.id == req.id);
            return result;
        }

        public ProductImage Update(ProductImage req)
        {
            // FIX: Use FirstOrDefault instead of Where + SingleOrDefault for better performance
            var productImage = _context.productImages?
                .FirstOrDefault(c => c.id == req.id);

            if (productImage == null)
                return null;

            // Update the properties
            productImage.productid = req.productid;
            productImage.productimage = req.productimage;

            // FIX: Mark the entity as modified and save changes
            _context.Update(productImage);
            _context.SaveChanges();

            // Return the updated entity
            var result = _context.productImages?
                .FirstOrDefault(u => u.id == req.id);

            return result;
        }
    }
}