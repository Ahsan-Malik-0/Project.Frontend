using Project.Frontend.Model;
using Project.Frontend.Model.DTOs;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Nodes;

namespace Project.Frontend.AdminServices
{
    public class AdminServices
    {
        private readonly HttpClient httpClient;

        public AdminServices(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<ViewRequisitionRequestDetailsDto>> GetRequisitionRequestDetails()
        {
            try
            {
                var response = await httpClient.GetAsync("Admin/ViewPendingRequisitions");
                if (!response.IsSuccessStatusCode)
                    return new List<ViewRequisitionRequestDetailsDto>();

                var requisitionDetails = await response.Content.ReadFromJsonAsync<List<ViewRequisitionRequestDetailsDto>>();

                return requisitionDetails ?? new List<ViewRequisitionRequestDetailsDto>();
            }
            catch
            {
                return new List<ViewRequisitionRequestDetailsDto>();
            }
        }

        public async Task<EventRequisitionDetailsDto?> ViewRequisitionRequestDetails(Guid requisitionId)
        {
            try
            {
                var response = await httpClient.GetAsync($"Admin/ViewRequisitionDetails/{requisitionId}");
                if (!response.IsSuccessStatusCode)
                    return null;

                var requisitionDetails = await response.Content.ReadFromJsonAsync<EventRequisitionDetailsDto>();

                return requisitionDetails ?? null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<ResponseResult> RejectEventRequisition(Guid requisitionId, string reason)
        {
            try
            {
                ResponseMessageDto responseMessage = new ResponseMessageDto() { ResponseMessage = reason };
                var response = await httpClient.PostAsJsonAsync($"Admin/RejectEventRequisition/{requisitionId}", responseMessage);

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

        public async Task<bool> ApproveEventRequisition(Guid requisitionId)
        {
            try
            {
                var response = await httpClient.GetAsync($"Admin/ApproveEventRequisition/{requisitionId}");
                if (!response.IsSuccessStatusCode)
                    return false;

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<ViewReservedNonFinancialRequirements?>> GetReservedNonFinancialResources()
        {
            try
            {
                var response = await httpClient.GetAsync("Admin/ViewReservedNonFinancialRequirements");
                if (!response.IsSuccessStatusCode)
                    return null!;

                var reservedResources = await response.Content.ReadFromJsonAsync<List<ViewReservedNonFinancialRequirements>>();

                if (reservedResources == null || reservedResources.Count == 0)
                    return null!;

                return reservedResources!;

            }
            catch
            {
                return null!;
            }
        }


        // Profile Details
        public async Task<MemberProfileDto?> GetProfile(Guid memberId)
        {
            var response = await httpClient.GetAsync($"Admin/viewProfile/{memberId}");

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
                var response = await httpClient.PutAsJsonAsync("Admin/updateProfile", updatedProfile);

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