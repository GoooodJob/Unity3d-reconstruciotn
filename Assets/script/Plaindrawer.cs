using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Plaindrawer: MonoBehaviour
{
    public void CubePlain()
    {
        GameObject terrain1 = GameObject.FindGameObjectWithTag("Terrain2048");
        terrain1.GetComponent<Terrain>().enabled = false;
        GameObject terrain2 = GameObject.FindGameObjectWithTag("Terrain256");
        terrain2.GetComponent<Terrain>().enabled = false;
        GameObject terrain3 = GameObject.FindGameObjectWithTag("Terrain512");
        terrain3.GetComponent<Terrain>().enabled = false ;

        foreach (GameObject obj in FileControllor.allcubes)
            {
                obj.SetActive(true);
            }
        using (StreamReader sr = FileControllor.info.OpenText())
        {
            var s = "";
            float pro1 = 0;
            int index = 0;
            while ((s = sr.ReadLine()) != null)
            {
                string[] points = s.Split(new char[] { ',' });
                if (index == 0)
                { 
                    pro1 = float.Parse(points[4]);
                    pro1 = (pro1 - FileControllor.minpro) / (FileControllor.maxpro - FileControllor.minpro);
                }
                float rep = float.Parse(points[6]);
                if (rep == 1)
                {
                    float geo = float.Parse(points[0]);
                    float pro = float.Parse(points[4]);
                    float n_pro = (pro - FileControllor.minpro) / (FileControllor.maxpro - FileControllor.minpro);
                    int a = 0;
                    GameObject cube = null;
                    while (a <= 50)
                    {
                        if (a <= 5)
                        {
                            cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                            cube.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                            cube.transform.position = new Vector3(geo, pro1, 0.3f*a);
                        }
                        else
                            if (a <= 45)
                            {
                                cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                                cube.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                                cube.transform.position = new Vector3(geo, n_pro, 0.3f*a);
                            }
                            else
                                if (a <= 50)
                                {
                                    cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                                    cube.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                                    cube.transform.position = new Vector3(geo, pro1, 0.3f*a);
                                }
                        a += 1;
                        FileControllor.allcubes.Add(cube as GameObject);
                    }
                }
                index += 1;
            }
        }

    }



}