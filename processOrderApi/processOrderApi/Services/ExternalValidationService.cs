using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace processOrderApi.Services
{
    public class ExternalValidationService : IExternalValidationService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public ExternalValidationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _baseUrl = ConfigurationManager.AppSettings["ExternalValidationUrl"];
        }

        public async Task<bool> ValidarClienteAsync(int clienteId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}{clienteId}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                // Manejo de errores de red/timeouts
                throw new Exception("Error al conectar con el servicio de validación", ex);
            }
        }
    }
}