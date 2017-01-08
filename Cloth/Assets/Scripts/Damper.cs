using UnityEngine;
using System.Collections;

public class Damper {

    public float Ks = 10; // Spring constant
    public float kd = 10; // Damping factor
    public float L0 = 4; // Rest length
    public float L; // Length
    public Point P1, P2; // The conected particals
    private float V1, V2;
    public float speed = 1;
    Vector3 dir;

    public void Start()
    {
        // The inital distance between the to points becomes the rest length.
        L0 = Vector3.Distance(P1.r, P2.r);
    }

    public void ComputeForce()
    {
        ThreeDtoOneD();
        BacktoThreeD();
    }

    private void ThreeDtoOneD()
    {
        // Find the distance between the points.
        Vector3 dist = P2.r - P1.r;
        // Change that distance in to a singular value.
        L = dist.magnitude;
        // Divide the distance by the length to make a direction.
        dir = dist / L;
        V1 = Vector3.Dot(P1.v, dir);
        V2 = Vector3.Dot(P2.v, dir);
    }

    private void BacktoThreeD()
    {
        float fsd = (-Ks * (L0 - L)) - (kd * (V1 - V2));
        // Change the force of each point by the fsd times direction.
        if (P1.ap)
        { P1.f += fsd * dir * speed; }
        if (P2.ap)
        { P2.f += -(fsd * dir * speed); }
    }
}
