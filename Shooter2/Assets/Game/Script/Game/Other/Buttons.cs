using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Buttons : MonoBehaviour
{
    public GameObject[] menu;

    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void SetFalseOnClick(int menuObject)
    {
        menu[menuObject].SetActive(false);
    }

    public void SetActiveOnClick(int menuObject)
    {
        menu[menuObject].SetActive(true);
    }
}
