using UnityEngine;
using System.IO;

[RequireComponent(typeof(Terrain))]
public class Test3 : MonoBehaviour
{
    int width = 2048;
    float depth;
    int height = 2048;
    float maxpro = 0;
    float minpro = 100000000000000000;
    float maxgeo = 0;
    float mingeo = 100000000000000000;
    int geo_st = 0;
    //public float Scale = 20f;

    private FileInfo info;

    private void Awake()
    {

        DirectoryInfo directory = new DirectoryInfo("Assets/Resources");
        FileInfo[] infos = directory.GetFiles("crack_1.csv");
        info = infos[0];
    }

    void Start()
    {
        Terrain terrain = GetComponent<Terrain>();
        MinMax();
        //Debug.Log(mingeo);
        depth = maxpro - minpro;
        int maxgeo_int = (int)maxgeo;
        int mingeo_int = (int)mingeo;
        int geo_scale = (maxgeo_int - mingeo_int)*100;
        //Debug.Log(geo_scale);
        float geo_start = (width - geo_scale) / 2;
        geo_st = (int)geo_start;
        //Debug.Log(geo_st);
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
    }

    private TerrainData GenerateTerrain(TerrainData terrainData)
    {

        terrainData.heightmapResolution =  height + 1;
        terrainData.size = new Vector3(width, depth, height);
        float[,] res = GenerateHeights();
        //Debug.Log(res);
        terrainData.SetHeights(0, 0, res);
        return terrainData;
    }

    private float[,] GenerateHeights()
    {
        float[,] heights = new float[width, height];

        using (StreamReader sr = info.OpenText())
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
                    pro1 = (pro1 - minpro) / (maxpro - minpro);
                }
                float rep = float.Parse(points[6]);
                //Debug.Log(rep);
                if (rep == 1)
                {
                    //float xCoord = (float)(index / x) * 256;
                    //Debug.Log(pro1);
                    float geo = float.Parse(points[0]);
                    float pro = float.Parse(points[4]);
                    float geo_ex = (geo - mingeo) * 100;
                    //Debug.Log(geo);
                    int geoint = (int)geo_ex;
                    //Debug.Log(geoint);
                    //float geocoord = geo / width * Scale;
                    //float ycoord = y / height * Scale;
                    //Debug.Log(pro);
                    for (int y = 0; y < height; y++)
                    {
                        if (y <= 475 || y > height - 475)
                        {
                            heights[geoint+geo_st, y] = pro1;
                            //Debug.Log(heights[geoint, y]);
                        }
                        else
                        {
                            float n_pro = (pro - minpro) / (maxpro - minpro);
                            heights[geoint+geo_st, y] = n_pro;
                        }
                        //Debug.Log(heights[index, y]);
                        //Debug.Log(y);
                    }
                }
                index += 1;
                //Debug.Log(index);
            }
        }

        return heights;
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
                    //width += 1;
                    //Debug.Log(width);

                }
                index += 1;
              
            }

        }

        //return new float[] { minpro, maxpro, };

    }

}
