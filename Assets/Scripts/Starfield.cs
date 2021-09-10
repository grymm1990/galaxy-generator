using System;
using System.Collections.Generic;
using UnityEngine;

public static class Starfield
{
    public static List<Vector2> GenerateGrid(Vector2 root, Vector2 spawnAreaSize, float density, Texture2D filter, int seed)
    {
        System.Random pseudoRandom;
        if (seed == 0) pseudoRandom = new System.Random();
        else pseudoRandom = new System.Random(seed);

        int[,] spawnPoints = new int[Mathf.CeilToInt(spawnAreaSize.x), Mathf.CeilToInt(spawnAreaSize.y)];
        Vector2 interval = new Vector2(spawnAreaSize.x / spawnPoints.GetLength(0), spawnAreaSize.y / spawnPoints.GetLength(1));
        List <Vector2> points = new List<Vector2>();

        for (int x = 0; x < spawnPoints.GetLength(0); x++)
        {
            for (int z = 0; z < spawnPoints.GetLength(1); z++)
            {
                Vector2 newPoint = new Vector2((float)spawnAreaSize.x / -2 + interval.x * x, (float)spawnAreaSize.y / -2 + interval.y * z);
                if ((float)pseudoRandom.Next(1, 1000) / 1000 < GetDensity(newPoint,density,spawnAreaSize,filter))
                {
                    points.Add(root + newPoint);
                }
            }
        }
        return points;
    }


    private static float GetDensity(Vector2 point,float density,Vector2 spawnAreaSize,Texture2D filter)
    {
        if (filter == null) return density;

        int xPos = Mathf.CeilToInt((point.x / spawnAreaSize.x) * filter.width) + filter.width / 2;
        int yPos = Mathf.CeilToInt((point.y / spawnAreaSize.y) * filter.height) + filter.height / 2;

        Color pixelColor = filter.GetPixel(xPos, yPos);

        return pixelColor.grayscale * density * 0.01f;
    }
}
