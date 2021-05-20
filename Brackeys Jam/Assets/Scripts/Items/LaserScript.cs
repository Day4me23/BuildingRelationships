using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{

    private LineRenderer lineRenderer;
    public Transform LaserHit;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        //lineRenderer.enabled = true;
        lineRenderer.enabled = false;
        lineRenderer.useWorldSpace = true;
        
    }

    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up);
        Debug.DrawLine(transform.position, hit.point);
        LaserHit.position = hit.point;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, LaserHit.position);

        if (Input.GetKey(KeyCode.Mouse0))
        {
            lineRenderer.enabled = true;
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }
}
