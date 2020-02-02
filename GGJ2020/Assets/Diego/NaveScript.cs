using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaveScript : MonoBehaviour
{
    Vector3 temp;
    public GameObject nextNode;
    public bool startSpawn;
    public GameObject arrow;
    
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


        if (startSpawn)
        {
            Debug.Log("enterSpawn");
            startSpawn = false;
            StopAllCoroutines();
        }
        else
        {
            
            startSpawn = true;
            StopAllCoroutines();
        }
    }

    IEnumerator SpawnArrow()
    {

        yield return new WaitForSeconds(.5f);
        if (nextNode != null)
        {
            temp = nextNode.transform.position - transform.position;
            GameObject arrowTemp = Instantiate(arrow, transform.position + (temp.normalized * 4), Quaternion.identity);
            arrowTemp.GetComponent<arrow>().changeObjective(nextNode);
            startSpawn = true;
        }

    }
}
