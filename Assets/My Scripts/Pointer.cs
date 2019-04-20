using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        float targetLength = m_DefaultLength;

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
