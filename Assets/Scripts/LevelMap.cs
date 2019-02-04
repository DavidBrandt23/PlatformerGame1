using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMap : MonoBehaviour {
    private List<List<GameObject>> map;
    // Don't use Tile class outside of LevelMap
    private const string tileTag = "Tile";
    private const string LevelMapObjectTag = "LevelMapObjectTag";
    private const int maxSize = 2000;
    public static LevelMap GetLevelMapObject()
    {
        return GameObject.Find("LevelMapObjectTag").GetComponent<LevelMap>();
    }

    void Start () {
        initializeMap();
    }
    private void initializeMap()
    {
        map = new List<List<GameObject>>(maxSize);

        for(int i = 0; i < maxSize; i++)
        {
            map.Add(new List<GameObject>());
            for (int j = 0; j < maxSize; j++)
            {
                map[i].Add(null);
            }
        }
        GameObject[] TileObjects = GameObject.FindGameObjectsWithTag(tileTag);

        foreach (GameObject obj in TileObjects)
        {
            float objX = obj.transform.position.x;
            float objY = obj.transform.position.y;
            
            int tileX = (int)objX;
            int tileY = (int)objY;
            if((float)tileX != objX || (float)tileY != objY)
            {
                Debug.LogError("TileObject invalid position: " + obj.name + " " + objX + "," + objY);
            }
            
            map[tileX][tileY] = obj;
        }
    }
    public bool TileIsSolid(int? x, int? y)
    {
        GameObject tile = getTile(x, y);
        if(tile == null)
        {
            return false;
        }
        Tile tileScript = tile.GetComponent<Tile>();
        if (!tileScript.IsSolid)
        {
            return false;
        }
        return true;
    }

    public bool TileIsBelowSlope(int? x, int? y)
    {
        GameObject tile = getTile(x, y);
        if (tile == null)
        {
            return false;
        }
        GameObject aboveTile = getTile(x, y+1);
        if (aboveTile == null)
        {
            return false;
        }
        Tile aboveTileScript = aboveTile.GetComponent<Tile>();
        return aboveTileScript.LeftY != aboveTileScript.RightY;
    }

    public float getTileTop(int? x, int? y, float worldX)
    {
        ///get worldY of top of tile at certain x
        float tileSize = 1.0f;
        GameObject tile = getTile(x, y);
        if (tile == null)
        {
            return 0;
        }
        Tile colTileY = tile.GetComponent<Tile>();
        float halfTileHeight = (tileSize / 2);
        float tileLeftX = (float)x - halfTileHeight;

        float t = (worldX - tileLeftX) / tileSize;
        float floorY = (1 - t) * TileLeftY(x,y) + t * TileRightY(x, y); //relative from bot of tile
        floorY = floorY / 32.0f;
        floorY = Mathf.Clamp(floorY, 0.0f, 1.0f);
 
        float slopeHeightAtPlayerMid = ((float)y - halfTileHeight + floorY);
        return slopeHeightAtPlayerMid;
    }

    public int TileSlopeDir(int? x, int? y)
    {

        GameObject tile = getTile(x, y);
        if (tile == null)
        {
            return 0;
        }
        Tile tileScript = tile.GetComponent<Tile>();
        int tileLeftY = tileScript.LeftY;
        int tileRightY = tileScript.RightY;
        if (tileLeftY > tileRightY)
        {
            return -1;
        }
        if (tileLeftY<tileRightY)
        {
            return 1;
        }
        return 0;
    }

    public int TileLeftY(int? x, int? y)
    {
        GameObject tile = getTile(x, y);
        if (tile == null)
        {
            return 0;
        }
        Tile tileScript = tile.GetComponent<Tile>();
        return tileScript.LeftY;
    }

    public int TileRightY(int? x, int? y)
    {
        GameObject tile = getTile(x, y);
        if (tile == null)
        {
            return 0;
        }
        Tile tileScript = tile.GetComponent<Tile>();
        return tileScript.RightY;
    }


    private GameObject getTile(int? x, int? y)
    {

        if(x==null || y == null || x < 0 || y < 0|| x >= maxSize || y >= maxSize)
        {
            return null;
        }
        GameObject tile = map[(int)x][(int)y];
        return tile;
    }

    private void OnGUI()
    {
        //MyGlobal.DrawText(new Vector3(1,1,0), "dsfsd");
    }
    // Update is called once per frame
    void Update () {
		
	}
}
