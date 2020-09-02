using UnityEngine;
using System.IO;

[RequireComponent(typeof(Terrain))]
public class Terrain256 : MonoBehaviour
{
    int width = 256;
    int height = 256;
    int geo_st = 0;
    int depth2 = 30;
    int geo_2 = 0;

    public void GenerateSmall()
    {
        foreach (GameObject obj in FileControllor.allcubes)
        {
            obj.SetActive(false);
        }
        GameObject terrain1 = GameObject.FindGameObjectWithTag("Terrain2048");
        terrain1.GetComponent<Terrain>().enabled = false;
        GameObject terrain2 = GameObject.FindGameObjectWithTag("Terrain256");
        terrain2.GetComponent<Terrain>().enabled = true;
        GameObject terrain3 = GameObject.FindGameObjectWithTag("Terrain512");
        terrain3.GetComponent<Terrain>().enabled = false;
        Terrain terrain = GetComponent<Terrain>();
        int maxgeo_int = (int)FileControllor.maxgeo;
        int mingeo_int = (int)FileControllor.mingeo;
        int geo_scale = (maxgeo_int - mingeo_int) * 10;
        float geo_start = (width - geo_scale) / 2;
        geo_st = (int)geo_start;
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
    }


    private TerrainData GenerateTerrain(TerrainData terrainData)
    {

        terrainData.heightmapResolution = height + 1;
        terrainData.size = new Vector3(width, depth2, height);
        float[,] res = GenerateHeights();
        terrainData.SetHeights(0, 0, res);
        return terrainData;
    }

    private float[,] GenerateHeights()
    {
        float[,] heights = new float[width, height];
        float pro1 = 0;
        using (StreamReader sr = FileControllor.info.OpenText())
        {
            var s = "";

            int index = 0;
            float n_pro = 0;
            int geoint = 0;
            float geo_ex = 0;
            float geo = 0;
            while ((s = sr.ReadLine()) != null) //&& index < width)
            {
                string[] points = s.Split(new char[] { ',' });
                if (index == 0)
                {
                    geo = float.Parse(points[0]);
                    geo_ex = (geo - FileControllor.mingeo) * 10;
                    geoint = (int)geo_ex;
                    geo_2 = geoint;
                    pro1 = float.Parse(points[4]);
                    pro1 = (pro1 - FileControllor.minpro) / (FileControllor.maxpro - FileControllor.minpro);
                }
                float rep = float.Parse(points[6]);
                if (rep == 1)
                {
                    geo = float.Parse(points[0]);
                    geo_ex = (geo - FileControllor.mingeo) * 10;
                    geoint = (int)geo_ex;
                    float pro = float.Parse(points[4]);
                    for (int y = 0; y < height; y++)
                    {
                        if (y <= 58 || y > height - 58)
                        {
                            heights[geoint + geo_st, y] = pro1;
                        }
                        else
                        {
                            n_pro = (pro - FileControllor.minpro) / (FileControllor.maxpro - FileControllor.minpro);
                            heights[geoint + geo_st, y] = n_pro;
                        }
                    }
                }
                index += 1;
            }

        }
        for (int x = 0; x <= geo_st; x++)
        {
            for (int y = 0; y < height; y++)
            {
                heights[x, y] = pro1;
            }

        }

        for (int x = geo_2 + geo_st; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                heights[x, y] = pro1;
            }
        }

        return heights;
    }


}
