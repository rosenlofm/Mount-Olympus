using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;
using UnityEngine.SceneManagement;

public class Pointer_Language : MonoBehaviour
{
    public float m_DefaultLength = 5.0f;
    public GameObject m_Dot;
    //public VRInputModule m_InputModule;
    public Transform trans;
    public SteamVR_ActionSet actionSet;
    public SteamVR_Action_Boolean triggerButton;

    private LineRenderer m_LineRenderer = null;

    public GameObject[] olympusCubes;
    public Material highlight;
    public Material normal;

    private void Awake()
    {
        m_LineRenderer = GetComponent<LineRenderer>();
        actionSet.Activate(SteamVR_Input_Sources.Any, 0, true);

    }

    private void Start()
    {
        //actionSet.Activate(SteamVR_Input_Sources.Any, 0, true);
    }


    // Update is called once per frame
    private void Update()
    {
        UpdateLine();

        if (triggerButton.stateDown)
        {
            CheckUserSelection();
        }
    }

    private void UpdateLine()
    {
        // use default length or distance from input module
        //PointerEventData data = m_InputModule.GetData();
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

            // handle the hover-highlight state for the scene launchers
            CheckHover(hit);

        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                olympusCubes[i].GetComponent<Renderer>().material = normal;
            }
        }

        // set position of th dot
        m_Dot.transform.position = endPosition;

        // set linerenderer
        m_LineRenderer.SetPosition(0, trans.position);
        m_LineRenderer.SetPosition(1, endPosition);
    }

    private void CheckUserSelection()
    {
        RaycastHit hit = CreateRaycast(m_DefaultLength);

        if (hit.collider != null)
        {
            if (hit.collider.tag == "Olympus")
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    private void CheckHover(RaycastHit hit)
    {
        if (hit.collider.gameObject.tag == "Olympus")
        {
            for (int i = 0; i < 3; i++)
            {
                olympusCubes[i].GetComponent<Renderer>().material = highlight;
            }
        }

        else
        {
            for (int i = 0; i < 3; i++)
            {
                olympusCubes[i].GetComponent<Renderer>().material = normal;
            }
        }
    }

    private RaycastHit CreateRaycast(float length)
    {
        RaycastHit hit;
        Ray ray = new Ray(trans.position, trans.forward);

        Physics.Raycast(ray, out hit, m_DefaultLength);

        return hit;
    }
}
