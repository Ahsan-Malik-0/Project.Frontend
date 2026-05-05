using Microsoft.AspNetCore.Authorization;
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


        // Event Handling ---------------------------------------------------------------------
        // Get pending events
        public async Task<List<Event>> GetPendingEvents(string memberId)
        {
            try
            {
                var response = await httpClient.GetAsync($"Chairperson/pendingEvents/{memberId}");

                if (!response.IsSuccessStatusCode)
                    return new List<Event>();

                var events = await response.Content.ReadFromJsonAsync<List<Event>>();

                return events ?? new List<Event>();
            }
            catch
            {
                return new List<Event>();
            }
        }

        // Update Event
        public async Task<ResponseResult> UpdateEvent(UpdateEventDto updateEventDto)
        {
            try
            {
                var response = await httpClient.PutAsJsonAsync($"chairperson/updateEvent/{updateEventDto.Id}", updateEventDto);

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

        // Delete Event
        public async Task<ResponseResult> DeleteEvent(Guid eventId)
        {
            try
            {
                var response = await httpClient.DeleteAsync($"Chairperson/deleteRequestedEvent/{eventId}");

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

        // Reject Event
        public async Task<ResponseResult> RejectEvent(UpdateEventStatusDto updateEventStatus)
        {
            try
            {
                var response = await httpClient.PutAsJsonAsync($"Chairperson/rejectEvent/{updateEventStatus.Id}", updateEventStatus);
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

        // Postponed Event
        public async Task<ResponseResult> PostponeEvent(Guid eventId)
        {
            try
            {
                var response = await httpClient.PutAsync($"Chairperson/postponeEvent/{eventId}", new StringContent(""));
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

        // Get specific event (use for edit event)
        public async Task<Event?> GetEventById(Guid societyId)
        {
            try
            {
                var response = await httpClient.GetAsync($"Chairperson/getEventById/{societyId}");

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


        // Requisition Handling --------------------------------------------------------------
        // Get pending event requisition
        public async Task<List<PendingEventRequisitionDto>> GetPendingRequisitions(Guid memberId)
        {
            try
            {
                var response = await httpClient.GetAsync($"ChairPerson/getPendingEventRequisitions/{memberId}");

                if (!response.IsSuccessStatusCode)
                {
                    return new List<PendingEventRequisitionDto>();
                }

                var pendingRequisitionDetails = await response.Content.ReadFromJsonAsync<List<PendingEventRequisitionDto>>();
                return pendingRequisitionDetails!;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<PendingEventRequisitionDto>();
            }
        }

        // Details for specific event requisition
        public async Task<EventRequisitionDetailsDto?> GetRequisitionDetails(Guid requisitionId)
        {
            try
            {
                var response = await httpClient.GetAsync($"ChairPerson/getEventRequisitionDetails/{requisitionId}");

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var requisitionDetails = await response.Content.ReadFromJsonAsync<EventRequisitionDetailsDto>();
                return requisitionDetails;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }


        // Get Chairperson details for event requisition
        public async Task<ChairpersonDetailsForRequisitionDto?> GetChairpersonDetailsForRequisition(Guid chairpersonId)
        {
            var response = await httpClient.GetAsync($"ChairPerson/getChairpersonDetailsForRequisition/{chairpersonId}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var chairpersonDetails = await response.Content.ReadFromJsonAsync<ChairpersonDetailsForRequisitionDto>();

            return chairpersonDetails;
        }

        // Create Event Requisition
        public async Task<ResponseResult> CreateRequisition(CreateEventRequisitionDto createEventRequisition)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync("ChairPerson/createEventRequisition", createEventRequisition);
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

        // Update Event Requisition
        public async Task<ResponseResult> UpdateRequisition(Guid requisitionId, UpdateEventRequisitionDto updateEventRequisition)
        {
            try
            {
                var response = await httpClient.PutAsJsonAsync($"ChairPerson/updateEventRequisition/{requisitionId}", updateEventRequisition);
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

        // Delete Event Requisition
        public async Task<ResponseResult> DeleteRequisition(Guid requisitionId)
        {
            try
            {
                var response = await httpClient.DeleteAsync($"ChairPerson/deleteEventRequisition/{requisitionId}");
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

        // Handle Yearly Budget -------------------------------------------------------------
        // Get all yearly budgets
        public async Task<List<YearlyBudgetResponseDto>> GetAllYearlyBudgets(Guid memberId)
        {
            try
            {
                var response = await httpClient.GetAsync($"ChairPerson/getAllYearlyBudgetRequisitions/{memberId}");

                if (!response.IsSuccessStatusCode)
                {
                    return new List<YearlyBudgetResponseDto>();
                }

                var yearlyBudgets = await response.Content.ReadFromJsonAsync<List<YearlyBudgetResponseDto>>();
                return yearlyBudgets ?? new List<YearlyBudgetResponseDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<YearlyBudgetResponseDto>();
            }
        }

        // Create Yearly Budget
        public async Task<ResponseResult> CreateYearlyBudget(CreateYearlyBudgetDto createYearlyBudgetDto, Guid memberId)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync($"ChairPerson/createYearlyBudgetRequisition/{memberId}", createYearlyBudgetDto);
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
  

        // Profile Details
        public async Task<MemberProfileDto?> GetProfile(Guid memberId)
        {
            var response = await httpClient.GetAsync($"ChairPerson/viewProfile/{memberId}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var memberProfile = await response.Content.ReadFromJsonAsync<MemberProfileDto>();

            return memberProfile;
        }

        // Edit Profile
        public async Task<ResponseResult> UpdateProfile(MemberProfileUpdateDto updatedProfile)
        {
            try
            {
                var response = await httpClient.PutAsJsonAsync("Chairperson/updateProfile", updatedProfile);

                if (!response.IsSuccessStatusCode)
                {
                    var resString = await response.Content.ReadAsStringAsync();
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

        public async Task<ViewMemberProfileDto?> GetPresidentProfile(Guid memberId)
        {
            var response = await httpClient.GetAsync($"Chairperson/viewPresidentProfile/{memberId}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var profile = await response.Content.ReadFromJsonAsync<ViewMemberProfileDto>();

            return profile;
        }

        public async Task<ResponseResult> UpdatePresidentProfile(Guid memberId, EditMemberProfileDto updatedProfile)
        {
            try
            {
                var response = await httpClient.PutAsJsonAsync($"Chairperson/updatePresidentProfile/{memberId}", updatedProfile);

                if (!response.IsSuccessStatusCode)
                {
                    var resString = await response.Content.ReadAsStringAsync();
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
