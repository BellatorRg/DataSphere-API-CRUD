
using Ejercicio2_APIDataSphere_Rg3.Models;

namespace Ejercicio2_APIDataSphere_Rg3.Repositories
{
    public interface IAssetRepository
    {
        List<Asset> GetAll();

        Asset? GetById(int id);

        void Add(Asset asset);

        void Update(Asset asset);

        void Delete(int id);

        decimal GetTotalExposure();
    }
}