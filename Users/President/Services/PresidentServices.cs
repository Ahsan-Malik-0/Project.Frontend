using Project.Frontend.Model;
using Project.Frontend.Model.DTOs;
using System.Net.Http.Json;
using System.Text.Json.Nodes;

namespace Project.Frontend.PresidentServices
{
    public class PresidentServices
    {
        private readonly HttpClient httpClient;

        public PresidentServices(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<Event>> GetPendingEvents(string memberId)
        {
            try
            {
                var respnse = await httpClient.GetAsync($"president/pendingEvents/{memberId}");

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

        public async Task<ResponseResult> CreateEvent(AddEventDto addEventDto)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync("president/addEvent", addEventDto);

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
                var response = await httpClient.PutAsJsonAsync($"president/updateEvent/{updateEventDto.Id}", updateEventDto);

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

        public async Task<ResponseResult> DeleteEvent(Guid eventId)
        {
            try
            {
                var response = await httpClient.DeleteAsync($"President/deletePendingEvent/{eventId}");

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
                var response = await httpClient.GetAsync($"president/getEventById/{societyId}");

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

        

        public async Task<List<Event>> History(string memberId)
        {
            try
            {
                var respnse = await httpClient.GetAsync($"president/history/{memberId}");

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

        public async Task<MemberProfileDto?> GetProfile(string memberId)
        {
            var response = await httpClient.GetAsync($"president/viewProfile/{memberId}");

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
                var reponse = await httpClient.PutAsJsonAsync($"president/updateProfile/{updatedProfile.Id}", updatedProfile);

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

        public async Task<Guid?> GetSocietyIdAsync(string memberId)
        {
            var response = await httpClient.GetAsync($"president/getSocietyId/{memberId}");

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<Guid>();
        }

    }
}
