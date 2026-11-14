//José Raúl Guerrero Parra
//Datos simulados de assets y exposición a hazards. 
//Esto es lo que alimentará el FE React. 

using Ejercicio2_APIDataSphere_Rg3.Models;
using System.Collections.Generic;

namespace Ejercicio2_APIDataSphere_Rg3.Data
{
    public static class RiskAssetsData
    {
        public static List<RiskAssetDto> GetRiskAssets()
        {
            return new List<RiskAssetDto>
            {
                // ASSET 1 - Edificio Miami Residential Pleasures
                new RiskAssetDto
                {
                    AssetId = 1,
                    Name = "Edificio Miami Residential Pleasures",
                    Latitude = 25.7617m,
                    Longitude = -80.1918m,
                    RegionCode = "MIA",
                    HazardType = "Hurricane",
                    SeverityLevel = 5,
                    ProbabilityScore = 4,
                    AssetValue = 1500000m
                },

                // ASSET 2 - Casino Miami Vice
                new RiskAssetDto
                {
                    AssetId = 2,
                    Name = "Casino Miami Vice",
                    Latitude = 25.7900m,
                    Longitude = -80.2200m,
                    RegionCode = "MIA",
                    HazardType = "Hurricane",
                    SeverityLevel = 4,
                    ProbabilityScore = 5,
                    AssetValue = 4000000m
                },

                new RiskAssetDto
                {
                    AssetId = 2,
                    Name = "Casino Miami Vice",
                    Latitude = 25.7900m,
                    Longitude = -80.2200m,
                    RegionCode = "MIA",
                    HazardType = "Windstorm",
                    SeverityLevel = 3,
                    ProbabilityScore = 4,
                    AssetValue = 4000000m
                },

                // ASSET 3 - Explotacion Agricola Pollos Popeye
                new RiskAssetDto
                {
                    AssetId = 3,
                    Name = "Explotacion Agricola Pollos Popeye",
                    Latitude = 26.1200m,
                    Longitude = -80.3000m,
                    RegionCode = "HML",
                    HazardType = "Hurricane",
                    SeverityLevel = 3,
                    ProbabilityScore = 2,
                    AssetValue = 900000m
                },

                new RiskAssetDto
                {
                    AssetId = 3,
                    Name = "Explotacion Agricola Pollos Popeye",
                    Latitude = 26.1200m,
                    Longitude = -80.3000m,
                    RegionCode = "HML",
                    HazardType = "Rainstorm",
                    SeverityLevel = 4,
                    ProbabilityScore = 4,
                    AssetValue = 900000m
                },

                // ASSET 4 - Residencia Abuelos Zumbones
                new RiskAssetDto
                {
                    AssetId = 4,
                    Name = "Residencia Abuelos Zumbones",
                    Latitude = 26.1224m,
                    Longitude = -80.1373m,
                    RegionCode = "FLL",
                    HazardType = "Flood",
                    SeverityLevel = 4,
                    ProbabilityScore = 3,
                    AssetValue = 2200000m
                },

                new RiskAssetDto
                {
                    AssetId = 4,
                    Name = "Residencia Abuelos Zumbones",
                    Latitude = 26.1224m,
                    Longitude = -80.1373m,
                    RegionCode = "FLL",
                    HazardType = "Hurricane",
                    SeverityLevel = 5,
                    ProbabilityScore = 4,
                    AssetValue = 2200000m
                },

                // ASSET 5 - Centro Cultural Scarface
                new RiskAssetDto
                {
                    AssetId = 5,
                    Name = "Centro Cultural Scarface",
                    Latitude = 28.5383m,
                    Longitude = -81.3792m,
                    RegionCode = "MIA",
                    HazardType = "Hurricane",
                    SeverityLevel = 3,
                    ProbabilityScore = 5,
                    AssetValue = 1800000m
                },

                new RiskAssetDto
                {
                    AssetId = 5,
                    Name = "Centro Cultural Scarface",
                    Latitude = 28.5383m,
                    Longitude = -81.3792m,
                    RegionCode = "MIA",
                    HazardType = "Tornado",
                    SeverityLevel = 4,
                    ProbabilityScore = 5,
                    AssetValue = 1800000m
                },

                // ASSET 6 - Almacen Miami Polvo Blanco
                new RiskAssetDto
                {
                    AssetId = 6,
                    Name = "Almacen Miami Polvo Blanco",
                    Latitude = 28.6000m,
                    Longitude = -81.4200m,
                    RegionCode = "MIA",
                    HazardType = "Windstorm",
                    SeverityLevel = 4,
                    ProbabilityScore = 3,
                    AssetValue = 750000m
                },

                new RiskAssetDto
                {
                    AssetId = 6,
                    Name = "Almacen Miami Polvo Blanco",
                    Latitude = 28.6000m,
                    Longitude = -81.4200m,
                    RegionCode = "MIA",
                    HazardType = "Rainstorm",
                    SeverityLevel = 3,
                    ProbabilityScore = 4,
                    AssetValue = 750000m
                }
            };
        }
    }
}
