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
    private GameObject Trian;
    [SerializeField]
    private float gravityStrength = 9.8f;
    [SerializeField]
    private Vector3 air;
    [SerializeField]
    private float speed;
    public List<Point> points;
    public List<Damper> dampers;
    public List<Triangle> tris = new List<Triangle>();

    void Start()
    {
        MakePoints(rowLength);
        MakeDampers();
        MakeTriagnles();
    }

    void MakePoints(int a)
    {
        float x = 0f;
        float y = 0f;
        for (int i = 0; i < a; i++)
        {
            for (int j = 0; j < a; j++)
            {
                GameObject temp = Instantiate(prefab, new Vector3(0, y, x), new Quaternion()) as GameObject;
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
                dp.speed = speed;
                dp.name = i.ToString();
                dampers.Add(dp);
            }

            if (i < ((rowLength * rowLength) - rowLength))
            {
                GameObject temp = Instantiate(Damp, new Vector3(0, 0, 0), new Quaternion()) as GameObject;
                Damper dp = temp.GetComponent<Damper>();
                dp.P1 = points[i];
                dp.P2 = points[i + rowLength];
                dp.speed = speed;
                dp.name = i.ToString();
                dampers.Add(dp);
            }

            if (i % rowLength != (rowLength - 1) && i < (rowLength * rowLength) - rowLength)
            {
                GameObject temp = Instantiate(Damp, new Vector3(0, 0, 0), new Quaternion()) as GameObject;
                Damper dp = temp.GetComponent<Damper>();
                dp.P1 = points[i];
                dp.P2 = points[i + (rowLength + 1)];
                dp.speed = speed;
                dp.name = i.ToString();
                dampers.Add(dp);
            }

            if (i % rowLength != 0 && i < (rowLength * rowLength) - rowLength)
            {
                GameObject temp = Instantiate(Damp, new Vector3(0, 0, 0), new Quaternion()) as GameObject;
                Damper dp = temp.GetComponent<Damper>();
                dp.P1 = points[i];
                dp.P2 = points[i + (rowLength - 1)];
                dp.speed = speed;
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
        for (int i = 0; i < points.Count; i++)
        {
            if (i % rowLength != 0 && i > (rowLength - 1))
            {
                GameObject temp = Instantiate(Trian, new Vector3(0, 0, 0), new Quaternion()) as GameObject;
                Triangle dp = temp.GetComponent<Triangle>();
                dp.TP1 = points[i];
                dp.TP2 = points[i - rowLength];
                dp.TP3 = points[i - 1];
                tris.Add(dp);
            }

            if ((i + 1) % rowLength != 0 && i < ((rowLength * rowLength) - rowLength))
            {
                GameObject temp = Instantiate(Trian, new Vector3(0, 0, 0), new Quaternion()) as GameObject;
                Triangle dp = temp.GetComponent<Triangle>();
                dp.TP1 = points[i];
                dp.TP2 = points[i + rowLength];
                dp.TP3 = points[i + 1];
                tris.Add(dp);
            }

            if ((i + 1) % rowLength != 0 && i < ((rowLength * rowLength) - rowLength))
            {
                GameObject temp = Instantiate(Trian, new Vector3(0, 0, 0), new Quaternion()) as GameObject;
                Triangle dp = temp.GetComponent<Triangle>();
                dp.TP1 = points[i];
                dp.TP2 = points[i + rowLength + 1];
                dp.TP3 = points[i + 1];
                tris.Add(dp);
            }

            if ((i + 1) % rowLength != 0 && i < ((rowLength * rowLength) - rowLength))
            {
                GameObject temp = Instantiate(Trian, new Vector3(0, 0, 0), new Quaternion()) as GameObject;
                Triangle dp = temp.GetComponent<Triangle>();
                dp.TP1 = points[i];
                dp.TP2 = points[i + rowLength];
                dp.TP3 = points[i + rowLength + 1];
                tris.Add(dp);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Point p in points)
        {
            if (p.ap)
            {
                ApplyGravity(p);
            }
        }
        ApplyForces();
        ApplyAerodynamics();
    }

    void ApplyGravity(Point p)
    {
        Vector3 gravityDirection = new Vector3(0, -1, 0);
        Vector3 gravityVector = gravityDirection * gravityStrength;
        p.f = gravityVector;
    }

    void ApplyForces()
    {
        foreach (Damper d in dampers)
        {
            d.ComputeForce();
            Debug.DrawLine(d.P1.transform.position, d.P2.transform.position);
        }
    }

    void ApplyAerodynamics()
    {
        foreach (Triangle t in tris)
        {
            t.ComputeAerodynamicForce(air);
        }
    }
}
