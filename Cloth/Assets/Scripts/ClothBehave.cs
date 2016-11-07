using UnityEngine;
using System.Collections.Generic;

public class ClothBehave : MonoBehaviour {

    [SerializeField]
    private GameObject prefab;
    public List<Point> points = new List<Point>();
    public List<Damper> dampers = new List<Damper>();
    public List<Triangle> tris = new List<Triangle>();

    // Use this for initialization
    void Start ()
    {
        MakePoints(6);
        MakeDampers();
        MakeTriagnles();
	}
	
    void MakePoints(int a)
    {
        float x = 0f;
        float y = 0f;
        int count = 0;
        for (int i = 0; i < a; i++)
        {
            for (int j = 0; j < a; j++, count++)
            {
                GameObject temp = Instantiate(prefab, new Vector3(x, y, 0), new Quaternion()) as GameObject;
                Point mp = temp.GetComponent<Point>();
                mp.r = new Vector3(x, y, 0);
                mp.v = new Vector3(0, 0, 0);
                mp.m = (1 <= 0) ? 1 : 1;
                points.Add(mp);
                x += 4f;
            }
            x = 0f;
            y += 4f;
        }
        points[points.Count - 1].ap = false;
        points[points.Count - a].ap = false;
    }

    void MakeDampers()
    {

    }

    void MakeTriagnles()
    {

    }

	// Update is called once per frame
	void Update ()
    {
        foreach (Point p in points)
        {
            if (p.ap)
            {
                ApplyGravity(p);
                ApplyForces(p);
                ApplyAerodynamics(p);
            }
        }
	}

    void ApplyGravity(Point p)
    {
        Vector3 g = new Vector3(0, -0.0000981f, 0) * (p.m / Mathf.Pow(Time.deltaTime, 2));
        p.f += p.m * g;
    }

    void ApplyForces(Point p)
    {

    }

    void ApplyAerodynamics(Point p)
    {

    }
}
