using UnityEngine;
using System.Collections;

public class Point {

    public Vector3 v = Vector3.zero; //velocity
    public Vector3 a = Vector3.zero; //acceleration
    public float m = 10.0f; //mass
    public Vector3 p = Vector3.zero; // momentum
    public Vector3 f = Vector3.zero; // force
    public Vector3 r = Vector3.zero; // position
    public bool CLicked;
    public bool ap = true;

    public void Start()
    {
        m = 10.0f;
    }

    public void Update()
    {
        a = f * (1 / m) ;
        v += a * Time.fixedDeltaTime;
        v = Vector3.ClampMagnitude(v, 5.0f);
        r += v * Time.fixedDeltaTime;
        if (r.y < 0)
        {
            r.y = 0;
        }
    }
}
