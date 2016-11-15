using UnityEngine;
using System.Collections;

public class Point : MonoBehaviour {

    public Vector3 v = Vector3.zero; //velocity
    public Vector3 a = Vector3.zero; //acceleration
    public float m = 10.0f; //mass
    public Vector3 p = Vector3.zero; // momentum
    public Vector3 f = Vector3.zero; //force
    public bool ap = true;

    void Start()
    {
        m = 10.0f;
    }

    void LateUpdate()
    {
        a = f * (1 / m) ;
        v += a * Time.fixedDeltaTime;
        transform.position += v * Time.fixedDeltaTime;
    }
}
