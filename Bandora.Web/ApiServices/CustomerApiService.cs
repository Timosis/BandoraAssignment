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

    public interface ICustomerApiService
    {
        Task<ServiceResult<List<CustomerVM>>> GetCustomersList();
        Task<ServiceResult<CustomerVM>> GetCustomer(int id);
        Task<ServiceResult> CreateCustomer(CustomerVM customer);
        Task<ServiceResult<List<OrderVM>>> GetCustomerOrdersList(int customerId);
        Task<ServiceResult<List<OrderDetailVM>>> GetCustomerOrderDetail(int orderId);
    }

    public class CustomerApiService : ICustomerApiService
    {
        private readonly HttpClient httpClient;
        public CustomerApiService(HttpClient httpClient)
        {
            httpClient.BaseAddress = new Uri("https://localhost:44392/api/customer/");
            httpClient.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
            httpClient.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Sample");
            this.httpClient = httpClient;
        }


        public async Task<ServiceResult> CreateCustomer(CustomerVM customer)
        {
            ServiceResult createCustomerResult = new ServiceResult();
            try
            {
                HttpResponseMessage responseMessage = await httpClient.PostAsJsonAsync("CreateCustomer", customer);
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

        public async Task<ServiceResult<List<CustomerVM>>> GetCustomersList()
        {
            ServiceResult<List<CustomerVM>> getCustomersResult = new ServiceResult<List<CustomerVM>>();
            try
            {
                HttpResponseMessage responseMessage = await httpClient.GetAsync("GetCustomersList");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = await responseMessage.Content.ReadAsStringAsync();
                    getCustomersResult = JsonConvert.DeserializeObject<ServiceResult<List<CustomerVM>>>(responseData); ;
                }
                return getCustomersResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ServiceResult<CustomerVM>> GetCustomer(int id)
        {
            ServiceResult<CustomerVM> getCustomerResult = new ServiceResult<CustomerVM>();
            try
            {
                HttpResponseMessage responseMessage = await httpClient.GetAsync("GetCustomer/" + id);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = await responseMessage.Content.ReadAsStringAsync();
                    getCustomerResult = JsonConvert.DeserializeObject<ServiceResult<CustomerVM>>(responseData); ;
                }
                return getCustomerResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ServiceResult<List<OrderDetailVM>>> GetCustomerOrderDetail(int orderId)
        {
            ServiceResult<List<OrderDetailVM>> getCustomerOrderDetailResult = new ServiceResult<List<OrderDetailVM>>();
            try
            {
                HttpResponseMessage responseMessage = await httpClient.GetAsync("GetCustomerOrderDetail/" + orderId);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = await responseMessage.Content.ReadAsStringAsync();
                    getCustomerOrderDetailResult = JsonConvert.DeserializeObject<ServiceResult<List<OrderDetailVM>>>(responseData); ;
                }
                return getCustomerOrderDetailResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ServiceResult<List<OrderVM>>> GetCustomerOrdersList(int customerId)
        {
            ServiceResult<List<OrderVM>> getCustomerOrdersResult = new ServiceResult<List<OrderVM>>();
            try
            {
                HttpResponseMessage responseMessage = await httpClient.GetAsync("GetCustomerOrdersList/" + customerId);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = await responseMessage.Content.ReadAsStringAsync();
                    getCustomerOrdersResult = JsonConvert.DeserializeObject<ServiceResult<List<OrderVM>>>(responseData); ;
                }
                return getCustomerOrdersResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
