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
    public interface IEquipmentApiService
    {
        Task<ServiceResult<List<EquipmentVM>>> GetEquipments();
        Task<ServiceResult<EquipmentVM>> GetEquipment(int id);
        Task<ServiceResult> CreateEquipment(EquipmentVM equipment);
    }

    public class EquipmentApiService : IEquipmentApiService
    {
        private readonly HttpClient httpClient;
        public EquipmentApiService(HttpClient httpClient)
        {
            httpClient.BaseAddress = new Uri("https://localhost:44392/api/equipment/");
            httpClient.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
            httpClient.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Sample");
            this.httpClient = httpClient;
        }


        public async Task<ServiceResult> CreateEquipment(EquipmentVM equipment)
        {
            ServiceResult createEquipmentResult = new ServiceResult();
            try
            {
                HttpResponseMessage responseMessage = await httpClient.PostAsJsonAsync("CreateEquipment", equipment);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = await responseMessage.Content.ReadAsStringAsync();
                    createEquipmentResult = JsonConvert.DeserializeObject<ServiceResult>(responseData);
                }
                return createEquipmentResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ServiceResult<List<EquipmentVM>>> GetEquipments()
        {
            ServiceResult<List<EquipmentVM>> getEquipmentsResult = new ServiceResult<List<EquipmentVM>>();
            try
            {
                HttpResponseMessage responseMessage = await httpClient.GetAsync("GetEquipmentsList");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = await responseMessage.Content.ReadAsStringAsync();
                    getEquipmentsResult = JsonConvert.DeserializeObject<ServiceResult<List<EquipmentVM>>>(responseData);
                }
                return getEquipmentsResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ServiceResult<EquipmentVM>> GetEquipment(int id)
        {
            ServiceResult<EquipmentVM> getEquipmentResult = new ServiceResult<EquipmentVM>();
            try
            {
                HttpResponseMessage responseMessage = await httpClient.GetAsync("GetEquipment");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = await responseMessage.Content.ReadAsStringAsync();
                    getEquipmentResult = JsonConvert.DeserializeObject<ServiceResult<EquipmentVM>>(responseData); ;
                }
                return getEquipmentResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
