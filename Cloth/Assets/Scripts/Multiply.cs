using UnityEngine;
using System.Collections.Generic;

public class Multiply : MonoBehaviour {

    static int a_Particle = 16;
    static int a_SpringDamper = 36;
    [SerializeField]
    private Point poin;
    [SerializeField]
    private Damper damp;
    private List<Point> points = new List<Point>();
    private List<Damper> dampers = new List<Damper>();

    void Start() {
        SpawnPoints();
        SpawnDamps();
        SpawnTri();
	}

    void SpawnPoints()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int b = 0; b < 4; b++)
            {
                Vector3 position = new Vector3(i * 3, b * 3, 0);
                Point a = (Point)Instantiate(poin, poin.transform.position, poin.transform.rotation);
                a.r = position;
                points.Add(a);
            }
        }
    }

    void SpawnDamps()
    {
        for (int p = 0; p < a_SpringDamper; p++)
        {
            Damper v = (Damper)Instantiate(damp, damp.transform.position, damp.transform.rotation);
            v.P1 = points[p];
            v.P2 = points[p + 1];
            Vector3 position = Vector3.Lerp(v.P1.r, v.P2.r, 0.5f);
            v.transform.position = position;
            dampers.Add(v);
        }
    }

    void SpawnTri()
    {

    }
}