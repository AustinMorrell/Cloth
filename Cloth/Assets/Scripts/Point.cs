using UnityEngine;
using System.Collections;

public class Point : MonoBehaviour {

    public Vector3 r; //position
    public Vector3 v; //velocity
    public Vector3 a; //acceleration
    public float m; //mass
    public Vector3 p; // momentum
    public Vector3 f; //force

    void Update()
    {
        transform.position = r;
    }
}
