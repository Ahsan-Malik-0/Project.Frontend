using Microsoft.AspNetCore.Authorization;
using Project.Frontend.Model;
using Project.Frontend.Model.DTOs;
using System.Net.Http.Json;
using System.Text.Json.Nodes;

namespace Project.Frontend.ChairpersonServices
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
        public async Task<List<Event>> GetPendingEvents(Guid memberId)
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
        public async Task<List<RequisitionDetailsForChairperson>> GetPendingRequisitions(Guid memberId)
        {
            try
            {
                var response = await httpClient.GetAsync($"ChairPerson/getPendingEventRequisitions/{memberId}");

                if (!response.IsSuccessStatusCode)
                {
                    return new List<RequisitionDetailsForChairperson>();
                }

                var pendingRequisitionDetails = await response.Content.ReadFromJsonAsync<List<RequisitionDetailsForChairperson>>();
                return pendingRequisitionDetails!;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<RequisitionDetailsForChairperson>();
            }
        }

        // Details for specific event requisition
        //public async Task<EventRequisitionDetailsDto?> GetRequisitionDetails(Guid requisitionId)
        //{
        //    try
        //    {
        //        var response = await httpClient.GetAsync($"ChairPerson/getEventRequisitionDetails/{requisitionId}");

        //        if (!response.IsSuccessStatusCode)
        //        {
        //            return null;
        //        }

        //        var requisitionDetails = await response.Content.ReadFromJsonAsync<EventRequisitionDetailsDto>();
        //        return requisitionDetails;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        return null;
        //    }
        //}

        // Get requissition by id
        public async Task<RequisitionDetailsForChairperson?> GetRequisitionDetailsById(Guid requisitionId)
        {
            var response = await httpClient.GetAsync($"ChairPerson/getEventRequisitionById/{requisitionId}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var chairpersonDetails = await response.Content.ReadFromJsonAsync<RequisitionDetailsForChairperson>();

            return chairpersonDetails;
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

        // Get requisition history
        public async Task<List<EventRequisitionHistoryForCP>?> GetRequisitionHistory(Guid memberId)
        {
            try
            {
                var response = await httpClient.GetAsync($"ChairPerson/getEventRequisitionHistory/{memberId}");

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var requisitionHistory = await response.Content.ReadFromJsonAsync<List<EventRequisitionHistoryForCP>>();
                return requisitionHistory ?? null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }


        // Handle Event Audits -------------------------------------------------------------
        // Get event audits
        public async Task<EventAudit?> GetEventAudit(Guid eventId)
        {
            try
            {
                var response = await httpClient.GetAsync($"ChairPerson/viewEventAudits/{eventId}");

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }
                var pendingAudits = await response.Content.ReadFromJsonAsync<EventAudit>();
                return pendingAudits ?? null;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        // Create event audit
        public async Task<ResponseResult> CreateEventAudit(CreateEventAuditDto createEventAuditDto)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync($"ChairPerson/createEventAudit/{createEventAuditDto.EventId}", createEventAuditDto);
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

        // Update event audit
        public async Task<ResponseResult> UpdateEventAudit(Guid auditId, UpdateEventAuditDto updateEventAuditDto)
        {
            try
            {
                var response = await httpClient.PutAsJsonAsync($"ChairPerson/updateEventAudit/{auditId}", updateEventAuditDto);
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

        // Delete event audit
        public async Task<ResponseResult> DeleteEventAudit(Guid auditId)
        {
            try
            {
                var response = await httpClient.DeleteAsync($"ChairPerson/deleteEventAudit/{auditId}");
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

        public async Task<ResponseResult> VerifyTakeAmount(Guid auditId)
        {
            try
            {
                var response = await httpClient.GetAsync($"ChairPerson/verifyTakeAmount/{auditId}");
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

        // Get remaining budget
        public async Task<decimal?> GetRemainigYearlyBudge(Guid memberId)
        {
            try
            {
                var response = await httpClient.GetAsync($"ChairPerson/GetRemainigYearlyBudge/{memberId}");

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                return await response.Content.ReadFromJsonAsync<decimal>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }


        // Virtul Society ----------------------------------------
        public async Task<List<GetVirtualSocietyDetailsDto>?> GetVirtualSocietiesDetails()
        {
            try
            {
                var response = await httpClient.GetAsync($"ChairPerson/getVirtualSocietiesDetails");

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var virtualSocietyDetails = await response.Content.ReadFromJsonAsync<List<GetVirtualSocietyDetailsDto>>();
                return virtualSocietyDetails ?? null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        // Submit Contribution
        public async Task<ResponseResult> ContributeToVirtualSociety(Guid memberId, ContributeToVirtualSocietyDto contribution)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync($"ChairPerson/contributeToVirtualSociety/{memberId}", contribution);
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

        public async Task<List<Event>?> GetVirtualSocietiyEvents(Guid virtualSocietyId)
        {
            try
            {
                var response = await httpClient.GetAsync($"ChairPerson/getVirtualSocietyEvents/{virtualSocietyId}");

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var events = await response.Content.ReadFromJsonAsync<List<Event>>();
                return events ?? null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<ResponseResult> CreateVirtualSocietyEvent(AddVirtualSocietyEventDto newEvent)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync("Chairperson/createVirtualSocietyEvent", newEvent);

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

        public async Task<ResponseResult> CreateVirtualSocietyRequisition(CreateVirtualSocietyRequisitionDto newRequisition)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync("Chairperson/createVirtualSocietyRequisition", newRequisition);

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
        //public async Task<List<GetPastVirtualSocietyDetailsDto>?> GetPastVirtualSocietyDetails()
        //{
        //    try
        //    {
        //        var response = await httpClient.GetAsync($"ChairPerson/getPastVirtualSocietyDetails");

            //        if (!response.IsSuccessStatusCode)
            //        {
            //            return null;
            //        }

            //        var yearlyBudgets = await response.Content.ReadFromJsonAsync<List<GetPastVirtualSocietyDetailsDto>>();
            //        return yearlyBudgets ?? null;
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine(ex.Message);
            //        return null;
            //    }
            //}


        public async Task<ResponseResult> CreateVirtualSociety(CreateVirtualSocietyDto newVirtualSocietyDto)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync("Task/createVirtualSociety", newVirtualSocietyDto);
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

        //public async Task<List<ListOfSocietiesDto>> ListOfSocieties()
        //{
        //    try
        //    {
        //        var response = await httpClient.GetAsync($"Task/getAllSocieties");

        //        if (!response.IsSuccessStatusCode)
        //        {
        //            return new List<ListOfSocietiesDto>();
        //        }

        //        var yearlyBudgets = await response.Content.ReadFromJsonAsync<List<ListOfSocietiesDto>>();
        //        return yearlyBudgets ?? new List<ListOfSocietiesDto>();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        return new List<ListOfSocietiesDto>();
        //    }
        //}

        public async Task<bool?> CheckSocietySelected(Guid memberId)
        {
            var response = await httpClient.GetAsync($"Task/checkSocietyExist/{memberId}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            bool check = await response.Content.ReadFromJsonAsync<bool>();

            return check;
        }

        //public async Task<ResponseResult> AddIdInVirtualSocietiesSociety(AddIdInVirtualSocietiesSocietyDto addIdInVirtualSocietiesSociety)
        //{
        //    try
        //    {
        //        var response = await httpClient.PostAsJsonAsync("Task/createVirtualSocietyRequisition", addIdInVirtualSocietiesSociety);
        //        if (!response.IsSuccessStatusCode)
        //        {
        //            var resString = await response.Content.ReadAsStringAsync();
        //            var jsonNode = JsonNode.Parse(resString);
        //            var error = jsonNode?["errors"]?.ToString() ?? string.Empty;

        //            return new ResponseResult() { Success = false, Error = error };
        //        }

        //        return new ResponseResult() { Success = true };

        //    }
        //    catch (Exception ex)
        //    {
        //        return new ResponseResult() { Success = false, Error = ex.Message };
        //    }
        //}

        public async Task<VirtualSociety?> GetLatestVirtualSociety()
        {
            var response = await httpClient.GetAsync($"Task/getVirtualSocietyDetails");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var virtualSociety = await response.Content.ReadFromJsonAsync<VirtualSociety>();

            return virtualSociety;
        }

        //public async Task<ResponseResult> CreateVirtualSocietyRequisition(CreateVirtualSocietyRequisitionDto newRequisition)
        //{
        //    try
        //    {
        //        var response = await httpClient.PostAsJsonAsync("Task/createVirtualSocietyRequisition", newRequisition);
        //        if (!response.IsSuccessStatusCode)
        //        {
        //            var resString = await response.Content.ReadAsStringAsync();
        //            var jsonNode = JsonNode.Parse(resString);
        //            var error = jsonNode?["errors"]?.ToString() ?? string.Empty;

        //            return new ResponseResult() { Success = false, Error = error };
        //        }

        //        return new ResponseResult() { Success = true };

        //    }
        //    catch (Exception ex)
        //    {
        //        return new ResponseResult() { Success = false, Error = ex.Message };
        //    }
        //}

        public async Task<List<Event>> GetPendingVirtualSocietyEvents(Guid virtualSocietyId)
        {
            try
            {
                var respnse = await httpClient.GetAsync($"president/getPendingVirtualSocietyEvents/{virtualSocietyId}");

                if (!respnse.IsSuccessStatusCode)
                    return new List<Event>();

                var virtualSocietyEvents = await respnse.Content.ReadFromJsonAsync<List<Event>>();

                return virtualSocietyEvents ?? new List<Event>();
            }
            catch
            {
                return new List<Event>();
            }
        }

        

        public async Task<ResponseResult> DeleteVirtualSocietyEventEvent(Guid eventId)
        {
            try
            {
                var response = await httpClient.DeleteAsync($"Task/deleteVirtualSocietyEvent/{eventId}");

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
    }
}