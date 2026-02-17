using Project.Frontend.Model;
using Project.Frontend.Model.DTOs;
using System.Net.Http.Json;
using System.Text.Json.Nodes;

namespace Project.Frontend.Services
{
    public class MemberServices
    {
        private readonly HttpClient httpClient;

        public MemberServices(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<MemberProfileDto?> GetMemberProfile(string memberId)
        {
            var response = await httpClient.GetAsync($"president/viewProfile?memberId={memberId}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var memberProfile = await response.Content.ReadFromJsonAsync<MemberProfileDto>();

            return memberProfile;
        }

        public async Task<ResponseResult> UpdateMemberProfile(MemberProfileUpdateDto updatedProfile)
        {
            try
            {
                var reponse = await httpClient.PutAsJsonAsync("president/updateProfile", updatedProfile);

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
