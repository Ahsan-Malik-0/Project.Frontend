using Project.Frontend.Model;
using Project.Frontend.Model.DTOs;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Nodes;

namespace Project.Frontend.FinanceServices
{
    public class FinanceServices
    {
        private readonly HttpClient httpClient;

        public FinanceServices(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<ViewRequisitionDetailsForFinanceDto>> GetRequisitionRequestDetails()
        {
            try
            {
                var response = await httpClient.GetAsync("Finance/ViewEventRequisitionDetails");
                if (!response.IsSuccessStatusCode)
                    return new List<ViewRequisitionDetailsForFinanceDto>();

                var requisitionDetails = await response.Content.ReadFromJsonAsync<List<ViewRequisitionDetailsForFinanceDto>>();

                return requisitionDetails ?? new List<ViewRequisitionDetailsForFinanceDto>();
            }
            catch
            {
                return new List<ViewRequisitionDetailsForFinanceDto>();
            }
        }

        //public async Task<EventRequisitionDetailsDto?> ViewRequisitionRequestDetails(Guid requisitionId)
        //{
        //    try
        //    {
        //        var response = await httpClient.GetAsync($"Finance/ViewRequisitionDetails/{requisitionId}");
        //        if (!response.IsSuccessStatusCode)
        //            return null;

        //        var requisitionDetails = await response.Content.ReadFromJsonAsync<EventRequisitionDetailsDto>();

        //        return requisitionDetails ?? null;
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        public async Task<ResponseResult> RejectEventRequisition(Guid requisitionId, string reason)
        {
            try
            {
                ResponseMessageDto responseMessage = new ResponseMessageDto() { ResponseMessage = reason };

                var response = await httpClient.PostAsJsonAsync($"Finance/RejectEventRequisition/{requisitionId}", responseMessage);
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

        public async Task<ChairpersonDetailsForRequisitionDto?> GetChairpersonDetailsForRequisition(string societyName)
        {
            try
            {
                var response = await httpClient.GetAsync($"Finance/getChairpersonDetails/{societyName}");
                if (!response.IsSuccessStatusCode)
                    return null;

                var chairpersonDetails = await response.Content.ReadFromJsonAsync<ChairpersonDetailsForRequisitionDto>();

                return chairpersonDetails ?? null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<ResponseResult> ReleasedEventRequisitionBudget(Guid requisitionId, string reviewMessage)
        {
            try
            {
                ResponseMessageDto responseMessage = new ResponseMessageDto() { ResponseMessage = reviewMessage };
                var response = await httpClient.PostAsJsonAsync($"Finance/ReleasedEventRequisitionBudget/{requisitionId}", responseMessage);
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

        // View History of Requisition
        public async Task<List<ViewRequisitionDetailsForFinanceHistoryDto>> GetRequisitionHistory()
        {
            try
            {
                var response = await httpClient.GetAsync("Finance/ViewEventRequisitionHistory");
                if (!response.IsSuccessStatusCode)
                    return new List<ViewRequisitionDetailsForFinanceHistoryDto>();

                var requisitionHistory = await response.Content.ReadFromJsonAsync<List<ViewRequisitionDetailsForFinanceHistoryDto>>();

                return requisitionHistory ?? new List<ViewRequisitionDetailsForFinanceHistoryDto>();
            }
            catch
            {
                return new List<ViewRequisitionDetailsForFinanceHistoryDto>();
            }
        }

        // Handle Audits ---------------------------------------------------------------------------
        public async Task<EventAudit?> GetEventAudit(Guid eventId)
        {
            try
            {
                var response = await httpClient.GetAsync($"Finance/ViewEventAuditDetails/{eventId}");

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

        public async Task<ResponseResult> VerifyTakeAmount(Guid auditId)
        {
            try
            {
                var response = await httpClient.GetAsync($"Finance/verifyTakeAmount/{auditId}");
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

        public async Task<ResponseResult> RequestAudit(Guid requisitionId)
        {
            try
            {
                var response = await httpClient.GetAsync($"Finance/RequestForEventAudit/{requisitionId}");
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
            var response = await httpClient.GetAsync($"Finance/viewProfile/{memberId}");

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
                var response = await httpClient.PutAsJsonAsync("Finance/updateProfile", updatedProfile);

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