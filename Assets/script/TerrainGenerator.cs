using UnityEngine;
using System.IO;

[RequireComponent(typeof(Terrain))]
public class TerrainGenerator : MonoBehaviour
{
    int width = 1500;
    int depth = 20;
    int height = 200;
    float maxpro = 0;
    float minpro = 100000000000000000;
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
        float[] minmax = MinMax();
        float minpro = minmax[0];
        float maxpro = minmax[1];
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
    }

    private TerrainData GenerateTerrain(TerrainData terrainData)
    {

        terrainData.heightmapResolution = width + 1;
        terrainData.size = new Vector3(width, depth, height);
        float[,] res = GenerateHeights();
        //Debug.Log(res[10, 100]);
        terrainData.SetHeights(0 , 0, res);
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
                    //float geo = float.Parse(points[0]);
                    float pro = float.Parse(points[4]);
                    //Debug.Log(geo);
                    //float geocoord = geo / width * Scale;
                    //float ycoord = y / height * Scale;
                    //Debug.Log(pro);
                    for (int y = 0; y < height; y++)
                    {
                        if (y <= 30 || y > height - 30)
                        {
                            heights[index, y] = pro1;
                        }
                        else
                        {
                            float n_pro = (pro - minpro) / (maxpro - minpro);
                            heights[index,y] = n_pro;
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

    private float[] MinMax()
    {

        
        using (StreamReader sr = info.OpenText())
        {
            string s = "";
            while ((s = sr.ReadLine()) != null)
            {
                string[] points = s.Split(new char[] { ',' });
                float rep;
                float.TryParse(points[6], out rep);
                if (rep == 1)
                {
                    float prox;
                    float.TryParse(points[4], out prox);
                    if (prox >= maxpro)
                    {
                        maxpro = prox;
                    }
                    if (prox <= minpro)
                    {
                        minpro = prox;
                    }
                    //x += 1;
                    //Debug.Log(width);
                }

            }
            
        }

        return new float[] { minpro, maxpro };

    }

}
