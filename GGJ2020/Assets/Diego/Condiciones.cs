using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Condiciones : MonoBehaviour
{
    public GameObject droid1;
    public GameObject droid2;
    public GameObject droid3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (droid1 != null && droid2 != null && droid3 != null)
        {
            if ((droid1.GetComponent<Droid>().home && droid2.GetComponent<Droid>().home && droid3.GetComponent<Droid>().home)
                || (droid1 == null && droid2.GetComponent<Droid>().home && droid3.GetComponent<Droid>().home)
                || (droid1.GetComponent<Droid>().home && droid2 == null && droid3.GetComponent<Droid>().home)
                || (droid1.GetComponent<Droid>().home && droid2.GetComponent<Droid>().home && droid3 == null)
                || (droid1 == null && droid2 == null && droid3.GetComponent<Droid>().home)
                || (droid1 == null && droid2.GetComponent<Droid>().home && droid3 == null)
                || (droid1.GetComponent<Droid>().home && droid2 == null && droid3 == null))
            {
                SceneManager.LoadScene(4);
            }
        }
        if (droid1 == null && droid2 == null && droid3 == null)
        {
            SceneManager.LoadScene(5);
        }
    }
}
