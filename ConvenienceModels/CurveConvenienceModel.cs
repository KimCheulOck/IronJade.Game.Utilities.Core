using System.Collections.Generic;
using UnityEngine;

namespace IronJade.Util.Core
{
    public class CurveConvenienceModel
    {
        public Vector3[] GetQuadraticBezierPoints(int segment, Vector3 curve, Vector3 startPoint, Vector3 endPoint)
        {
            Vector3 start = startPoint + ((endPoint - startPoint) / 2);
            Vector3 curvePoint = start + (curve * 1);
            Vector3[] paths = new Vector3[segment];

            for (int i = 0; i < segment; ++i)
            {
                float t = i / (segment - 1.0f);
                Vector3 p0 = Mathf.Pow(1 - t, 2) * startPoint;    // = (1-t) * (1-t) * p0 = (1-t)^p0
                Vector3 p1 = 2 * (1 - t) * t * curvePoint;        // = 2 * (1-t) * t * p1 = 2(1-t)tp1
                Vector3 p2 = t * t * endPoint;                    // = t * t * p2         = t2p2

                paths[i] = p0 + p1 + p2;
            }

            return paths;
        }

        public Vector3[] GetLinearBezierPoints(int segment, Vector3 startPoint, Vector3 endPoint)
        {
            Vector3[] paths = new Vector3[segment];

            for (int i = 0; i < segment; ++i)
            {
                float t = i / (segment - 1.0f);
                Vector3 p0 = (1 - t) * startPoint;                  // = (1-t) * p0 = (1-t)p0
                Vector3 p1 = t * endPoint;                          // = t * p1     = tp1

                paths[i] = p0 + p1;
            }

            return paths;
        }

        public Vector3[] GetSplineBezierPoints(int segment, Vector3[] splinePoints)
        {
            List<Vector3> paths = new List<Vector3>();

            for (int i = 0; i < splinePoints.Length - 2; i++)
            {
                if (splinePoints[i] == null)
                    continue;

                if (splinePoints[i + 1] == null)
                    continue;

                if (splinePoints[i + 2] == null)
                    continue;

                Vector3 p0 = 0.5f * (splinePoints[i] + splinePoints[i + 1]);
                Vector3 p1 = splinePoints[i + 1];
                Vector3 p2 = 0.5f * (splinePoints[i + 1] + splinePoints[i + 2]);

                float t;
                float pointStep = 1.0f / segment;

                if (i == splinePoints.Length - 3)
                    pointStep = 1.0f / (segment - 1.0f);

                for (int j = 0; j < segment; j++)
                {
                    t = j * pointStep;

                    paths.Add((Mathf.Pow(1 - t, 2) * p0) +
                              (2.0f * (1.0f - t) * t * p1) + (t * t * p2));
                }
            }

            return paths.ToArray();
        }

        public Vector3[] GetSplineBezierPoints(int segment, Vector3 startPoint, Vector3 centerPoint, Vector3 endPoint)
        {
            Vector3 p0 = 0.5f * (startPoint + centerPoint);
            Vector3 p1 = centerPoint;
            Vector3 p2 = 0.5f * (centerPoint + endPoint);
            Vector3[] paths = new Vector3[segment];

            float t;
            float pointStep = 1.0f / (segment - 1.0f);

            for (int j = 0; j < segment; j++)
            {
                t = j * pointStep;

                paths[j] = (Mathf.Pow(1 - t, 2) * p0) +
                           (2.0f * (1.0f - t) * t * p1) + (t * t * p2);
            }

            return paths;
        }
    }
}