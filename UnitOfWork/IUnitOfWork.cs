using Ejercicio2_APIDataSphere_Rg3.Repositories;

namespace Ejercicio2_APIDataSphere_Rg3.UnitOfWork
{
    public interface IUnitOfWork
    {
        IAssetRepository Assets { get; }
    }
}
