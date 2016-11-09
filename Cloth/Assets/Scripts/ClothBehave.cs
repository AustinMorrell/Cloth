using UnityEngine;
using System.Collections.Generic;

public class ClothBehave : MonoBehaviour {

    [SerializeField]
    private int rowLength = 2;
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private GameObject Damp;
    public Point[] points;
    public Damper[] dampers;
    public List<Triangle> tris = new List<Triangle>();

    void Start()
    {
        points = FindObjectsOfType<Point>();
        dampers = FindObjectsOfType<Damper>();
    }

    // Use this for initialization
 //   void Start ()
 //   {
 //       MakePoints(rowLength);
 //       MakeDampers();
 //       MakeTriagnles();
	//}
	
 //   void MakePoints(int a)
 //   {
 //       float x = 0f;
 //       float y = 0f;
 //       int count = 0;
 //       for (int i = 0; i < a; i++)
 //       {
 //           for (int j = 0; j < a; j++, count++)
 //           {
 //               GameObject temp = Instantiate(prefab, new Vector3(x, y, 0), new Quaternion()) as GameObject;
 //               Point mp = temp.GetComponent<Point>();
 //               mp.r = new Vector3(x, y, 0);
 //               mp.v = new Vector3(0, 0, 0);
 //               mp.m = (1 <= 0) ? 1 : 1;
 //               points.Add(mp);
 //               x += 4f;
 //           }
 //           x = 0f;
 //           y += 4f;
 //       }
 //       points[points.Count - 1].ap = false;
 //       points[points.Count - a].ap = false;
 //   }

 //   void MakeDampers()
 //   {
 //       foreach (Point p in points)
 //       {
 //           int Index = FindIndex(points, p);
 //           GameObject temp = Instantiate(Damp, new Vector3(0, 0, 0), new Quaternion()) as GameObject;
 //           Damper d = temp.GetComponent<Damper>();

 //           d.P1 = p;
 //           int indexingFactor = 0;
 //           for (; Index > indexingFactor;)
 //           {
 //                   indexingFactor += rowLength;
 //           }

 //           if (Index < (indexingFactor + 1))
 //           {
 //               d.P2 = points[Index + 1];
 //           }
 //           else
 //           {
 //               Destroy(temp);
 //           }
 //       }
 //   }
 //   //d amage
 //   void MakeTriagnles()
 //   {

 //   }

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
        Vector3 g = new Vector3(0, -0.00981f, 0) * (p.m / Mathf.Pow(Time.deltaTime, 2));
        p.f += p.m * g;
    }

    void ApplyForces(Point p)
    {

    }

    void ApplyAerodynamics(Point p)
    {

    }

    //public int FindIndex(List<Point> list, Point mp)
    //{
    //    int index = 0;

    //    for (int i = 0; i < list.Count; i++)
    //    {
    //        if (list[i] == mp)
    //        {
    //            index = i;
    //            break;
    //        }
    //    }
    //    return index;
    //}
}
