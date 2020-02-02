using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Management : MonoBehaviour
{
    public void playscene(string name) 
    {
        SceneManager.LoadScene(name);
    }
}
