using UnityEngine;
using System.Collections;

public class Triangle : MonoBehaviour {

    public Vector3 surfaceNormal;
    public Vector3 averageVelocity;
    public float areaOfTriangle;
    public float windCoefficient = 1f;
    public Point TP1, TP2, TP3;
    public Damper SD1, SD2, SD3;

    public Triangle() { }

    public Triangle(Point mpOne, Point mpTwo, Point mpThree)
    {
        TP1 = mpOne;
        TP2 = mpTwo;
        TP3 = mpThree;
    }

    public bool ComputeAerodynamicForce(Vector3 air)
    {
        Vector3 surface = ((TP1.v + TP2.v + TP3.v) / 3);
        averageVelocity = surface - air;
        surfaceNormal = Vector3.Cross((TP2.r - TP1.r), (TP3.r - TP1.r)) / Vector3.Cross((TP2.r - TP1.r), (TP3.r - TP1.r)).magnitude;
        float ao = (1f / 2f) * Vector3.Cross((TP2.r - TP1.r), (TP3.r - TP1.r)).magnitude;
        areaOfTriangle = ao * (Vector3.Dot(averageVelocity, surfaceNormal) / averageVelocity.magnitude);
        Vector3 aeroForce = -(1f / 2f) * 1f * Mathf.Pow(averageVelocity.magnitude, 2) * 1f * areaOfTriangle * surfaceNormal;
        TP1.f += aeroForce / 3;
        TP2.f += aeroForce / 3;
        TP3.f += aeroForce / 3;
        return true;
    }
}
