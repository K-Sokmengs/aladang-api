using System;
using System.Collections.Generic;
using aladang_server_api.Interface;
using aladang_server_api.Models.BO.Req;
using aladang_server_api.Models.BO.Res;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace aladang_server_api.Controllers
{
    [Route("app/v1/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProduct _productService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProduct productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            try
            {
                _logger.LogInformation("Getting all products");
                List<ProductRes> result = _productService.GetAll();

                return new OkObjectResult(new
                {
                    code = 200,
                    message = "Data Successfully",
                    count = _productService.Count(),
                    countPage = _productService.PageCount(),
                    currentPage = 1,
                    data = result
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all products");
                return new ObjectResult(new
                {
                    code = 500,
                    message = "Internal Server Error: " + ex.Message
                })
                { StatusCode = 500 };
            }
        }

        [HttpGet("all/{page}")]
        public IActionResult Get(int page)
        {
            try
            {
                _logger.LogInformation("Getting products for page {Page}", page);
                List<ProductRes> result = _productService.GetProduct(page);

                return new OkObjectResult(new
                {
                    code = 200,
                    message = "Data Successfully",
                    count = _productService.Count(),
                    countPage = _productService.PageCount(),
                    currentPage = page,
                    data = result
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting products for page {Page}", page);
                return new ObjectResult(new
                {
                    code = 500,
                    message = "Internal Server Error: " + ex.Message
                })
                { StatusCode = 500 };
            }
        }

        [HttpGet("shop/{shopid}")]
        public IActionResult GetByShopId(int shopid)
        {
            try
            {
                _logger.LogInformation("Getting products for shop {ShopId}", shopid);
                List<ProductRes> result = _productService.GetByShopId(shopid);

                return new OkObjectResult(new
                {
                    code = 200,
                    message = "Data Successfully",
                    count = _productService.Count(shopid),
                    countPage = 1,
                    currentPage = 1,
                    data = result
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting products for shop {ShopId}", shopid);
                return new ObjectResult(new
                {
                    code = 500,
                    message = "Internal Server Error: " + ex.Message
                })
                { StatusCode = 500 };
            }
        }

        [HttpGet("shop/{shopid}/{page}/{status}")]
        public IActionResult GetByShopId(int shopid, int page, string status)
        {
            try
            {
                _logger.LogInformation("Getting products for shop {ShopId}, page {Page}, status {Status}", shopid, page, status);
                List<ProductRes> result = _productService.GetByShopId(shopid, page, status);

                return new OkObjectResult(new
                {
                    code = 200,
                    message = "Data Successfully",
                    count = _productService.Count(shopid, status),
                    countPage = _productService.PageCount(shopid, status),
                    currentPage = page,
                    data = result
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting products for shop {ShopId}, page {Page}, status {Status}", shopid, page, status);
                return new ObjectResult(new
                {
                    code = 500,
                    message = "Internal Server Error: " + ex.Message
                })
                { StatusCode = 500 };
            }
        }

        [HttpGet("status/{status}/{page}")]
        public IActionResult GetByStatus(string status, int page)
        {
            try
            {
                _logger.LogInformation("Getting products with status {Status} for page {Page}", status, page);
                List<ProductRes> result = _productService.GetByStatus(status, page);

                if (result != null && result.Count > 0)
                {
                    return new OkObjectResult(new
                    {
                        code = 200,
                        message = "Data Successfully",
                        count = _productService.Count(status),
                        countPage = _productService.PageCount(status),
                        currentPage = page,
                        data = result
                    });
                }

                return new NotFoundObjectResult(new
                {
                    code = 404,
                    message = "Data Not Found!",
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting products with status {Status} for page {Page}", status, page);
                return new ObjectResult(new
                {
                    code = 500,
                    message = "Internal Server Error: " + ex.Message
                })
                { StatusCode = 500 };
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                _logger.LogInformation("Getting product by ID {ProductId}", id);
                var result = _productService.GetById(id);

                if (result != null)
                {
                    return new OkObjectResult(new
                    {
                        code = 200,
                        message = "Data Successfully",
                        count = 1,
                        countPage = 1,
                        currentPage = 1,
                        data = result
                    });
                }

                return new NotFoundObjectResult(new
                {
                    code = 404,
                    message = "Data Not Found!",
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting product by ID {ProductId}", id);
                return new ObjectResult(new
                {
                    code = 500,
                    message = "Internal Server Error: " + ex.Message
                })
                { StatusCode = 500 };
            }
        }

        [HttpPost("create")]
        [Authorize]
        public IActionResult CreateProduct([FromBody] ProductReq req)
        {
            try
            {
                _logger.LogInformation("CreateProduct endpoint called");

                if (req == null)
                {
                    _logger.LogWarning("CreateProduct: Request body is null");
                    return new BadRequestObjectResult(new
                    {
                        code = 400,
                        message = "Request body cannot be null"
                    });
                }

                // Basic validation
                if (!req.ShopId.HasValue || req.ShopId.Value == 0)
                {
                    return new BadRequestObjectResult(new
                    {
                        code = 400,
                        message = "ShopId is required"
                    });
                }

                if (string.IsNullOrEmpty(req.ProductName))
                {
                    return new BadRequestObjectResult(new
                    {
                        code = 400,
                        message = "ProductName is required"
                    });
                }

                _logger.LogInformation("Creating product: {ProductName} for Shop: {ShopId}",
                    req.ProductName, req.ShopId);

                var result = _productService.CreateNew(req);

                _logger.LogInformation("Product created successfully with ID: {ProductId}", result?.id);

                return new OkObjectResult(new
                {
                    code = 200,
                    message = "Product Created Successfully",
                    count = 1,
                    countPage = 1,
                    currentPage = 1,
                    data = result
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CreateProduct: Error creating product");

                return new ObjectResult(new
                {
                    code = 500,
                    message = "Internal Server Error: " + ex.Message,
                    detailedMessage = ex.InnerException?.Message
                })
                { StatusCode = 500 };
            }
        }

        [HttpPut("update")]
        public IActionResult UpdateProduct([FromBody] ProductReq req)
        {
            try
            {
                _logger.LogInformation("UpdateProduct endpoint called");

                if (req == null)
                {
                    return new BadRequestObjectResult(new
                    {
                        code = 400,
                        message = "Request body cannot be null"
                    });
                }

                if (req.Id == 0)
                {
                    return new BadRequestObjectResult(new
                    {
                        code = 400,
                        message = "Product ID is required for update"
                    });
                }

                _logger.LogInformation("Updating product ID: {ProductId}", req.Id);

                var result = _productService.Update(req);

                _logger.LogInformation("Product updated successfully with ID: {ProductId}", result?.id);

                return new OkObjectResult(new
                {
                    code = 200,
                    message = "Product Updated Successfully",
                    count = 1,
                    countPage = 1,
                    currentPage = 1,
                    data = result
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UpdateProduct: Error updating product ID: {ProductId}", req?.Id);
                return new ObjectResult(new
                {
                    code = 500,
                    message = "Internal Server Error: " + ex.Message,
                    detailedMessage = ex.InnerException?.Message
                })
                { StatusCode = 500 };
            }
        }
    }
}