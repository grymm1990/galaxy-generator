using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ST.Graphics
{
    public class LineRenderDrivers : MonoBehaviour
    {

        internal void DrawPolygon(LineRenderer lr, int vertexNumber, float radius, Transform trans, float startWidth, float endWidth)
        {
            lr.startWidth = startWidth;
            lr.endWidth = endWidth;
            lr.loop = true;
            float angle = 2 * Mathf.PI / vertexNumber;
            lr.positionCount = vertexNumber;

            for (int i = 0; i < vertexNumber; i++)
            {
                Matrix4x4 rotationMatrix = new Matrix4x4(new Vector4(Mathf.Cos(angle * i), Mathf.Sin(angle * i), 0, 0),
                                                         new Vector4(-1 * Mathf.Sin(angle * i), Mathf.Cos(angle * i), 0, 0),
                                           new Vector4(0, 0, 1, 0),
                                           new Vector4(0, 0, 0, 1));
                Vector3 initialRelativePosition = new Vector3(0, radius, 0);
                lr.SetPosition(i, trans.TransformPoint(rotationMatrix.MultiplyPoint(initialRelativePosition)));
            }
        }

        internal void DrawEllipse(LineRenderer lr, int pointCount, float radius, float radius2, Transform trans)
        {
            float x;
            float y;
            float z = 0f;
            lr.positionCount = pointCount;
            float angle = 1f;

            for (int i = 0; i < pointCount; i++)
            {
                x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
                y = Mathf.Cos(Mathf.Deg2Rad * angle) * radius2;

                lr.SetPosition(i, trans.TransformPoint(new Vector3(x, y, z)));

                angle += (360f / (pointCount - 1));
            }
        }

        void DrawQuadraticBezierCurve(LineRenderer lr, Vector3 point0, Vector3 point1, Vector3 point2)
        {
            lr.positionCount = 200;
            float t = 0f;
            Vector3 B = new Vector3(0, 0, 0);
            for (int i = 0; i < lr.positionCount; i++)
            {
                B = (1 - t) * (1 - t) * point0 + 2 * (1 - t) * t * point1 + t * t * point2;
                lr.SetPosition(i, B);
                t += (1 / (float)lr.positionCount);
            }

        }

        void DrawCubicBezierCurve(LineRenderer lr, Vector3 point0, Vector3 point1, Vector3 point2, Vector3 point3)
        {
            lr.positionCount = 200;
            float t = 0f;
            Vector3 B = new Vector3(0, 0, 0);
            for (int i = 0; i < lr.positionCount; i++)
            {
                B = (1 - t) * (1 - t) * (1 - t) * point0 + 3 * (1 - t) * (1 - t) *
                    t * point1 + 3 * (1 - t) * t * t * point2 + t * t * t * point3;

                lr.SetPosition(i, B);
                t += (1 / (float)lr.positionCount);
            }
        }
    }
}
