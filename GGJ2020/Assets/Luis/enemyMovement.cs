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
    public enum Path
    {
        CIRCLE,
        SINE,
        BNFORTH,
        CHASE,
        DMG
    }
    public Path enemyPath;
    public Path defPath;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (droids.Count != 0)
        {
            enemyPath = Path.CHASE;
        }
        else
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
                if (avaliable) {
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

                    }
                }

                move = (target.transform.position - transform.position).normalized * speed * Time.deltaTime;
                transform.Translate(move);

                break;
            case Path.DMG:
                break;
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "droid")
        {
            droids.Add(other.gameObject);
        }

        if (other.gameObject.tag == "droid")
        {

            if ((other.gameObject.transform.position - transform.position).magnitude < 1.6f)
            {
                if (other.gameObject.GetComponent<Droid>().isBroken)
                {
                    enemyPath = Path.DMG;
                    other.gameObject.GetComponent<Droid>().hacking = true;

                }
            }
            if (other.gameObject.GetComponent<Droid>().hacked)
            {
                Vector3 temp = other.gameObject.transform.position;
                Quaternion temp2 = other.gameObject.transform.rotation;
                Destroy(other.gameObject);
                Instantiate(enemy, temp, temp2);
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