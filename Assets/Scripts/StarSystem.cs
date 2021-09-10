using UnityEngine;

public class StarSystem : MonoBehaviour
{
    [SerializeField] StarType starType;
    [SerializeField] string typeString;
    string systemName;

    public PlanetData[] planetDatas;

    public string SystemName { get { return systemName; } }
    public string TypeString { get { return typeString; } }


    public void SetName(string newName)
    {
        systemName = newName;
    }

    public StarType GetStarType()
    {
        return starType;
    }

    public void BuildPlanets(SystemGenerator sG, System.Random rG, Material[] terrestrialMats, Material[] gasGiantMats)
    {
        InitializePlanets(sG, rG);

        for (int i = 0; i < planetDatas.Length; i++)
        {
            if (planetDatas[i].type == BodyType.Terrestrial)
            {
                int matIndex = rG.Next(0, terrestrialMats.Length);
                planetDatas[i].material = terrestrialMats[matIndex];
            }
            else if (planetDatas[i].type == BodyType.GasGiant)
            {
                int matIndex = rG.Next(0, gasGiantMats.Length);
                planetDatas[i].material = gasGiantMats[matIndex];
            }

            planetDatas[i].orbitalPeriod = rG.Next(0, 360);
            //TODO possibly check for angles too close ot each other.
        }
        //Debug.Log("Built system with " + planetDatas.Length + " orbitals.");
    }

    void InitializePlanets(SystemGenerator sG, System.Random rG)
    {
        int terraCount = rG.Next(starType.terrestrialCountRange.x, starType.terrestrialCountRange.y);
        int gasCount = rG.Next(starType.gasGiantCountRange.x, starType.gasGiantCountRange.y);

        planetDatas = new PlanetData[terraCount + gasCount];

        for (int i = 0; i < planetDatas.Length; i++)
        {
            bool isBelt = (rG.Next(0, 100) < starType.beltChance);
            BodyType type = GetBodyType(i, isBelt, terraCount);
            float size = sG.GetSize(type, rG) / 10f;

            planetDatas[i] = new PlanetData(type, size, null, 0f);
        }
    }

    BodyType GetBodyType (int index, bool isBelt, int terraCount)
    {
        if (index < terraCount)
        {
            if (isBelt) return BodyType.Belt;
            else return BodyType.Terrestrial;
        }
        else return BodyType.GasGiant;
    }
}
