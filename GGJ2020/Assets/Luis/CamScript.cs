using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamScript : MonoBehaviour
{
    private Vector3 localZ;
    public GameObject Cube;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Cube.transform.position);
        transform.SetPositionAndRotation((Cube.transform.position - new Vector3(0.0f, 0.0f, 10.0f)), transform.rotation);
    }
}
