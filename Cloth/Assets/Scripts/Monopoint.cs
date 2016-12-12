using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Monopoint : MonoBehaviour, IDragHandler, IPointerClickHandler, IPointerUpHandler {

    public Point p;
    private float speed;

	// Use this for initialization
	void Start ()
    {
        p.Start();
        speed = 2;
        p.CLicked = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        p.Update();
	}

    void LateUpdate()
    {
        transform.position = p.r;
    }

    public void OnDrag(PointerEventData eventData)
    {
        p.r += new Vector3(eventData.delta.x, eventData.delta.y, p.r.z) * Time.deltaTime * speed;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        p.CLicked = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        p.CLicked = false;
    }
}
