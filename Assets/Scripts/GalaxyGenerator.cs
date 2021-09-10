using System.Collections.Generic;
using UnityEngine;

public class GalaxyGenerator : MonoBehaviour
{
    System.Random rG;

    [SerializeField] int resolution = 1;
    [SerializeField] float density = 0.005f;
    [SerializeField] float armOffset = -30f;
    [SerializeField] float maxRotation;
    [SerializeField] int numberOfArms;
    [SerializeField] float minimumSeparation = 3;

    Vector2 coreDimensions;
    Vector2 armDimensions;

    [SerializeField] SystemGenerator systemGenerator;
    [SerializeField] Transform galaxyContainer;
    [SerializeField] Texture2D coreFilter;
    [SerializeField] Texture2D armFilter;

    [SerializeField] IntegerVariable state;

    public void Generate(GenerationParameters parameters)
    {
        state.value = 2;
        if (parameters.seed == 0) rG = new System.Random();
        else rG = new System.Random(parameters.seed);

        systemGenerator.Initialize(rG);

        coreDimensions = new Vector2((float)rG.Next(300, 500), (float)rG.Next(300, 500));
        armDimensions = new Vector2((float)rG.Next(400, 600), (float)rG.Next(200, 500));

        numberOfArms = parameters.arms;
        maxRotation = parameters.rotation;

        List<Vector2> starPoints = GeneratePoints(parameters.seed);
        GenerateStars(starPoints, parameters.starTypes);
        state.value = 1;
    }

    public void ClearThenGenerate(GenerationParameters parameters)
    {
        state.value = 2;
        for (int i = galaxyContainer.childCount-1; i > -1; i--)
        {
            Destroy(galaxyContainer.GetChild(i).gameObject);
        }
        Generate(parameters);
    }

    void GenerateStars(List<Vector2> drawPoints, StarType[] starTypes)
    {
        List<Vector2> generatedStarCoordinates = new List<Vector2>();

        foreach (Vector2 point in drawPoints)
        {
            if (Utilities.PositionValid(generatedStarCoordinates, point, minimumSeparation))
            {
                StarType randomStarType = Utilities.GetRandomStarType(rG,starTypes);
                GameObject newStar = Instantiate(randomStarType.prefab, galaxyContainer);
                newStar.transform.position = point;

                //Disable light on galaxy stars
                newStar.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);

                StarSystem starSystem = newStar.GetComponent<StarSystem>();
                systemGenerator.GenerateSystem(starSystem);

                generatedStarCoordinates.Add(point);
            }
        }
    }

    List<Vector2> GeneratePoints(int seed)
    {
        List<Vector2> galaxyPoints = new List<Vector2>();

        List<Vector2> corePoints = Starfield.GenerateGrid(Vector3.zero, coreDimensions, density, coreFilter, seed);
        Utilities.AddPoints(galaxyPoints, corePoints);

        Vector2 armCenter1 = new Vector2((coreDimensions.x / 2 + armDimensions.y / 2), 0);
        Vector2 armCenter2 = new Vector2((-coreDimensions.x / 2 - armDimensions.y / 2), 0);
        armCenter1 += new Vector2(armOffset, 0);
        armCenter2 -= new Vector2(armOffset, 0);

        //Arm 1
        List<Vector2> arm1 = Starfield.GenerateGrid(armCenter1, armDimensions, density, armFilter, seed);
        arm1 = RotatePoints(arm1, (coreDimensions.x + armDimensions.x + armOffset), maxRotation);
        Utilities.AddPoints(galaxyPoints, arm1);

        //Arm 2
        List<Vector2> arm2 = Starfield.GenerateGrid(armCenter2, armDimensions, density, armFilter, seed);
        arm2 = RotatePoints(arm2, (coreDimensions.x + armDimensions.x + armOffset), maxRotation);
        Utilities.AddPoints(galaxyPoints, arm2);

        if (numberOfArms == 4)
        {
            Vector2 armDimensionsFlipped = new Vector2(armDimensions.y, armDimensions.x);
            Texture2D armFilterRotated = Utilities.RotateTexture(armFilter, true);

            Vector2 armCenter3 = new Vector2(0, (coreDimensions.y / 2 + armDimensions.x / 2));
            Vector2 armCenter4 = new Vector2(0, (-coreDimensions.y / 2 - armDimensions.x / 2));
            armCenter3 += new Vector2(armOffset, 0);
            armCenter4 -= new Vector2(armOffset, 0);
            
            //Arm 3
            List<Vector2> arm3 = Starfield.GenerateGrid(armCenter3, armDimensionsFlipped, density, armFilterRotated, seed);
            arm3 = RotatePoints(arm3, (coreDimensions.y  + armDimensions.x + armOffset), maxRotation);
            Utilities.AddPoints(galaxyPoints, arm3);

            //Arm 4
            List<Vector2> arm4 = Starfield.GenerateGrid(armCenter4, armDimensionsFlipped, density, armFilterRotated, seed);
            arm4 = RotatePoints(arm4, (coreDimensions.y  + armDimensions.x + armOffset), maxRotation);
            Utilities.AddPoints(galaxyPoints, arm4);
        }
        return galaxyPoints;
    }

    List<Vector2> RotatePoints(List<Vector2> points, float maxDistance, float maxRotation)
    {
        List<Vector2> newPoints = new List<Vector2>();
        float distanceFactor = 0;
        float angle = 0;
        foreach (Vector2 point in points)
        {
            distanceFactor = point.magnitude / maxDistance;
            angle = Mathf.Lerp(0, maxRotation, distanceFactor);
            Vector2 newPoint = Quaternion.Euler(0, 0, angle) * point;
            newPoint = new Vector2(Mathf.RoundToInt(newPoint.x), Mathf.RoundToInt(newPoint.y));
            newPoints.Add(newPoint);
        }
        return newPoints;
    }
}
