using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ST.Graphics
{
    public class Circle : LineRenderDrivers
    {
        [SerializeField] LineRenderer lr;

        [SerializeField] float radius = 1f;
        [SerializeField] int pointCount = 5;
        [SerializeField] float lineWidth = 0.2f;


        private void OnValidate()
        {
            DrawPolygon(lr, pointCount, radius, transform, lineWidth, lineWidth);
        }

        void Update()
        {
            DrawPolygon(lr, pointCount, radius, transform, lineWidth, lineWidth);
        }
    }
}
