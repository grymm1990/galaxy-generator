using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlanetData
{
    public BodyType type;
    public float size;
    public Material material;
    public float orbitalPeriod;

    public PlanetData(BodyType type, float size, Material material, float orbitalPeriod)
    {
        this.type = type;
        this.size = size;
        this.material = material;
        this.orbitalPeriod = orbitalPeriod;
    }
}
