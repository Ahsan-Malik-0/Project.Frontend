using Project.Frontend.Model;
using Project.Frontend.Model.DTOs;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;
using System.Text.Json.Nodes;

namespace Project.Frontend.Services
{
    public class EventServices
    {
        private readonly HttpClient httpClient;

        public EventServices(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<Event>> GetPendingEvents(string memberId)
        {
            try
            {
                var respnse = await httpClient.GetAsync($"president/pendingEvents?memberId={memberId}");

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

        public async Task<ResponseResult> CreateEventByPresident(AddEventDto addEventDto)
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
                return new ResponseResult() { Success = false , Error = ex.Message};
            }
        }

        public async Task<ResponseResult> DeleteEvent(Guid eventId)
        {
            try
            {
                var response = await httpClient.DeleteAsync($"president/deleteEvent/{eventId}");

                if(!response.IsSuccessStatusCode)
                {
                    var resString = await response.Content.ReadAsStringAsync();
                    var jsonNode = JsonNode.Parse(resString);
                    var error = jsonNode?["errors"]?.ToString() ?? string.Empty;

                    return new ResponseResult() { Success = false, Error = error};
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
                var response = await httpClient.GetAsync($"president/getEventById?eventId={societyId}");

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

        public async Task<ResponseResult> UpdateEvent(UpdateEventDto updateEventDto)
        {
            try
            {
                var response = await httpClient.PutAsJsonAsync("president/updateEvent", updateEventDto);

                if (!response.IsSuccessStatusCode) {
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

        public async Task<List<Event>> GetAcceptedEvents(string memberId)
        {
            try
            {
                var respnse = await httpClient.GetAsync($"president/history?memberId={memberId}");

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
    }
}