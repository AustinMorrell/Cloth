using UnityEngine;
using System.Collections;

public class Damper : MonoBehaviour {

    public float Ks = 10; // Spring constant
    public float kd = 100; // Damping factor
    public float L0; // Rest length
    private float L; // Length
    public Point P1, P2;
    private float V1, V2;
    private Vector3 f1, f2;
    Vector3 dir;

    void Start()
    {
        L0 = Vector3.Distance(P1.transform.position, P2.transform.position);
    }

    void Update()
    {
        ComputeForce();
        Debug.DrawLine(P1.transform.position, P2.transform.position);
    }

    public void ComputeForce()
    {
        ThreeDtoOneD();
        BacktoThreeD();
    }

    private void ThreeDtoOneD()
    {
        Vector3 dist = P2.transform.position - P1.transform.position;
        L = dist.magnitude;
        dir = dist / L;
        V1 = Vector3.Dot(P1.v, dir);
        V2 = Vector3.Dot(P2.v, dir);
    }

    private void BacktoThreeD()
    {
        float fsd = (-Ks * (L0 - L)) - (kd * (V1 - V2));
        if (P1.ap)
        { P1.f += fsd * dir; }
        if (P2.ap)
        { P2.f += -(fsd * dir); }
    }
}
