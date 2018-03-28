using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject menu;

	void Update ()
    {
        Debug.Log(menu.activeSelf);
		if(Input.GetKeyDown(KeyCode.Escape) && !menu.activeSelf)
        {
            menu.SetActive(true);
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && menu.activeSelf)
        {
            menu.SetActive(false);
        }
    }
}

