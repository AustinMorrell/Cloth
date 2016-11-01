using UnityEngine;
using System.Collections;

public class Damper : MonoBehaviour {

    public float Ks = 3; // Spring constant
    public float kd = 2; // Damping factor
    public float L0 = 0; // Rest length
    public float L = 3; // Length
    public Point P1, P2;
    private float V1, V2;
    private Vector3 f1, f2;
    Vector3 dir;

    void Start()
    {

    }

    public void ComputeForce()
    {
        ThreeDtoOneD();
        BacktoThreeD();
    }

    private void ThreeDtoOneD()
    {
        Vector3 dist = P2.r - P1.r;
        dir = dist.normalized;
        V1 = Vector3.Dot(P1.v, dir);
        V2 = Vector3.Dot(P2.v, dir);
    }

    private void BacktoThreeD()
    {
        float fsd = (-Ks * (L0 - L)) - (kd * (V1 - V2));
        P1.r = fsd * dir;
        P2.r = -(P1.r);
    }
}
