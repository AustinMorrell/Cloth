using UnityEngine;
using System.Collections;

public class Multiply : MonoBehaviour {

    static int a_Particle = 16;
    static int a_SpringDamper = 36;
    [SerializeField]
    private Point poin;
    [SerializeField]
    private Damper damp;
    private Point[] points = new Point[16];
    private Damper[] dampers = new Damper[36];

    void Start() {
        int c = 0;
        for (int i = 0; i < 4; i++)
        {
            for (int b = 0; b < 4; b++)
            {
                poin.r = new Vector3(i * 3, b * 3, 0);
                points[c] = poin;
                c++;
                if (c > 16)
                {
                    c = 16;
                }
                Debug.Log(c);
            }
        }

        foreach(Point d in points)
        {
            Instantiate(d, d.transform.position, d.transform.rotation);
        }
	}
}
