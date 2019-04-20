using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Pointer : MonoBehaviour
{
    public float m_DefaultLength = 5.0f;
    public GameObject m_Dot;
    public VRInputModule m_InputModule;
    public Transform trans;

    private LineRenderer m_LineRenderer = null;

    private void Awake()
    {
        m_LineRenderer = GetComponent<LineRenderer>();
    }


    // Update is called once per frame
    private void Update()
    {
        UpdateLine();
    }

    private void UpdateLine()
    {
        // use default length or distance from input module
        PointerEventData data = m_InputModule.GetData();
        float targetLength = m_DefaultLength;

        // replace target length 
        // if not hitting anything, use default lenght, else use the distance to object hit
        //float targetLength = data.pointerCurrentRaycast.distance == 0 ? m_DefaultLength : data.pointerCurrentRaycast.distance;

        // Raycast
        RaycastHit hit = CreateRaycast(targetLength);

        // Default (if raycast doesn't hit anything)
        Vector3 endPosition = trans.position + (trans.forward * m_DefaultLength);

        // or based on hit (on collider)
        if (hit.collider != null)
        {
            endPosition = hit.point;
        }

        // set position of th dot
        m_Dot.transform.position = endPosition;

        // set linerenderer
        m_LineRenderer.SetPosition(0, trans.position);
        m_LineRenderer.SetPosition(1, endPosition);
    }

    private RaycastHit CreateRaycast(float length)
    {
        RaycastHit hit;
        Ray ray = new Ray(trans.position, trans.forward);

        Physics.Raycast(ray, out hit, m_DefaultLength);

        return hit;
    }
}
