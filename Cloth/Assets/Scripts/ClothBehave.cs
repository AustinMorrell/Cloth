using UnityEngine;
using System.Collections.Generic;

public class ClothBehave : MonoBehaviour {

    public List<Point> points = new List<Point>();
    public List<Damper> dampers = new List<Damper>();

    // Use this for initialization
    void Start () {
        foreach (Point a in FindObjectsOfType<Point>())
        {
            points.Add(a);
        }

        foreach (Damper b in FindObjectsOfType<Damper>())
        {
            dampers.Add(b);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        foreach (Point p in points)
        {
            ApplyGravity(p);
            ApplyForces();
            ApplyAerodynamics();
        }
	}

    void ApplyGravity(Point p)
    {
        Vector3 g = new Vector3(0, -0.0000981f, 0) * (p.m / Mathf.Pow(Time.deltaTime, 2));
        p.f += p.m * g;
    }

    void ApplyForces()
    {

    }

    void ApplyAerodynamics()
    {

    }
}
