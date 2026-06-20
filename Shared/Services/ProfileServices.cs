using Project.Frontend.Model;
using Project.Frontend.Model.DTOs;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Nodes;

namespace Project.Frontend.Shared.Services
{
    public class ProfileServices
    {
        private readonly HttpClient httpClient;

        public ProfileServices(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<MemberProfileDto?> GetProfile(Guid memberId)
        {
            var response = await httpClient.GetAsync($"auth/viewProfile/{memberId}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var memberProfile = await response.Content.ReadFromJsonAsync<MemberProfileDto>();

            return memberProfile;
        }

        public async Task<ResponseResult> UpdateProfile(UpdateMemberProfileDto updatedProfile)
        {
            try
            {
                var reponse = await httpClient.PutAsJsonAsync($"auth/updateProfile/{updatedProfile.Id}", updatedProfile);

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

        public async Task<ViewMemberProfileDto?> GetMemberProfile(Guid memberId)
        {
            var response = await httpClient.GetAsync($"auth/viewMemberProfile/{memberId}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var profile = await response.Content.ReadFromJsonAsync<ViewMemberProfileDto>();

            return profile;
        }

        public async Task<ResponseResult> UpdateMemberProfile(Guid memberId, EditMemberProfileDto updatedProfile)
        {
            try
            {
                var response = await httpClient.PutAsJsonAsync($"auth/updateMemberProfile/{memberId}", updatedProfile);

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
