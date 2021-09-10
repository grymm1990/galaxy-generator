using UnityEngine;

[CreateAssetMenu]
public class StarType : ScriptableObject
{
    public GameObject prefab;
    public float starRadius;
    public float frequency;

    public Vector2Int terrestrialCountRange;
    public Vector2Int gasGiantCountRange;
    public float beltChance;
}
