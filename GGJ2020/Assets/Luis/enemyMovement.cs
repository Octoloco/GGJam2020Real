using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{
    public float speed = 10.0f;
    public float sineAmp = 0.1f;
    public float sineFrec = 5.0f;
    public float pathLen = 1.0f;
    private List<GameObject> droids = new List<GameObject>();
    private float count = 15.0f;
    private Vector3 move;
    private GameObject target;
    public enum Path
    {
        CIRCLE,
        SINE,
        BNFORTH,
        CHASE
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
                move = new Vector3(sineFrec * Mathf.Cos(Time.time), 0.0f, Mathf.Sin(sineFrec * Time.time)) * 0.1f;
                break;
            case Path.SINE:
                move = new Vector3(pathLen*Mathf.Sin(Time.time), 0.0f, sineAmp * Mathf.Sin(sineFrec*Time.time))*0.1f;
                break;
            case Path.BNFORTH:
                move = new Vector3(0.0f, 0.0f, pathLen*Mathf.Sin(sineFrec * Time.time))*0.1f;
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
                break;
        }
        transform.Translate(move);
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "droid")
        {
            droids.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "droid")
        {
            droids.Remove(other.gameObject);
            
        }
    }
}
