using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCubeLauncher : MonoBehaviour
{

    private Transform cube;
    private float speed = 10;
    // Start is called before the first frame update
    void Start()
    {
        cube = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        cube.Rotate(Vector3.up * speed * Time.deltaTime);
    }
}
