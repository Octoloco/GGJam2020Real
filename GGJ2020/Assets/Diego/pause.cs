using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class pause : MonoBehaviour
{
    public GameObject Panel;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (Time.timeScale > 0)
            {
                Time.timeScale = 0;
                Panel.SetActive(true);

            }
            else if (Time.timeScale < 1)
            {
                Time.timeScale = 1;
                Panel.SetActive(false);
            }
        }
    }

    public void Continue() {
        Time.timeScale = 1;
        Panel.SetActive(false);
    }
}
