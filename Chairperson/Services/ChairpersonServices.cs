using Project.Frontend.Model;
using Project.Frontend.Model.DTOs;
using System.Net.Http.Json;
using System.Text.Json.Nodes;

namespace Project.Frontend.Chairperson.Services
{
    public class ChairpersonServices
    {

        private readonly HttpClient httpClient;

        public ChairpersonServices(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<Event>> GetPendingEvents(string memberId)
        {
            try
            {
                var respnse = await httpClient.GetAsync($"chairperson/pendingEvents?memberId={memberId}");

                if (!respnse.IsSuccessStatusCode)
                    return new List<Event>();

                var events = await respnse.Content.ReadFromJsonAsync<List<Event>>();

                return events ?? new List<Event>();
            }
            catch
            {
                return new List<Event>();
            }
        }

        public async Task<ResponseResult> SoftDeleteEvent(Event @event)
        {
            try
            {
                var response = await httpClient.PutAsJsonAsync($"ChairPerson/softDeleteEvent", @event);

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

        public async Task<ResponseResult> UpdateEvent(UpdateEventDto updateEventDto)
        {
            try
            {
                var response = await httpClient.PutAsJsonAsync("chairperson/updateEvent", updateEventDto);

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

        public async Task<Event?> GetEventById(Guid societyId)
        {
            try
            {
                var response = await httpClient.GetAsync($"chairperson/getEventById?eventId={societyId}");

                if (!response.IsSuccessStatusCode)
                    return null;

                else
                {
                    var @event = await response.Content.ReadFromJsonAsync<Event>();
                    return @event;
                }
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<PendingEventRequisitionDetailsDto>> GetPendingRequisitions(Guid id)
        {
            try
            {
                var response = await httpClient.GetAsync($"ChairPerson/getPendingRequisitions?memberId={id}");

                if (!response.IsSuccessStatusCode)
                {
                    return new List<PendingEventRequisitionDetailsDto>();
                }

                var pendingRequisitionDetails = await response.Content.ReadFromJsonAsync<List<PendingEventRequisitionDetailsDto>>();
                return pendingRequisitionDetails!;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<PendingEventRequisitionDetailsDto>();
            }
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

        public async Task<ChairpersonDetailForRequisitionFormDto?> GetDetailsForRequisition(Guid memberId)
        {
            var response = await httpClient.GetAsync($"chairperson/detailsForRequisition?id={memberId}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var memberDetails = await response.Content.ReadFromJsonAsync<ChairpersonDetailForRequisitionFormDto>();

            return memberDetails;
        }

        public async Task<ResponseResult> RejectEvent(AcceptRejectEventDto acceptRejectEvent)
        {
            try
            {
                var response = await httpClient.PutAsJsonAsync("ChairPerson/acceptRejectEvent", acceptRejectEvent);
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

        public async Task<MemberProfileDto?> GetProfile(string memberId)
        {
            var response = await httpClient.GetAsync($"ChairPerson/viewProfile?memberId={memberId}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var memberProfile = await response.Content.ReadFromJsonAsync<MemberProfileDto>();

            return memberProfile;
        }

        public async Task<ResponseResult> UpdateProfile(MemberProfileUpdateDto updatedProfile)
        {
            try
            {
                var reponse = await httpClient.PutAsJsonAsync("Chairperson/updateProfile", updatedProfile);

                if (!reponse.IsSuccessStatusCode)
                {
                    var resString = await reponse.Content.ReadAsStringAsync();
                    var jsonNode = JsonNode.Parse(resString);
                    var error = jsonNode?["errors"]?.ToString();

                    return new ResponseResult() { Success = false, Error = error };
                }
                return new ResponseResult() { Success = true };

            }
            catch (Exception ex)
            {
                return new ResponseResult() { Success = false, Error = ex.Message };
            }
        }
    }
}
