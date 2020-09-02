using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.IO;
using System;

public class FileControllor:MonoBehaviour 
{
    public static float depth;
    public static float maxgeo = 0;
    public static float mingeo = 100000000000000000;
    public static float maxpro = 0;
    public static float minpro = 100000000000000000;
    public static List<GameObject> allcubes = new List<GameObject>();
    //public static bool CubeRun = false;
    string path;
    DirectoryInfo directory;
    public static FileInfo info;

    public void OpenExplorer()
    {
        path = EditorUtility.OpenFilePanel("","","csv");
        string[] directories = path.Split(Path.AltDirectorySeparatorChar);
        string filename = directories[directories.Length - 1];
        int len = filename.Length;
        string pathfile = path.Substring(0, path.Length - len - 1);
        DirectoryInfo directory = new DirectoryInfo(pathfile);
        FileInfo[] infos = directory.GetFiles(filename);
        info = infos[0];
        MinMax ();
        depth = maxpro - minpro;
    }

    private void MinMax()
    {
        using (StreamReader sr = info.OpenText())
        {
            string s = "";
            int index = 0;
            while ((s = sr.ReadLine()) != null)
            {
                string[] points = s.Split(new char[] { ',' });
                float rep = float.Parse(points[6]);
                if (rep == 1)
                {
                    float prox = float.Parse(points[4]);
                    if (prox >= maxpro)
                    {
                        maxpro = prox;
                    }
                    if (prox <= minpro)
                    {
                        minpro = prox;
                    }
                    float geox = float.Parse(points[0]);
                    if (geox >= maxgeo)
                    {
                        maxgeo = geox;
                    }
                    if (geox <= mingeo)
                    {
                        mingeo = geox;
                    }
                }
                index += 1;

            }

        }

    }
}
