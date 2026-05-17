using Project.Frontend.Model;
using Project.Frontend.Model.DTOs;
using System.Net.Http.Json;
using System.Text.Json.Nodes;

namespace Project.Frontend.SharedServices
{
    public class YearlyBudgetScrutinyServices
    {
        private readonly HttpClient httpClient;

        public YearlyBudgetScrutinyServices(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<YearlyBudget>> GetYearlyBudgetScrutiny()
        {
            try
            {
                var response = await httpClient.GetAsync("YearlyBudgetScrutiny/yearlyBudgetDetails");
                if (!response.IsSuccessStatusCode)
                    return new List<YearlyBudget>();

                var yearlyBudgetScrutiny = await response.Content.ReadFromJsonAsync<List<YearlyBudget>>();

                return yearlyBudgetScrutiny ?? new List<YearlyBudget>();
            }
            catch
            {
                return new List<YearlyBudget>();
            }
        }

        public async Task<List<ViewScrutinyDetailsDto>> GetScrutinyDetails(Guid yearlyBudgetId)
        {
            try
            {
                var response = await httpClient.GetAsync($"YearlyBudgetScrutiny/scrutinyDetails/{yearlyBudgetId}");
                if (!response.IsSuccessStatusCode)
                    return null!;

                var scrutinyDetails = await response.Content.ReadFromJsonAsync<List<ViewScrutinyDetailsDto>>();

                return scrutinyDetails ?? null!;
            }
            catch
            {
                return null!;
            }
        }

        public async Task<ResponseResult> AddComment(Guid administrativeUnitId, AddCommentDto commentDto)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync($"YearlyBudgetScrutiny/addComment/{administrativeUnitId}", commentDto);
                if(!response.IsSuccessStatusCode)
                {
                    var resString = await response.Content.ReadAsStringAsync();
                    var jsonNode = JsonNode.Parse(resString);
                    var error = jsonNode?["errors"]?.ToString() ?? string.Empty;

                    return new ResponseResult() { Success = false, Error = error };
                }
                return new ResponseResult() { Success = true, Error = "Comment added successfully." };
            }
            catch (Exception ex)
            {
                return new ResponseResult() { Success = false, Error = $"An error occurred while adding the comment: {ex.Message}" };
            }
        }

        public async Task<ResponseResult> DeleteComment(Guid commentId)
        {
            try
            {
                var response = await httpClient.DeleteAsync($"YearlyBudgetScrutiny/deleteComment/{commentId}");
                if (!response.IsSuccessStatusCode)
                {
                    var resString = await response.Content.ReadAsStringAsync();
                    var jsonNode = JsonNode.Parse(resString);
                    var error = jsonNode?["errors"]?.ToString() ?? string.Empty;

                    return new ResponseResult() { Success = false, Error = error };
                }

                return new ResponseResult() { Success = true, Error = "Comment deleted successfully." };
            }
            catch (Exception ex)
            {
                return new ResponseResult() { Success = false, Error = $"An error occurred while deleting the comment: {ex.Message}" };
            }
        }

        public async Task<ResponseResult> ApproveYearlyBudget(Guid budgetId, ApproveYearlyBudgetDto dto)
        {
            var response = await httpClient.PostAsJsonAsync($"YearlyBudgetScrutiny/approveYearlyBudget/{budgetId}", dto);
            if (!response.IsSuccessStatusCode)
                return new ResponseResult { Success = false, Error = await response.Content.ReadAsStringAsync() };
            return new ResponseResult { Success = true };
        }
    }
}