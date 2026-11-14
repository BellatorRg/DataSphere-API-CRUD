//José Raúl Guerrero Parra

namespace Ejercicio2_APIDataSphere_Rg3.Models
{
    public class Asset
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public int CategoryId { get; set; }
        public int ConditionId { get; set; }
        public decimal BaseValue { get; set; }
    }
}