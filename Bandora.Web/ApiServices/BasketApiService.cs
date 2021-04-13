using Bandora.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Bondora.Web.ApiServices
{
    public interface IBasketApiService
    {
        Task<ServiceResult> CheckoutCustomerBasket(OrderVM order);
    }


    public class BasketApiService : IBasketApiService
    {
        private readonly HttpClient httpClient;

        public BasketApiService(HttpClient httpClient)
        {
            httpClient.BaseAddress = new Uri("https://localhost:44392/api/basket/");
            httpClient.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
            httpClient.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Sample");
            this.httpClient = httpClient;
        }
        public async Task<ServiceResult> CheckoutCustomerBasket(OrderVM order)
        {
            ServiceResult createCustomerResult = new ServiceResult();
            try
            {
                HttpResponseMessage responseMessage = await httpClient.PostAsJsonAsync("CheckoutCustomerBasket", order);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = await responseMessage.Content.ReadAsStringAsync();
                    createCustomerResult = JsonConvert.DeserializeObject<ServiceResult>(responseData);
                }
                return createCustomerResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
