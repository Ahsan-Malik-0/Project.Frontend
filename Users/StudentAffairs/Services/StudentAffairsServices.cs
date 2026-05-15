using Project.Frontend.Model;
using Project.Frontend.Model.DTOs;
using System.Net.Http.Json;
using System.Text.Json.Nodes;

namespace Project.Frontend.StudentAffairsServices
{
    public class StudentAffairsServices
    {
        private readonly HttpClient httpClient;

        public StudentAffairsServices(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<ViewRequisitionRequestDetailsDto>> GetRequisitionRequestDetails()
        {
            try
            {
                var response = await httpClient.GetAsync("StudentAffairs/ViewPendingRequisitions");
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
                var response = await httpClient.GetAsync($"StudentAffairs/ViewRequisitionDetails/{requisitionId}");
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

       

        // view Approved requisition details
        public async Task<List<ViewRequisitionDetailsForStudentAffairsDto>?> ViewApprovedRequisitionDetails()
        {
            try
            {
                var response = await httpClient.GetAsync($"StudentAffairs/ViewApprovedRequisitionDetails");
                if (!response.IsSuccessStatusCode)
                    return null;

                var requisitionDetails = await response.Content.ReadFromJsonAsync<List<ViewRequisitionDetailsForStudentAffairsDto>>();

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

                var response = await httpClient.PostAsJsonAsync($"StudentAffairs/RejectEventRequisition/{requisitionId}", responseMessage);
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

        public async Task<ResponseResult> ApproveEventRequisition(Guid requisitionId, ApproveEventRequisitionDto approveEventRequisition)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync($"StudentAffairs/ApproveEventRequisition/{requisitionId}", approveEventRequisition);
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

        public async Task<ResponseResult> MarkEventAsCompleted(Guid requisitionId)
        {
            try
            {
                var response = await httpClient.GetAsync($"StudentAffairs/MarkEventAsCompleted/{requisitionId}");
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
            var response = await httpClient.GetAsync($"StudentAffairs/viewProfile/{memberId}");

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
                var response = await httpClient.PutAsJsonAsync("StudentAffairs/updateProfile", updatedProfile);

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