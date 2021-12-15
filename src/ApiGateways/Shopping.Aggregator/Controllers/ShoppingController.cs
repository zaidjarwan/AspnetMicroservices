using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shopping.Aggregator.Models;
using Shopping.Aggregator.Services;
using System.Threading.Tasks;

namespace Shopping.Aggregator.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ShoppingController : ControllerBase
    {
        private readonly ICatalogService _catalogService;
        private readonly IBasketService _basketService;
        private readonly IOrderService _orderService;

        public ShoppingController(ICatalogService catalogService,
                                    IBasketService basketService,
                                    IOrderService orderService)
        {
            this._catalogService = catalogService;
            this._basketService = basketService;
            this._orderService = orderService;
        }

        [HttpGet("{userName}", Name = "GetShopping")]
        public async Task<ActionResult<ShoppingModel>> GetShopping(string userName)
        {
            // Get baseket with username
            // iterate basket items and consume products with basket item productId number
            // map product related member into basketitem dto with extended columns
            // consume ordering microservices to retrive order list
            // return root shopping model dto class which incudes all responses

            var basket = await _basketService.GetBasket(userName);

            foreach (var item in basket.Items)
            {
                var product = await _catalogService.GetCatalog(item.ProductId);

                //set additional product fields onto basket item
                item.ProductName = product.Name;
                item.Category = product.Category;
                item.Summary = product.Summary;
                item.Description = product.Description;
                item.ImageFile = product.ImageFile;
            }

            var orders = await _orderService.GetOrdersByUserName(userName);

            var shoppingModel = new ShoppingModel
            {
                UserName = userName,
                BasketWithProducts = basket,
                Orders = orders
            };

            return Ok(shoppingModel);
        }

    }
}
