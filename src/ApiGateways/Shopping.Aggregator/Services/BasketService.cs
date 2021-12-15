using Shopping.Aggregator.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Shopping.Aggregator.Services
{
    public class BasketService : IBasketService
    {
        private readonly HttpClient _httpClient;

        public BasketService(HttpClient httpClient)
        {
            this._httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }
        public Task<BasketModel> GetBasket(string userName)
        {
            throw new System.NotImplementedException();
        }
    }
}
