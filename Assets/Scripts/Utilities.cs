using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
    public static Texture2D RotateTexture(Texture2D originalTexture, bool clockwise)
    {
        Color32[] original = originalTexture.GetPixels32();
        Color32[] rotated = new Color32[original.Length];
        int w = originalTexture.width;
        int h = originalTexture.height;

        int iRotated, iOriginal;

        for (int j = 0; j < h; ++j)
        {
            for (int i = 0; i < w; ++i)
            {
                iRotated = (i + 1) * h - j - 1;
                iOriginal = clockwise ? original.Length - 1 - (j * w + i) : j * w + i;
                rotated[iRotated] = original[iOriginal];
            }
        }

        Texture2D rotatedTexture = new Texture2D(h, w);
        rotatedTexture.SetPixels32(rotated);
        rotatedTexture.Apply();
        return rotatedTexture;
    }

    public static bool PositionValid(List<Vector2> existingStars, Vector2 point, float minimumSeparation)
    {
        foreach (Vector2 starCoordinate in existingStars)
        {
            float dist = Vector2.Distance(point, starCoordinate);
            if (point == starCoordinate || dist < minimumSeparation) return false;
        }
        return true;
    }

    public static List<Vector2> AddPoints(List<Vector2> galaxyPoints, List<Vector2> addedPoints)
    {
        foreach (Vector2 point in addedPoints)
        {
            galaxyPoints.Add(point);
        }
        return galaxyPoints;
    }

    public static StarType GetRandomStarType(System.Random rG, StarType[] starTypes)
    {
        StarType newType = null;

        float randomRoll = (float)rG.Next(0, 100) / 100f;
        float currentValue = 0;
        for (int i = 0; i < starTypes.Length; i++)
        {
            currentValue += starTypes[i].frequency / 100f;
            if (randomRoll < currentValue)
            {
                newType = starTypes[i];
                break;
            }
        }
        return newType;
    }
}
