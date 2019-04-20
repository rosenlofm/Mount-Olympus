using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerPointer : MonoBehaviour
{
    private Transform trans;

    public LineRenderer lineRenderer;
    public float lineWidth = 0.0001f;
    public float lineMaxLength = 5.0f;


    // Start is called before the first frame update
    void Start()
    {
        trans = GetComponent<Transform>();

        Vector3[] initLaserPositions = new Vector3[2] { Vector3.zero, Vector3.zero };
        lineRenderer.SetPositions(initLaserPositions);
        //lineRenderer.SetWidth(lineWidth, lineWidth);
        lineRenderer.startWidth = lineWidth;
        lineRenderer.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {

        RaycastHit hit;

        if (Physics.Raycast(trans.position, trans.TransformDirection(Vector3.forward), out hit))
        {
            Debug.DrawRay(trans.position, trans.TransformDirection(Vector3.forward), Color.yellow);
            Debug.Log("Did hit");

            //DrawLineToTarget(trans.position, Vector3.forward, lineMaxLength);
            //lineRenderer.enabled = true;

            lineRenderer.SetPosition(0, trans.position);
            lineRenderer.SetPosition(1, hit.point);

        }
        else
        {
            Debug.DrawRay(trans.position, trans.TransformDirection(Vector3.forward), Color.white);
            Debug.Log("Did NOT hit");

           // DrawLineToTarget(trans.position, Vector3.forward, lineMaxLength);
            //lineRenderer.enabled = true;
        }


    }


    void DrawLineToTarget(Vector3 targetPosition, Vector3 direction, float length)
    {

    }
}
