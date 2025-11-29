using System;
using System.Net.NetworkInformation;
using aladang_server_api.Configuration;
using aladang_server_api.Helpers;
using aladang_server_api.Interface;
using aladang_server_api.Models;
using aladang_server_api.Models.BO.Req;
using aladang_server_api.Models.BO.Res;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace aladang_server_api.Services
{
    public class ProductService : IProduct
    {
        private readonly IConfiguration _configuration;
        private string defaultUrlNoImg = "";
        private string basicUrlImg = "", appDomain = "";
        private AppDBContext _contex;
        double pageResult = 10;
        public ProductService(AppDBContext dbConnection, IConfiguration configuration)
        {
            _contex = dbConnection;
            _configuration = configuration;

            appDomain = _configuration.GetSection("Application:AppDomain").Value;
            basicUrlImg = appDomain + ProviderConnector.Image;
            defaultUrlNoImg = appDomain + ProviderConnector.ImageDefaultNoImg;
        }

        public double Count()
        {
            return _contex.products!.Count();
        }

        public double Count(string status)
        {
            return _contex.products!.Where(p => p.Status == status).Count();
        }

        public double Count(int shopid, string status)
        {
            if (status.ToLower() == "all")
            {
                return _contex.products!.Where(p => p.ShopId == shopid).Count();
            }
            return _contex.products!.Where(p => p.ShopId == shopid).Where(s=>s.Status==status).Count();
        }

        public double Count(int shopid)
        {
            return _contex.products!.Where(p => p.ShopId == shopid).Count();
        }

        public double PageCount()
        {
            return Math.Ceiling((double)_contex.products!.Count() / pageResult)!;
        }

        public double PageCount(string status)
        {
            return Math.Ceiling((double)_contex.products!.Where(p => p.Status == status).Count() / pageResult)!;
        }

        public double PageCount(int shopid, string status)
        {
            if (status.ToLower() == "all")
            {
                return Math.Ceiling((double)_contex.products!.Where(p => p.ShopId == shopid).Count() / pageResult)!;
            }
            return Math.Ceiling((double)_contex.products!.Where(p => p.ShopId == shopid).Where(s => s.Status == status).Count() / pageResult)!;
        }

        public List<ProductRes> GetAll()
        {
            var products = _contex.products!
               .OrderByDescending(d => d.id) 
               .ToList();
            var productResList = new List<ProductRes>();
            products.ForEach(product =>
            {
                var productRes = new ProductRes();
                productRes.SetProductCreate(product); 
                var productImageDetailResList = new List<ProductImageDetailRes>();
                var listImage = _contex.ProductImageDetails!.Where(c => c.ProductId == product.id).ToList();
                listImage.ForEach(image =>
                {
                    var productImageDetail = new ProductImageDetailRes();
                    productImageDetail.setData(image);
                    productImageDetailResList.Add(productImageDetail);
                });
                productRes.ProductImageDetails = productImageDetailResList;
                productResList.Add(productRes);

            });
            return productResList!;
        }

        public List<ProductRes> GetProduct(int page)
        {
            if (page != 0)
            {
                var products = _contex.products!
                .OrderByDescending(d => d.id)
                .Skip((page - 1) * (int)pageResult)
                .Take((int)pageResult)
                .ToList();
                var productResList = new List<ProductRes>();
                products.ForEach(product =>
                {
                    var productRes = new ProductRes();
                    productRes.SetProductCreate(product); 
                    var productImageDetailResList = new List<ProductImageDetailRes>();
                    var listImage = _contex.ProductImageDetails!.Where(c => c.ProductId == product.id).ToList();
                    listImage.ForEach(image =>
                    {
                        var productImageDetail = new ProductImageDetailRes();
                        productImageDetail.setData(image);
                        productImageDetailResList.Add(productImageDetail);
                    });
                    productRes.ProductImageDetails = productImageDetailResList;
                    productResList.Add(productRes);

                });
                return productResList!;
            }

            return null!;
        }


        public List<ProductRes> GetByShopId(int shopid,int page, string status)
        {
            var productResList = new List<ProductRes>();
            if (page != 0)
            {
                if (status == "all")
                {
                    var products = _contex.products!
                        .Where(s => s.ShopId == shopid)
                        .OrderByDescending(d => d.id)
                        .Skip((page - 1) * (int)pageResult) 
                        .Take((int)pageResult)
                        .ToList();
                   
                    products.ForEach(product =>
                    {
                        var productRes = new ProductRes();
                        productRes.SetProductCreate(product); 
                        var productImageDetailResList = new List<ProductImageDetailRes>();
                        var listImage = _contex.ProductImageDetails!.Where(c => c.ProductId == product.id).ToList();
                        listImage.ForEach(image =>
                        {
                            var productImageDetail = new ProductImageDetailRes();
                            productImageDetail.setData(image);
                            productImageDetailResList.Add(productImageDetail);
                        });
                        productRes.ProductImageDetails = productImageDetailResList;
                        productResList.Add(productRes);

                    });
                    return productResList!;
                }
                else
                {
                    var products = _contex.products!
                        .Where(s => s.ShopId == shopid)
                        .Where(s => s.Status == status)
                        .OrderByDescending(d => d.id)
                        .Skip((page - 1) * (int)pageResult)
                        .Take((int)pageResult) 
                        .ToList();
                    products.ForEach(product =>
                    {
                        var productRes = new ProductRes();
                        productRes.SetProductCreate(product); 
                        var productImageDetailResList = new List<ProductImageDetailRes>();
                        var listImage = _contex.ProductImageDetails!.Where(c => c.ProductId == product.id).ToList();
                        listImage.ForEach(image =>
                        {
                            var productImageDetail = new ProductImageDetailRes();
                            productImageDetail.setData(image);
                            productImageDetailResList.Add(productImageDetail);
                        });
                        productRes.ProductImageDetails = productImageDetailResList;
                        productResList.Add(productRes);

                    });
                    return  productResList;
                }
              
            }

            return null!;
        }

        public List<ProductRes> GetByShopId(int shopid)
        {
            if (shopid != 0)
            { 
                var products = _contex.products!
                    .Where(s => s.ShopId == shopid) 
                    .Take((int)pageResult)
                    .ToList();
                var productResList = new List<ProductRes>();
                products.ForEach(product =>
                {
                    var productRes = new ProductRes();
                    productRes.SetProductCreate(product); 
                    var productImageDetailResList = new List<ProductImageDetailRes>();
                    var listImage = _contex.ProductImageDetails!.Where(c => c.ProductId == product.id).ToList();
                    listImage.ForEach(image =>
                    {
                        var productImageDetail = new ProductImageDetailRes();
                        productImageDetail.setData(image);
                        productImageDetailResList.Add(productImageDetail);
                    });
                    productRes.ProductImageDetails = productImageDetailResList;
                    productResList.Add(productRes);

                });
                return productResList!;
            }

            return null!;
        }

        public ProductRes GetById(int id)
        {
            var productRes = new ProductRes();
            var product = _contex.products!.FirstOrDefault(l => l.id == id)!;
            {
                productRes.SetProductCreate(product); 
                var productImageDetailResList = new List<ProductImageDetailRes>();
                var listImage = _contex.ProductImageDetails!.Where(c => c.ProductId == product.id).ToList();
                listImage.ForEach(image =>
                {
                    var productImageDetail = new ProductImageDetailRes();
                    productImageDetail.setData(image);
                    productImageDetailResList.Add(productImageDetail);
                });
                productRes.ProductImageDetails = productImageDetailResList;
                return productRes;
            }

        }


        public List<ProductRes> GetByStatus(string status,int page)
        {
            if (page != 0)
            {
                var products = _contex.products!
                    .Where(p => p.Status == status)
                    .OrderByDescending(d => d.id)
                    .Skip((page - 1) * (int)pageResult)
                    .Take((int)pageResult)
                
                    .ToList();
                var productResList = new List<ProductRes>();
                products.ForEach(product =>
                {
                    var productRes = new ProductRes();
                    productRes.SetProductCreate(product); 
                    var productImageDetailResList = new List<ProductImageDetailRes>();
                    var listImage = _contex.ProductImageDetails!.Where(c => c.ProductId == product.id).ToList();
                    listImage.ForEach(image =>
                    {
                        var productImageDetail = new ProductImageDetailRes();
                        productImageDetail.setData(image);
                        productImageDetailResList.Add(productImageDetail);
                    });
                    productRes.ProductImageDetails = productImageDetailResList;
                    productResList.Add(productRes);

                });
                return productResList!;
            }

            //List<Product> product = _contex.products!.Where(p =>p.Status==status).ToList();
            //if (product != null)
            //{
            //    return product;
            //}
            return null!;

        }

        public Product CreateNew(ProductReq req)
        {
            Product create = new Product();
            create.SetProductCreate(req);
            _contex.Add(create);
            _contex.SaveChanges();
            var result = _contex.products!.FirstOrDefault(u => u.id == create.id)!;
            if (result.id != 0)
            {
                req.ProductImageDetails.ForEach(data =>
                {
                    var productImageDetail = new ProductImageDetail();
                    productImageDetail.setData(data);
                    productImageDetail.ProductId = result.id;
                    _contex.Add(productImageDetail);
                    _contex.SaveChanges();
                });
            }

            return result;

        }

        public Product Update(ProductReq req)
        {
            Product product = _contex.products!.FirstOrDefault(c => c.id == req.Id)!;
            // product.ShopId = req.ShopId;
            // product.ProductCode = req.ProductCode;
            // product.ProductName = req.ProductName;
            // product.Description = req.Description;
            // product.QtyInStock = req.QtyInStock;
            // product.Price = req.Price;
            // product.NewPrice = req.NewPrice;
            // product.CurrencyId = req.CurrencyId;
            // product.CutStockType = req.CutStockType;
            // product.ExpiredDate = req.ExpiredDate;
            // product.LinkVideo = req.LinkVideo;
            // product.ImageThumbnail = req.ImageThumbnail;
            // product.Status = req.Status;
            product.SetProductCreate(req);
            _contex.Update(product);
            _contex.SaveChanges();
            if (product.id != 0)
            {
                req.ProductImageDetails.ForEach(data =>
                {
                    var findById = _contex.ProductImageDetails!.FirstOrDefault(c=>c.Id==data.Id);
                    if (findById != null)
                    {
                        findById.setData(data);
                        _contex.Update(findById);
                        _contex.SaveChanges();
                    }
                    else
                    {
                        var productImageDetail = new ProductImageDetail();
                        productImageDetail.setData(data);
                        productImageDetail.ProductId = product.id;
                        _contex.Add(productImageDetail);
                        _contex.SaveChanges();
                    }
                 
                });
            }
            return product!;

        }

    }
}

