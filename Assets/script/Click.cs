using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour
{
    // Start is called before the first frame update
    public void ClickButton()
    {
        GameObject button = GameObject.FindGameObjectWithTag("button");
        button.SetActive(false);
        GameObject terrain1 = GameObject.FindGameObjectWithTag("Terrain2048");
        terrain1.GetComponent<Terrain>().enabled = false;
        GameObject terrain2 = GameObject.FindGameObjectWithTag("Terrain256");
        terrain2.GetComponent<Terrain>().enabled = false;
        GameObject terrain3 = GameObject.FindGameObjectWithTag("Terrain512");
        terrain3.GetComponent<Terrain>().enabled = false;

    }
}
