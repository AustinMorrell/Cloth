using UnityEngine;
using System.Collections.Generic;

public class ClothBehave : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    public float mass;
    [SerializeField]
    private int rowLength = 5;
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private GameObject Damp;
    [SerializeField]
    private float gravityStrength = 9.8f;
    public List<Point> points;
    public List<Damper> dampers;
    public List<Triangle> tris = new List<Triangle>();

    void Start()
    {
        MakePoints(rowLength);
        MakeDampers();
    }

    void MakePoints(int a)
    {
        float x = 0f;
        float y = 0f;
        for (int i = 0; i < a; i++)
        {
            for (int j = 0; j < a; j++)
            {
                GameObject temp = Instantiate(prefab, new Vector3(x, y, 0), new Quaternion()) as GameObject;
                Point mp = temp.GetComponent<Point>();
                mp.transform.position = new Vector3(x, y, 0);
                mp.v = new Vector3(0, 0, 0);
                mp.m = mass;
                points.Add(mp);
                x += 4f;
            }
            x = 0f;
            y += 4f;
        }
    }

    void MakeDampers()
    {
        for (int i = 0; i < points.Count; i++)
        {
            if (i % rowLength != rowLength - 1)
            {
                GameObject temp = Instantiate(Damp, new Vector3(0, 0, 0), new Quaternion()) as GameObject;
                Damper dp = temp.GetComponent<Damper>();
                dp.P1 = points[i];
                dp.P2 = points[i + 1];
                dp.name = i.ToString();
                dampers.Add(dp);
            }

            if (i < ((rowLength * rowLength) - rowLength))
            {
                GameObject temp = Instantiate(Damp, new Vector3(0, 0, 0), new Quaternion()) as GameObject;
                Damper dp = temp.GetComponent<Damper>();
                dp.P1 = points[i];
                dp.P2 = points[i + rowLength];
                dp.name = i.ToString();
                dampers.Add(dp);
            }

            if (i % rowLength != (rowLength - 1) && i < (rowLength * rowLength) - rowLength)
            {
                GameObject temp = Instantiate(Damp, new Vector3(0, 0, 0), new Quaternion()) as GameObject;
                Damper dp = temp.GetComponent<Damper>();
                dp.P1 = points[i];
                dp.P2 = points[i + (rowLength + 1)];
                dp.name = i.ToString();
                dampers.Add(dp);
            }

            if (i % rowLength != 0 && i < (rowLength * rowLength) - rowLength)
            {
                GameObject temp = Instantiate(Damp, new Vector3(0, 0, 0), new Quaternion()) as GameObject;
                Damper dp = temp.GetComponent<Damper>();
                dp.P1 = points[i];
                dp.P2 = points[i + (rowLength - 1)];
                dp.name = i.ToString();
                dampers.Add(dp);
            }

            if (i == 0 || i == (rowLength - 1) || i == ((rowLength * rowLength) - 1) || i == ((rowLength * rowLength) - rowLength))
            {
                points[i].ap = false;
            }
        }
    }

    void MakeTriagnles()
    {

    }

    // Update is called once per frame
    void Update()
    {
        foreach (Point p in points)
        {
            if (p.ap)
            {
                ApplyGravity(p);
                //ApplyForces(p);
                //ApplyAerodynamics(p);
            }
        }
    }

    void ApplyGravity(Point p)
    {
        Vector3 gravityDirection = new Vector3(0, -1, 0);
        Vector3 gravityVector = gravityDirection * gravityStrength;
        p.f = gravityVector;
    }

    void ApplyForces(Point p)
    {
        
    }

    void ApplyAerodynamics(Point p)
    {
 
    }
}
