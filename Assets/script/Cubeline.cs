using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Cubeline : MonoBehaviour
{
    //public GameObject cube;
    DirectoryInfo directory;
    FileInfo[] info;

    // Start is called before the first frame update
    void Start()
    {
        directory = new DirectoryInfo("Assets/Resources");
        info = directory.GetFiles("data.txt");
        FileInfo data = info[0];
      
        using (StreamReader sr = data.OpenText())
        {
            var s = "";
            int index = 0;
            while ((s = sr.ReadLine()) != null)
            {
                int pos = Int32.Parse(s);
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                cube.transform.position = new Vector3(index, pos, 0);
                index += 1;
                //Debug.Log(Int32.Parse(s));
            }
        }

    }



}