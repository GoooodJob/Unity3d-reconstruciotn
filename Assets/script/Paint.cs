using UnityEngine;


public class Paint : MonoBehaviour
{
    // Start is called before the first frame update
    [System.Serializable]
    public class SplatHeights
    {
        public int textureIndex;
        public int stratingHeight;
    }

    public SplatHeights[] splatHeights;

    void Start()
    {
        Terrain terrain = GetComponent<Terrain>();
        TerrainData terrainData = terrain.terrainData;
        float[,,] splatmapData = new float[terrainData.alphamapWidth, terrainData.alphamapHeight, terrainData.alphamapLayers];
        for (int y = 0; y < terrainData.alphamapLayers; y++)
        {
            for (int x = 0; x < terrainData.alphamapWidth; x++)
            {
                float terrainHeight = terrainData.GetHeight(x, y);
                float[] splat = new float[splatHeights.Length];
                for (int i = 0; i < splatHeights.Length; i++)
                {
                    if (terrainHeight >= splatHeights[i].stratingHeight)
                        splat[i] = 1;
                }
                for (int j = 0; j < splatHeights.Length; j++)
                {
                    splatmapData[x, y, j] = splat[j];
                }
            }

            terrainData.SetAlphamaps(0, 0,splatmapData);
        }

    }


}
