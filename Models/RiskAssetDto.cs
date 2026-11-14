//José Raúl Guerrero Parra
//Modelo de datos para el FE en React. 
//Cada objeto es la exposición de un asset a un hazard. 
namespace Ejercicio2_APIDataSphere_Rg3.Models
{
    public class RiskAssetDto
    {
        public int AssetId { get; set; }

        public string Name { get; set; } = "";

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public string RegionCode { get; set; } = "";

        public string HazardType { get; set; } = "";

        public int SeverityLevel { get; set; }

        public int ProbabilityScore { get; set; }

        public decimal AssetValue { get; set; }

        public int RiskScore => SeverityLevel * ProbabilityScore;
    }
}
