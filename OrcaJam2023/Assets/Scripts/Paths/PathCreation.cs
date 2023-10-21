using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathCreation
{
    static float _minPointDist = .1f;
    static float _maxPointDist = .25f;

    static float _minPointHeightDiff = -.5f;
    static float _maxPointHeightDiff = .5f;

    static float _minPointHeight = -4f;
    static float _maxPointHeight = 4f;

    public static List<Vector3> CreatePathPoints(Vector3 startPos, Vector3 endPos)
    {
        List<Vector3> points = new();
        points.Add(startPos);
        Vector3 curPoint = startPos;

        int index = 0;
        while (curPoint != endPos)
        {
            index++;

            if (index > 10000)
            {
                Debug.LogError("Infinite Loop");
                return null;
            }

            float randDist = Random.Range(_minPointDist, _maxPointDist);
            float randHeight = curPoint.y + Random.Range(_minPointHeightDiff, _maxPointHeightDiff);
            randHeight = Mathf.Clamp(randHeight, _minPointHeight, _maxPointHeight);

            float distToEnd = endPos.x - curPoint.x;
            if (distToEnd <= randDist)
            {
                curPoint = endPos;
                points.Add(curPoint);
                break;
            }


            Vector3 newPoint = new(curPoint.x + randDist, randHeight);
            points.Add(newPoint);

            curPoint = newPoint;
        }

        return points;
    }

    /*public static List<Vector2> CreateRandomizedBezierPath(Vector2 startPos, Vector2 endPos, int numPoints)
    {
        List<Vector2> points = new List<Vector2>();
        points.Add(startPos);

        for (int i = 1; i < numPoints; i++)
        {
            float t = i / (float)(numPoints - 1);
            float randDist = Random.Range(0, _maxPointDist);
            float randHeight = Random.Range(-_maxPointHeight, _maxPointHeight);
            Vector2 point = Vector2.Lerp(startPos, endPos, t) + new Vector2(randDist, randHeight);
            points.Add(point);
        }

        points.Add(endPos);

        return points;
    }*/
}
