using UnityEngine;
using System.Collections;

public class Triangle {

    public Vector3 surfaceNormal; // The divided cross product of all 3 points.
    public Vector3 averageVelocity; // The average velocity between the 3 points.
    public float areaOfTriangle; // The space the triangle takes up.
    public Point TP1, TP2, TP3; // The 3 points connected in the triangle.

    public Triangle() { }

    public bool ComputeAerodynamicForce(Vector3 air)
    {
        air /= 10;

        // Find the average velocity.
        Vector3 surface = ((TP1.v + TP2.v + TP3.v) / 3);
        averageVelocity = surface - air;

        // Find the surface normal.
        surfaceNormal = Vector3.Cross((TP2.r - TP1.r), (TP3.r - TP1.r)) /
                        Vector3.Cross((TP2.r - TP1.r), (TP3.r - TP1.r)).magnitude;

        float ao = (1f / 2f) * Vector3.Cross((TP2.r - TP1.r),
            (TP3.r - TP1.r)).magnitude;
        areaOfTriangle = ao * (Vector3.Dot(averageVelocity, surfaceNormal) / averageVelocity.magnitude);

        // Aero dynamic force is equal to half the magnitude of the average velocity 
        // squared times the area of the triangle times the surface normal.
        Vector3 aeroForce = -(1f / 2f) * 1f * Mathf.Pow(averageVelocity.magnitude, 2) * 1f * areaOfTriangle * surfaceNormal;

        // Apply.
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
