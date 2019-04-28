using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;
using UnityEngine.SceneManagement;


public class Pointer : MonoBehaviour
{
    public float m_DefaultLength = 5.0f;
    public GameObject m_Dot;
    public VRInputModule m_InputModule;
    public Transform trans;
    public SteamVR_ActionSet actionSet;
    public SteamVR_Action_Boolean triggerButton;

    private LineRenderer m_LineRenderer = null;

    public GameObject[] languageCubes;
    public GameObject[] houseCubes;
    public GameObject[] labCubes;
    public GameObject[] morgueCubes;

    public Material highlight;
    public Material normal;


    public Pointer pointer;
    public Pointer_Language pointer_language;


    public GameObject Player;
    public GameObject PlayerPref;
    //public GameObject SpawnLocation;

    private void Awake()
    {
        m_LineRenderer = GetComponent<LineRenderer>();

        if(GameObject.FindGameObjectWithTag("Player") != null)
        {
            // player already exists, so just move it to the spawn location and set the Player gameobject parameter
            Player = GameObject.FindGameObjectWithTag("Player");
            Player.transform.position = new Vector3(0f, 5f, 0f);
        }
        else
        {
            // instantiate the Player
            Player = Instantiate(PlayerPref);
            Player.transform.position = new Vector3(0f, 5f, 0f);
        }
    }

    private void Start()
    {
        actionSet.Activate(SteamVR_Input_Sources.Any, 0, true);
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

            // handle the hover-highlight state for the scene launchers
            CheckHover(hit);

        }
        else
        {
            for(int i = 0; i < 3; i++)
            {
                languageCubes[i].GetComponent<Renderer>().material = normal;
                houseCubes[i].GetComponent<Renderer>().material = normal;
                labCubes[i].GetComponent<Renderer>().material = normal;
                morgueCubes[i].GetComponent<Renderer>().material = normal;
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

        if(hit.collider != null)
        {
            if(hit.collider.tag == "Language")
            {
                pointer.enabled = false;
                pointer_language.enabled = true;

                SceneManager.LoadScene(1);
            }
            else if (hit.collider.tag == "House")
            {
                SceneManager.LoadScene(1);
            }
            else if (hit.collider.tag == "Lab")
            {
                SceneManager.LoadScene(1);
            }
            else if (hit.collider.tag == "Morgue")
            {
                SceneManager.LoadScene(1);
            }
        }

    }

    private void CheckHover(RaycastHit hit)
    {
        if (hit.collider.gameObject.tag == "Language")
        {
            for (int i = 0; i < 3; i++)
            {
                languageCubes[i].GetComponent<Renderer>().material = highlight;
            }
        }
        else if (hit.collider.gameObject.tag == "House")
        {
            for (int i = 0; i < 3; i++)
            {
                houseCubes[i].GetComponent<Renderer>().material = highlight;
            }
        }
        else if(hit.collider.gameObject.tag == "Lab")
        {
            for (int i = 0; i < 3; i++)
            {
                labCubes[i].GetComponent<Renderer>().material = highlight;
            }
        }
        else if (hit.collider.gameObject.tag == "Morgue")
        {
            for (int i = 0; i < 3; i++)
            {
                morgueCubes[i].GetComponent<Renderer>().material = highlight;
            }
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                languageCubes[i].GetComponent<Renderer>().material = normal;
                houseCubes[i].GetComponent<Renderer>().material = normal;
                labCubes[i].GetComponent<Renderer>().material = normal;
                morgueCubes[i].GetComponent<Renderer>().material = normal;
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
