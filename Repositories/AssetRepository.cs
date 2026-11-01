

using Ejercicio2_APIDataSphere_Rg3.Models;

namespace Ejercicio2_APIDataSphere_Rg3.Repositories
{
    public class AssetRepository : IAssetRepository
    {
        private readonly List<Asset> _assets = new();

        // Obtener todos los activos
        public List<Asset> GetAll()
        {
            return _assets;
        }

        // Obtener un activo por su identificador
        public Asset? GetById(int id)
        {
            return _assets.FirstOrDefault(a => a.Id == id);
        }

        // Crear un activo, evitando identificadores duplicados
        public void Add(Asset asset)
        {
            var existing = GetById(asset.Id);

            if (existing != null)
            {
                throw new InvalidOperationException(
                    "Ya existe un activo con ese ID");
            }

            _assets.Add(asset);
        }

        // Actualizar un activo existente
        public void Update(Asset asset)
        {
            var existing = GetById(asset.Id);

            if (existing != null)
            {
                existing.Name = asset.Name;
                existing.Latitude = asset.Latitude;
                existing.Longitude = asset.Longitude;
                existing.CategoryId = asset.CategoryId;
                existing.ConditionId = asset.ConditionId;
                existing.BaseValue = asset.BaseValue;
            }
        }

        // Eliminar un activo por su identificador
        public void Delete(int id)
        {
            var asset = GetById(id);

            if (asset != null)
            {
                _assets.Remove(asset);
            }
        }

        // Calcular el valor económico total de los activos
        public decimal GetTotalExposure()
        {
            return _assets.Sum(a => a.BaseValue);
        }
    }
}
