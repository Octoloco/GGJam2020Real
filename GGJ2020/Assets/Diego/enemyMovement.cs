using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{ 
    public float speed = 10.0f;
    public float rotSpeed = 10.0f;
    public float pathLenF = 1.0f;
    public float runTime = 1.0f;
    public GameObject enemy;
    bool avaliable = true;
    private List<GameObject> droids = new List<GameObject>();
    private float count = 15.0f;
    private Vector3 move;
    private GameObject target;
    private bool colObst = false;
    Vector3 temp;
    Animator anim;
    public enum Path:int
    {
        CIRCLE = 0,
        SINE = 1,
        BNFORTH = 2,
        CHASE,
        DMG
    }
    public Path enemyPath;
    public Path defPath;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        int tempRan = (int)Random.Range(0, 2);
        if (tempRan == 0)
        {
            defPath = Path.CIRCLE;
        }
        else if (tempRan == 1)
        {
            defPath = Path.SINE;
        }
        else if (tempRan == 2)
        {
            defPath = Path.BNFORTH;
        }
        enemyPath = defPath;
    }

    // Update is called once per frame
    void Update()
    {

        if (droids.Count != 0 && enemyPath != Path.DMG)
        {
            
            StopAllCoroutines();
            
            enemyPath = Path.CHASE;
        }
        else if (droids.Count == 0 && enemyPath != Path.DMG && enemyPath != Path.CHASE)
        {
            enemyPath = defPath;
        }

        switch (enemyPath)
        {
            case Path.CIRCLE:
                transform.Translate(Vector3.right*speed*Time.deltaTime);
                transform.Rotate(Vector3.up, rotSpeed*Time.deltaTime);
                break;
            case Path.SINE:
                transform.Translate(Vector3.right * speed * Time.deltaTime);
                transform.Rotate(Vector3.up, 3.14f * Mathf.Sin(rotSpeed*Time.time));
                if (avaliable)
                {
                    StartCoroutine(Flip());
                    avaliable = false;
                }


                break;
            case Path.BNFORTH:
                transform.Translate(Vector3.right * speed * Time.deltaTime);
                if (avaliable)
                {
                    StartCoroutine(Flip());
                    avaliable = false;
                }


                break;
            case Path.CHASE:
                
                for (int i = 0; i < droids.Count; i++)
                {
                    
                    float dist = Vector3.Distance(droids[i].transform.position, transform.position);

                    if (dist < count)
                    {
                        
                        target = droids[i];
                        temp = target.transform.position - transform.position;
                    }
                }

                transform.rotation = Quaternion.LookRotation(-temp, Vector3.up);
                move = Vector3.back * speed * Time.deltaTime;
                transform.Translate(move);

                break;
            case Path.DMG:
                anim.SetBool("attack", true);
                break;
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "droid")
        {
            
            droids.Add(other.gameObject);
        }

        

    }

    private void OnTriggerStay(Collider other)
    {
        
        if (other.gameObject.tag == "droid")
        {

            if ((other.gameObject.transform.position - transform.position).magnitude <= 2f)
            {
                
                StopAllCoroutines();
                
                
                    enemyPath = Path.DMG;
                    other.gameObject.GetComponent<Droid>().hacking = true;

                
            }
            if (other.gameObject.GetComponent<Droid>().hacked)
            {
                Debug.Log("exists");
                anim.SetBool("attack", false);
                Vector3 temp = other.gameObject.transform.position;
                Quaternion temp2 = other.gameObject.transform.rotation;
                droids.Remove(other.gameObject);
                Destroy(other.gameObject);
                GameObject enemyTemp = Instantiate(enemy, temp, temp2);
                enemyTemp.GetComponent<enemyMovement>().enemyPath =(Path)(int)Random.Range(0f,2f);
                enemyPath = defPath;
                
            }


        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "droid")
        {
            
            droids.Remove(other.gameObject);
            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "obstacle")
        {
            transform.Rotate(Vector3.up, 90f);
        }
        
    }

    IEnumerator Flip()
    {
        yield return new WaitForSeconds(runTime);
        transform.Rotate(0.0f, 180.0f, 0.0f);
        avaliable = true;
    }   
}