using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    Vector3 temp;
    public GameObject nextNode;
    public GameObject flipNode;
    public GameObject arrow;
    bool startSpawn;
    // Start is called before the first frame update
    void Start()
    {
        startSpawn = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if (startSpawn)
        {
            StartCoroutine(SpawnArrow());
            startSpawn = false;
        }
        
    }

    public void Flip()
    {
        GameObject temp;
        temp = nextNode;
        nextNode = flipNode;
        flipNode = temp;
    }

    IEnumerator SpawnArrow()
    {
        yield return new WaitForSeconds(.5f);
        temp = nextNode.transform.position - transform.position;
        GameObject arrowTemp = Instantiate(arrow, transform.position + (temp.normalized * 3), Quaternion.identity);
        arrowTemp.GetComponent<arrow>().changeObjective(nextNode);
        startSpawn = true;

    }
}
