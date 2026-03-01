using Project.Frontend.Model;
using Project.Frontend.Model.DTOs;
using System.Net.Http.Json;
using System.Text.Json.Nodes;

namespace Project.Frontend.Services
{
    public class EventRequisitionServices
    {
        private readonly HttpClient httpClient;

        public EventRequisitionServices(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<ResponseResult> CreateRequisition(SendEventRequisitionDto requisitionDto)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync("chairperson/createRequisition", requisitionDto);
                if (!response.IsSuccessStatusCode)
                {
                    var resString = await response.Content.ReadAsStringAsync();
                    var jsonNode = JsonNode.Parse(resString);
                    var error = jsonNode?["errors"]?.ToString() ?? string.Empty;

                    return new ResponseResult() { Success = false, Error = error };
                }

                return new ResponseResult() { Success = true };

            }
            catch (Exception ex)
            {
                return new ResponseResult() { Success = false, Error = ex.Message };
            }
        }

        public async Task<List<PendingEventRequisitionsDto>> GetPendingEventRequisitions(Guid id)
        {
            try
            {
                var response = await httpClient.GetAsync($"ChairPerson/getPendingRequisitions?memberId={id}");

                if (!response.IsSuccessStatusCode)
                {
                    return new List<PendingEventRequisitionsDto>();
                }

                var pendingRequisitions = await response.Content.ReadFromJsonAsync<List<PendingEventRequisitionsDto>>();
                return pendingRequisitions!;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<PendingEventRequisitionsDto>();
            }
        }
    }
}
