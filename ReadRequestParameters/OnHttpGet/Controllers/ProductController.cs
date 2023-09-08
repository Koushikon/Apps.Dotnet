using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnHttpGet.Models;

namespace OnHttpGet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly List<Product> _products = new()
        {
            new Product{ Id = 1, Category = "Electronic", Brand = "Sony", Name = "Play Station", WarrantyYears = 2, IsAvailable = true },
            new Product{ Id = 2, Category = "Electronic", Brand = "Sony", Name = "Mobile", WarrantyYears = 2, IsAvailable = true },
            new Product{ Id = 3, Category = "Electronic", Brand = "LG", Name = "TV", WarrantyYears = 2, IsAvailable = true },
            new Product{ Id = 4, Category = "Clothing", Brand = "Adidas", Name = "Shoes", WarrantyYears = 3, IsAvailable = true },
            new Product{ Id = 5, Category = "Sports", Brand = "Nike", Name = "Sneakers", WarrantyYears = 3, IsAvailable = false },
            new Product{ Id = 6, Category = "Sports", Brand = "Adidas", Name = "Football", WarrantyYears = 3, IsAvailable = false },
            new Product{ Id = 7, Category = "Electronic", Brand = "Apple", Name = "Mobile", WarrantyYears = 2, IsAvailable = true }
        };

        private readonly IMapper _mapper;

        public ProductController(IMapper mapper)
        {
            _mapper = mapper;
        }

        #region From Query Parameters

        /***
         * Query Parameters
         * Example: https://localhost:7257/api/Product/ProductsAsQuery?category=Electronic&brand=OnePlus
         * 
         * When we work with query parameters, we have the option to use the [FromQuery] attribute to bind parameters
         * from the query string explicitly. However, the parameter binding process automatically converts request data
         * into strongly typed parameters. This process eliminates the need for explicit [FromQuery] attribute usage.
         */
        [HttpGet("ProductsAsQuery")]
        public IActionResult GetProductsAsQuery(string category, string brand)
        {
            List<Product> result = _products
                .Where(x => x.Category == category && x.Brand == brand)
                .ToList();

            return Ok(_mapper.Map<List<ProductDto>>(result));
        }


        /***
         * Query Parameters
         * Example: https://localhost:7257/api/Product/ProductsAsQueryFromQuery?type=Electronic&manufacturer=OnePlus
         */
        [HttpGet("ProductsAsQueryFromQuery")]
        public IActionResult GetProductsAsQueryFromQuery(
            [FromQuery(Name = "type")] string category,
            [FromQuery(Name = "manufacturer")] string brand)
        {
            List<Product> result = _products
                .Where(x => x.Category == category && x.Brand == brand)
                .ToList();

            return Ok(_mapper.Map<List<ProductDto>>(result));
        }

        #endregion


        #region From Route Parameters

        /***
         * Route Parameters
         * Example: https://localhost:7257/api/Product/ProductsAsParameter/3
         * 
         * By default, the framework binds route parameters based on their names when we use route parameters,
         * allowing us to omit the [FromRoute] attribute. However, there are scenarios where the parameter names
         * in the method signature differ from the route parameter names. In such situations, we can use the [FromRoute] attribute.
         */
        [HttpGet("ProductsAsParameter/{id}")]
        public IActionResult GetProductsAsParameter(int id)
        {
            return Ok(_mapper.Map<ProductDto>(
                _products
                .Where(x => x.Id == id)
                .FirstOrDefault())
            );
        }


        /***
         * Route Parameters
         * Example: https://localhost:7257/api/Product/ProductsAsParameterFromRoute/2
         */
        [HttpGet("ProductsAsParameterFromRoute/{productId}")]
        public IActionResult GetProductsAsParameterFromRoute([FromRoute(Name = "productId")] int id)
        {
            return Ok(_mapper.Map<ProductDto>(
                _products
                .Where(x => x.Id == id)
                .FirstOrDefault())
            );
        }

        #endregion


        #region Combination of Query and Route Parameters

        /***
         * Combination of Query and Route Parameters
         * Example: https://localhost:7257/api/Product/brand/OnePlus?warrenty=2
         */
        [HttpGet("brand/{brand}")]
        public IActionResult GetProductsByBrandAndWarrenty(string brand, int warrenty)
        {
            return Ok(_mapper.Map<List<ProductDto>>(
                _products
                .Where(x => x.Brand == brand && x.WarrantyYears == warrenty)
                .ToList())
            );
        }

        #endregion


        #region From Request Body Parameters

        /***
         * Request Body Parameters
         * Example: https://localhost:7257/api/Product/GetProductsFromBody
         * 
         * By default, the HTTP methods GET, HEAD, OPTIONS, and DELETE do not bind data implicitly from the request body.
         * According to the best practices of REST API, it's not recommend passing parameters
         * through the request body in a GET method, although it is possible.
         * 
         * We utilize the ProductDto class to pass the model into the request body.
         * The [FromBody] attribute binds the request body values to the specified model.
         */
        [HttpGet("GetProductsFromBody")]
        public IActionResult GetProductsFromBody([FromBody] ProductDto product)
        {
            return Ok(_mapper.Map<List<ProductDto>>(
                _products
                .Where(x => x.Category == product.Category)
                .ToList())
            );
        }

        #endregion


        #region From Header Parameters

        /***
         * Header Parameters
         * Example: https://localhost:7257/api/Product/GetProductsByCategoryAndBrandViaHeader
         * 
         * Header parameters are another way to pass data to a GET method using [FromHeader] attribute.
         * Unlike query and route parameters, header parameters are not part of the URL.
         * Instead, we can send them as part of the request headers.
         */
        [HttpGet("GetProductsByCategoryAndBrandViaHeader")]
        public IActionResult GetProductsByCategoryAndBrandViaHeader([FromHeader] string category, [FromHeader] string brand)
        {
            return Ok(_mapper.Map<List<ProductDto>>(
                _products
                .Where(x => x.Category == category && x.Brand == brand)
                .ToList())
            );
        }

        #endregion
    }
}
