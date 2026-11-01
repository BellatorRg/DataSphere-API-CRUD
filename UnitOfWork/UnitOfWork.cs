using Ejercicio2_APIDataSphere_Rg3.Repositories;

namespace Ejercicio2_APIDataSphere_Rg3.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public IAssetRepository Assets { get; }

        public UnitOfWork(IAssetRepository assetRepository)
        {
            Assets = assetRepository;
        }
    }
}
