using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorScript : MonoBehaviour
{
    Vector3 temp;
    public GameObject nextNode;
    public bool startSpawn;
    public GameObject arrow;

    public bool repairing;
    public float repairedPercentage;
    public float repairSpeed;
    public bool repaired;
    // Start is called before the first frame update
    void Start()
    {
        repaired = false;
        repairedPercentage = 0;
        repairing = false;
        startSpawn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (startSpawn)
        {
            StartCoroutine(SpawnArrow());
            startSpawn = false;
        }

        if (repairing)
        {
            repairedPercentage += 1 * repairSpeed * Time.deltaTime;
        }

        if (repairedPercentage >= 100)
        {
            repaired = true;
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
