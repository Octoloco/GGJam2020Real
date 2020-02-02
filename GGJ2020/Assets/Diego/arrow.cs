using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow : MonoBehaviour
{
    public GameObject nextNode;
    Vector3 temp;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (nextNode == null)
        {
            Destroy(gameObject);
        }
        else
        {
            temp = nextNode.transform.position - transform.position;
            transform.rotation = Quaternion.LookRotation(-temp, Vector3.up);
            transform.Translate(Vector3.back * speed * Time.deltaTime);
        }

        
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "node" || other.tag == "generator" || other.tag == "nave")
            Destroy(gameObject);
    }

    public void changeObjective(GameObject node)
    {
        nextNode = node;
    }
}
