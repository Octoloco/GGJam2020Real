using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Droid : MonoBehaviour
{
    public bool isBroken;
    public bool fixing;
    public bool hacking;
    public float hackPrcentage;
    public float hackSpeed;
    public float fixedPercentage;
    public int fixingSpeed;
    public bool hacked;

    public float energy;
    public float depleteSpeed;
    public float rechargeSpeed;

    public bool home = false;

    public List<GameObject> nodes = new List<GameObject>();

    Vector3 temp;
    public float speed;

    public GameObject nextNode;

    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        isBroken = true;
        fixing = false;
        fixedPercentage = 0;
        hacking = false;
        hacked = false;

        nodes.Add(GameObject.Find("Ship"));
        nodes.Add(nextNode);
        energy = 99;
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fixing)
        {
            fixedPercentage = fixedPercentage +(1 * fixingSpeed * Time.deltaTime);
            
        }
        else if (hacking)
        {
            hackPrcentage = hackPrcentage + (1 * hackSpeed * Time.deltaTime);
            nextNode = null;
        }

        if (fixedPercentage >= 100)
        {
            fixing = false;
            isBroken = false;
            
        }

        if (hackPrcentage >= 100)
        {
            hackPrcentage = 99;
            hacking = false;
            StartCoroutine(KillAnim());
            
        }

        

        if (nextNode != null)
        {
            temp = nextNode.transform.position - transform.position;
            transform.rotation = Quaternion.LookRotation(-temp, Vector3.up);
            transform.Translate(Vector3.back * speed * Time.deltaTime);
        }

        if (energy <= 0)
        {
            nodes[0].GetComponent<NaveScript>().startSpawn = true;
            nodes[0].GetComponent<NaveScript>().Flip();
            for (int i = 1; i < nodes.Count - 1; i++)
            {
                nodes[i].GetComponent<Node>().Flip();
            }
            nodes[nodes.Count - 1].GetComponent<GeneratorScript>().Flip();
            nextNode = nodes[nodes.Count - 2];
            GameObject temp1;
            GameObject temp2;
            temp1 = nodes[nodes.Count - 1];
            temp2 = nodes[nodes.Count - 2];
            
            nodes.Clear();
            nodes.Add(temp1);
            nodes.Add(temp2);
            
            energy = 1f;
            
        }

        if (energy >= 100)
        {
            nodes[0].GetComponent<GeneratorScript>().startSpawn = true;
            for (int i = 1; i < nodes.Count - 1; i++)
            {
                nodes[i].GetComponent<Node>().Flip();
            }
            nodes[0].GetComponent<GeneratorScript>().Flip();
            nodes[nodes.Count-1].GetComponent<NaveScript>().Flip();
            nextNode = nodes[nodes.Count - 2];
            GameObject temp1;
            GameObject temp2;
            temp1 = nodes[nodes.Count - 1];
            temp2 = nodes[nodes.Count - 2];
            nodes.Clear();
            nodes.Add(temp1);
            nodes.Add(temp2);
           
            energy = 99f;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "node")
        {
            nextNode = other.GetComponent<Node>().nextNode;
            nodes.Add(nextNode);
        }
        else if (other.tag == "generator" || other.tag == "nave")
        {
            nextNode = null;
            if (other.tag == "nave")
            {
                if (nodes[0].GetComponent<GeneratorScript>().repaired)
                {
                    for (int i = 1; i < nodes.Count - 1; i++)
                    {
                        Destroy(nodes[i]);
                    }
                    home = true;
                    GetComponent<SphereCollider>().enabled = false;
                    GetComponent<BoxCollider>().enabled = false;
                    
                    
                }
            }

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "generator")
        {
            energy -= 1 * depleteSpeed * Time.deltaTime;
            other.GetComponent<GeneratorScript>().repairing = true;
            if (other.GetComponent<GeneratorScript>().repaired)
            {
                nodes[0].GetComponent<NaveScript>().startSpawn = true;
                nodes[0].GetComponent<NaveScript>().Flip();
                for (int i = 1; i < nodes.Count - 1; i++)
                {
                    nodes[i].GetComponent<Node>().Flip();
                }
                nodes[nodes.Count - 1].GetComponent<GeneratorScript>().Flip();
                nextNode = nodes[nodes.Count - 2];
                GameObject temp1;
                GameObject temp2;
                temp1 = nodes[nodes.Count - 1];
                temp2 = nodes[nodes.Count - 2];

                nodes.Clear();
                nodes.Add(temp1);
                nodes.Add(temp2);

                
            }
        }
        else if (other.tag == "nave")
        {
            energy += 1 * rechargeSpeed * Time.deltaTime;
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "generator")
        {
            other.GetComponent<GeneratorScript>().repairing = false;
        }
    }

    IEnumerator KillAnim()
    {
        anim.SetBool("kill", true);
        yield return new WaitForSeconds(2);
        hacked = true;
    }
}
