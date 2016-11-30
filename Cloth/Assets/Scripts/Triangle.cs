using UnityEngine;
using System.Collections;

public class Triangle : MonoBehaviour {

    public Vector3 surfaceNormal;
    public Vector3 averageVelocity;
    public float areaOfTriangle;
    public Point TP1, TP2, TP3;

    public Triangle() { }

    public bool ComputeAerodynamicForce(Vector3 air)
    {
        air /= 10;

        Vector3 surface = ((TP1.v + TP2.v + TP3.v) / 3);
        averageVelocity = surface - air;

        surfaceNormal = Vector3.Cross((TP2.transform.position - TP1.transform.position), (TP3.transform.position - TP1.transform.position)) /
                        Vector3.Cross((TP2.transform.position - TP1.transform.position), (TP3.transform.position - TP1.transform.position)).magnitude;

        float ao = (1f / 2f) * Vector3.Cross((TP2.transform.position - TP1.transform.position),
            (TP3.transform.position - TP1.transform.position)).magnitude;
        areaOfTriangle = ao * (Vector3.Dot(averageVelocity, surfaceNormal) / averageVelocity.magnitude);

        Vector3 aeroForce = -(1f / 2f) * 1f * Mathf.Pow(averageVelocity.magnitude, 2) * 1f * areaOfTriangle * surfaceNormal;
        if (TP1.ap)
        {
            TP1.f += (aeroForce / 3);
        }
        if (TP2.ap)
        {
            TP2.f += (aeroForce / 3);
        }
        if (TP3.ap)
        {
            TP3.f += (aeroForce / 3);
        }
        return true;
    }
}
