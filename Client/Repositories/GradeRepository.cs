using Client.Contracts;
using Client.DataTransferObjects.Grades;
using Client.Utilities.Handlers;
using Newtonsoft.Json;
using System.Text;

namespace Client.Repositories;

public class GradeRepository : BaseRepository<GradeDtoGet, Guid>, IGradeRepository
{
    public GradeRepository(string request = "Grade/") : base(request)
    {
    }

    public async Task<ResponseHandler<GradeDtoGenerateScore>> PostGenerate(GradeDtoGenerateScore entity)
    {
        ResponseHandler<GradeDtoGenerateScore> entityDto = null!;
        StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
        using (var response = HttpClient.PostAsync(Request + "CreateGenerate", content).Result)
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entityDto = JsonConvert.DeserializeObject<ResponseHandler<GradeDtoGenerateScore>>(apiResponse);
        }
        return entityDto;
    }

    public async Task<ResponseHandler<GradeDtoGet>> PutGenerate(Guid id, GradeDtoGet entity)
    {
        ResponseHandler<GradeDtoGet> entityDto = null!;
        StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
        using (var response = HttpClient.PutAsync(Request + "UpdateGenerate", content).Result)
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entityDto = JsonConvert.DeserializeObject<ResponseHandler<GradeDtoGet>>(apiResponse);
        }
        return entityDto;
    }
}
