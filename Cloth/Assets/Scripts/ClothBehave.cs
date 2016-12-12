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
    public float gravityStrength = 9.8f;
    public float windStr;
    public float SpringConstant;
    public float DampingFactor;
    private Vector3 air;
    [SerializeField]
    private float speed;
    public List<Point> points;
    public List<Damper> dampers;
    public List<Triangle> tris;

    void Start()
    {
        air = new Vector3(0, 1, 1);
        points = new List<Point>();
        dampers = new List<Damper>();
        tris = new List<Triangle>();
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
                Point p = new Point();
                p.r = new Vector3(x, y, 0);
                p.v = new Vector3(0, 0, 0);
                p.m = mass;
                points.Add(p);
                temp.GetComponent<Monopoint>().p = p;
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
                Damper dp = new Damper();
                dp.P1 = points[i];
                dp.P2 = points[i + 1];
                dp.speed = speed;
                temp.GetComponent<Monodamper>().d = dp;
                temp.name = i.ToString();
                dampers.Add(dp);
            }

            if (i < ((rowLength * rowLength) - rowLength))
            {
                GameObject temp = Instantiate(Damp, new Vector3(0, 0, 0), new Quaternion()) as GameObject;
                Damper dp = new Damper();
                dp.P1 = points[i];
                dp.P2 = points[i + rowLength];
                dp.speed = speed;
                temp.GetComponent<Monodamper>().d = dp;
                temp.name = i.ToString();
                dampers.Add(dp);
            }

            if (i % rowLength != (rowLength - 1) && i < (rowLength * rowLength) - rowLength)
            {
                GameObject temp = Instantiate(Damp, new Vector3(0, 0, 0), new Quaternion()) as GameObject;
                Damper dp = new Damper();
                dp.P1 = points[i];
                dp.P2 = points[i + (rowLength + 1)];
                dp.speed = speed;
                temp.GetComponent<Monodamper>().d = dp;
                temp.name = i.ToString();
                dampers.Add(dp);
            }

            if (i % rowLength != 0 && i < (rowLength * rowLength) - rowLength)
            {
                GameObject temp = Instantiate(Damp, new Vector3(0, 0, 0), new Quaternion()) as GameObject;
                Damper dp = new Damper();
                dp.P1 = points[i];
                dp.P2 = points[i + (rowLength - 1)];
                dp.speed = speed;
                temp.GetComponent<Monodamper>().d = dp;
                temp.name = i.ToString();
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
                Triangle dp = new Triangle();
                dp.TP1 = points[i];
                dp.TP2 = points[i - rowLength];
                dp.TP3 = points[i - 1];
                tris.Add(dp);
                temp.GetComponent<Monotriangle>().t = dp;
            }

            if ((i + 1) % rowLength != 0 && i < ((rowLength * rowLength) - rowLength))
            {
                GameObject temp = Instantiate(Trian, new Vector3(0, 0, 0), new Quaternion()) as GameObject;
                Triangle dp = new Triangle();
                dp.TP1 = points[i];
                dp.TP2 = points[i + rowLength];
                dp.TP3 = points[i + 1];
                tris.Add(dp);
                temp.GetComponent<Monotriangle>().t = dp;
            }

            if ((i + 1) % rowLength != 0 && i < ((rowLength * rowLength) - rowLength))
            {
                GameObject temp = Instantiate(Trian, new Vector3(0, 0, 0), new Quaternion()) as GameObject;
                Triangle dp = new Triangle();
                dp.TP1 = points[i];
                dp.TP2 = points[i + rowLength + 1];
                dp.TP3 = points[i + 1];
                tris.Add(dp);
                temp.GetComponent<Monotriangle>().t = dp;
            }

            if ((i + 1) % rowLength != 0 && i < ((rowLength * rowLength) - rowLength))
            {
                GameObject temp = Instantiate(Trian, new Vector3(0, 0, 0), new Quaternion()) as GameObject;
                Triangle dp = new Triangle();
                dp.TP1 = points[i];
                dp.TP2 = points[i + rowLength];
                dp.TP3 = points[i + rowLength + 1];
                tris.Add(dp);
                temp.GetComponent<Monotriangle>().t = dp;
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
            d.Ks = SpringConstant;
            d.kd = DampingFactor;
            d.ComputeForce();
            if (d.L > 50)
            {
                dampers.Remove(d);
                foreach (Monodamper md in FindObjectsOfType<Monodamper>())
                {
                    if (md.d == d)
                    {
                        md.GetComponent<LineRenderer>().SetWidth(0, 0);
                        if (md.d.P1.CLicked)
                        {
                            points.Remove(md.d.P1);
                        }
                        if (md.d.P2.CLicked)
                        {
                            points.Remove(md.d.P2);
                        }
                        Destroy(md.gameObject);
                    }
                }
            }
        }

        foreach (Monodamper md in FindObjectsOfType<Monodamper>())
        {
            md.GetComponent<LineRenderer>().SetPositions(new Vector3[] { md.d.P1.r, md.d.P2.r });
        }
    }

    void ApplyAerodynamics()
    {
        foreach (Triangle t in tris)
        {
            if (t.TP1 == null || t.TP2 == null || t.TP3 == null)
            {
                foreach (Monotriangle md in FindObjectsOfType<Monotriangle>())
                {
                    if (md.t == t)
                    {
                        tris.Remove(t);
                        Destroy(md.gameObject);
                    }
                }
            }
            t.ComputeAerodynamicForce(air * windStr);
        }
    }

    public void SetGrav(float a)
    {
        gravityStrength = a;
    }

    public void SetWind(float a)
    {
        windStr = a;
    }

    public void SetSC(float a)
    {
        SpringConstant = a;
    }

    public void SetDF(float a)
    {
        DampingFactor = a;
    }
}
