using UnityEngine;

public class SystemGenerator : MonoBehaviour
{
    System.Random rG;

    [SerializeField] string[] systemNames;
    [SerializeField] string[] systemPrefixes;
    [SerializeField] Material[] terrestrialMats;
    [SerializeField] Material[] gasGiantMats;

    public Vector2Int terrestrialSizeRange;
    public Vector2Int gasGiantSizeRange;

    public void Initialize(System.Random rG)
    {
        this.rG = rG;
    }

    public void GenerateSystem(StarSystem starSystem)
    {
        string prefix = systemPrefixes[rG.Next(0, systemPrefixes.Length)];
        string name = systemNames[rG.Next(0, systemNames.Length)];
        starSystem.SetName(prefix + " " + name);

        starSystem.BuildPlanets(this, rG, terrestrialMats, gasGiantMats);
    }

    public float GetSize(BodyType type, System.Random rG)
    {
        if (type == BodyType.Terrestrial) return rG.Next(terrestrialSizeRange.x, terrestrialSizeRange.y);
        else if (type == BodyType.GasGiant) return rG.Next(gasGiantSizeRange.x, gasGiantSizeRange.y);
        else return 0;
    }
}
