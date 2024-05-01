using Application.Services.Model;

namespace Application.Interfaces
{
    public interface IRefDataGenreService
    {
        Task<IEnumerable<ModelObject>> GetData();
        Task<ModelObject> PostData(ModelName modelName);
        Task<ModelObject> PutData(ModelObject modelObject);
        Task<ModelObject> DeleteData(ModelId modelId);
        Task<ModelObject> GetByIdData(ModelId modelId);
    }
}
