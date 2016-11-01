using UnityEngine;
using System.Collections;

public class Point : MonoBehaviour {

    public Vector3 r = Vector3.zero; //position
    public Vector3 v = Vector3.zero; //velocity
    public Vector3 a = Vector3.zero; //acceleration
    public float m = 0.0f; //mass
    public Vector3 p = Vector3.zero; // momentum
    public Vector3 f = Vector3.zero; //force

    void Start()
    {
        m = 1.0f;
    }

    void Update()
    {
        a = f * (1 / m);
        v = v + (a * Time.deltaTime);
        r = r + (v * Time.deltaTime);
        transform.position = r;
    }
}
