using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMov : MonoBehaviour
{
    public float speed;
    public float rotSpeed;
    bool isFixing;
    // Start is called before the first frame update
    void Start()
    {
        isFixing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFixing)
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(Vector3.back * speed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(Vector3.down * rotSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(Vector3.up * rotSpeed * Time.deltaTime);
            }
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "droid")
        {
            
            if (other.GetComponent<Droid>().isBroken)
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    isFixing = true;
                    other.GetComponent<Droid>().fixing = true;


                }
                
            }
            else
            {
               isFixing = false;
            }
        }
    }
}
