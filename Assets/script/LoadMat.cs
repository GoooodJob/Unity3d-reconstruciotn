using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

[RequireComponent(typeof(LineRenderer))]
public class LoadMat : MonoBehaviour
{
    DirectoryInfo directory;
    FileInfo[] info;
    LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Start()
    {
        directory = new DirectoryInfo("Assets/Resources");
        info = directory.GetFiles("data.txt");
        FileInfo data = info[0];
        Debug.Log(data);

        // Open the file to read from.
        using (StreamReader sr = data.OpenText())
        {
            var s = "";
            int index = 0;
            while ((s = sr.ReadLine()) != null)
            {
                int pos = Int32.Parse(s);
                lineRenderer.SetPosition(index, new Vector3(pos, 0, 0));
                index += 1;
                //Debug.Log(Int32.Parse(s));
            }
        }


        //this.lineRenderer.SetPositions(info);
    }

}
