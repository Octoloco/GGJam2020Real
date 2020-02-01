using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float tanSpeed;
    public float angFrec;
    public float amplitude;
    private Vector3 moveDir;
    private Vector3 localZ;

    // Update is called once per frame
    void Update()
    {
        moveDir = new Vector3(1.0f, 0.0f, 0.0f) * tanSpeed * Time.deltaTime;
        localZ = (transform.position - Vector3.zero).normalized;
    }

    void FixedUpdate()
    {
        GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + transform.TransformDirection(moveDir));
        transform.Rotate(localZ, 5 * Mathf.Sin(5.0f * Time.time));
    }
}
