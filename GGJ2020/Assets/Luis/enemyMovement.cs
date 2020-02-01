using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{
    public float speed = 0.1f;
    public enum Path
    {
        CIRCLE,
        SINE,
        BNFORTH
    }
    public Path enemyPath;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(enemyPath)
        {
            case Path.CIRCLE:
                transform.Translate(new Vector3(Mathf.Cos(Time.time), 0.0f , Mathf.Sin(Time.time))* speed);
                break;
            case Path.SINE:
                transform.Translate(new Vector3(0.1f, 0.0f, 0.1f * Mathf.Sin(Time.time))* speed);
                break;
            case Path.BNFORTH:
                transform.Translate(new Vector3(0.0f, 0.0f, 0.1f*Mathf.Sin(Time.time))* speed);
                break;
                
        }
        
    }
}
