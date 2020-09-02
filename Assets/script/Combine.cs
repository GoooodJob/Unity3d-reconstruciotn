using UnityEngine;
using System.IO;
using UnityEditor;

[RequireComponent(typeof(Terrain))]
public class Combine : MonoBehaviour
{
    int width = 512;
    float depth0;
    int height = 512;
    int rep_n = 0;
    int rep1 = 0;
    int rep2 = 0;
    int rep3 = 0;
    float maxpro0 = 0;
    float minpro0 = 100000000000000000;
    float maxgeo0 = 0;
    float mingeo0 = 100000000000000000;
    float maxgeo1 = 0;
    float mingeo1 = 100000000000000000;
    float maxgeo2 = 0;
    float mingeo2 = 100000000000000000;
    float maxgeo3 = 0;
    float mingeo3 = 100000000000000000;
    float pro1 = 0;
    string path;
    FileInfo info0;
    FileInfo info1;
    FileInfo info2;
    FileInfo info3;
    DirectoryInfo directory;

    public void Combine512()
    {
        foreach (GameObject obj in FileControllor.allcubes)
        {
            obj.SetActive(false);
        }
        GameObject terrain1 = GameObject.FindGameObjectWithTag("Terrain2048");
        terrain1.GetComponent<Terrain>().enabled = false;
        GameObject terrain2 = GameObject.FindGameObjectWithTag("Terrain256");
        terrain2.GetComponent<Terrain>().enabled = false;
        GameObject terrain3 = GameObject.FindGameObjectWithTag("Terrain512");
        terrain3.GetComponent<Terrain>().enabled = true;
        info1 = OpenFile();
        rep1 = rep_n;
        maxgeo1 = maxgeo0;
        mingeo1 = mingeo0;
        info2 = OpenFile();
        rep2 = rep_n;
        maxgeo2 = maxgeo0;
        mingeo2 = mingeo0;
        info3 = OpenFile();
        rep3 = rep_n;
        maxgeo3 = maxgeo0;
        mingeo3 = mingeo0;
        Terrain terrain = GetComponent<Terrain>();
        depth0 = (maxpro0 - minpro0) / 10;
        int maxgeo1_int = (int)maxgeo1;
        int mingeo1_int = (int)mingeo1;
        int maxgeo2_int = (int)maxgeo2;
        int mingeo2_int = (int)mingeo2;
        int maxgeo3_int = (int)maxgeo3;
        int mingeo3_int = (int)mingeo3;
        terrain.terrainData = GenerateTerrain(terrain.terrainData);

    }

    private FileInfo OpenFile()
    {
        path = EditorUtility.OpenFilePanel("", "", "csv");
        string[] directories = path.Split(Path.AltDirectorySeparatorChar);
        string filename = directories[directories.Length - 1];
        int len = filename.Length;
        string pathfile = path.Substring(0, path.Length - len - 1);
        DirectoryInfo directory = new DirectoryInfo(pathfile);
        FileInfo[] infos = directory.GetFiles(filename);
        info0 = infos[0];
        MinMax();
        return info0;

    }


    private TerrainData GenerateTerrain(TerrainData terrainData)
    {

        terrainData.heightmapResolution = height + 1;
        terrainData.size = new Vector3(width, depth0, height);
        float[,] res = GenerateHeights();
        terrainData.SetHeights(0, 0, res);
        return terrainData;
    }

    private float[,] GenerateHeights()
    {
        float[,] heights = new float[width, height];

        using (StreamReader sr = info1.OpenText())
        {
            var s = "";
            
            int index = 0;
            while ((s = sr.ReadLine()) != null && index < rep1)
            {
                string[] points = s.Split(new char[] { ',' });
                if (index == 0)
                {
                    pro1 = float.Parse(points[4]);
                    pro1 = (pro1 - minpro0) / (maxpro0 - minpro0);
                }
                float rep = float.Parse(points[6]);
                if (rep == 1)
                {
                    float geo = float.Parse(points[0]);
                    float pro = float.Parse(points[4]);
                    float geo_ex = (geo - mingeo1) * 10;
                    int geoint = (int)geo_ex;
                    for (int y = 0; y < height; y++)
                    {
                        if (y <= 73 || y > height - 73)
                        {
                            heights[geoint, y] = pro1;
                        }
                        else
                        {
                            float n_pro = (pro - minpro0) / (maxpro0 - minpro0);
                            heights[geoint, y] = n_pro;
                        }
                    }
                }
                index += 1;
            }

        }
        using (StreamReader sr = info2.OpenText())
        {
            var s = "";
            
            int index = 0;
            while ((s = sr.ReadLine()) != null && index < rep2)
            {
                string[] points = s.Split(new char[] { ',' });
                float rep = float.Parse(points[6]);
                if (rep == 1)
                {
                    float geo = float.Parse(points[0]);
                    float pro = float.Parse(points[4]);
                    float geo_ex = (geo - mingeo2 + maxgeo1 - mingeo1) * 10;
                    int geoint = (int)geo_ex;
                    for (int y = 0; y < height; y++)
                    {
                        if (y <= 73 || y > height - 73)
                        {
                            heights[geoint, y] = pro1;
                        }
                        else
                        {
                            float n_pro = (pro - minpro0) / (maxpro0 - minpro0);
                            heights[geoint, y] = n_pro;
                        }
                    }
                }
                index += 1;
            }

        }
        using (StreamReader sr = info3.OpenText())
        {
            var s = "";
            
            int index = 0;
            while ((s = sr.ReadLine()) != null && index < rep3)
            {
                string[] points = s.Split(new char[] { ',' });
                float rep = float.Parse(points[6]);
                if (rep == 1)
                {
                    float geo = float.Parse(points[0]);
                    float pro = float.Parse(points[4]);
                    float geo_ex = (geo - mingeo3 + maxgeo2 - mingeo2 + maxgeo1 - mingeo1) * 10;
                    int geoint = (int)geo_ex;
                    for (int y = 0; y < height; y++)
                    {
                        if (y <= 73 || y > height - 73)
                        {
                            heights[geoint, y] = pro1;
                        }
                        else
                        {
                            float n_pro = (pro - minpro0) / (maxpro0 - minpro0);
                            heights[geoint, y] = n_pro;
                        }
                    }
                }
                index += 1;
            }

        }
        float geo_f = (maxgeo3 - mingeo3 + maxgeo2 - mingeo2 + maxgeo1 - mingeo1) * 10;
        for ( int x = (int) geo_f; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                heights[x, y] = pro1;
            }
        }
        return heights;
    }

    private void MinMax()
    {
        
        maxgeo0 = 0;
        mingeo0 = 100000000000000000;
        using (StreamReader sr = info0.OpenText())
        {
            rep_n = 0;
            string s = "";
            while ((s = sr.ReadLine()) != null)
            {
                string[] points = s.Split(new char[] { ',' });
                float rep = float.Parse(points[6]);
                if (rep == 1)
                {
                    float prox = float.Parse(points[4]);
                    if (prox >= maxpro0)
                    {
                        maxpro0 = prox;
                    }
                    if (prox <= minpro0)
                    {
                        minpro0 = prox;
                    }
                    float geox = float.Parse(points[0]);
                    if (geox >= maxgeo0)
                    {
                        maxgeo0 = geox;
                    }
                    if (geox <= mingeo0)
                    {
                        mingeo0 = geox;
                    }
                    rep_n += 1;
                }
                

            }

        }
    }

}
