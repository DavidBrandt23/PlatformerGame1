using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelMap : MonoBehaviour {

    // Don't use Tile class outside of LevelMap
    private const string tileTag = "Tile";
    private const string tileMapTag = "TileMap1";
    private Tilemap tileMap;
    public static LevelMap GetLevelMapObject()
    {
        return GameObject.Find("LevelMapObjectTag").GetComponent<LevelMap>();
    }

    public void onLoadLevel()
    {
        GameObject tileMapOb = GameObject.Find(tileMapTag);
        if(tileMapOb != null)
        {

            tileMap = tileMapOb.GetComponent<Tilemap>();
        }
        else
        {
            Debug.Log("GameManager could not find TileMap1");
        }
    }

    public bool TileIsSolid(int? x, int? y)
    {
        if (x==null || y== null)
        {
            return false;
        }
        if (tileMap == null) return false;
        if(tileMap.GetTile(new Vector3Int((int)x, (int)y, 0))!=null)
        {
            //Debug.Log(x + " " + y + " is n  null");
            return true;
        }
        return false;
    }

    public bool TileIsBelowSlope(int x, int y)
    {
        return TileSlopeDir(x, y + 1) != 0;
    }

    public float getTileTop(int x, int y, float worldX)
    {
        ///get worldY of top of tile at certain x
        float tileSize = 1.0f;
        LevelTile tile = null;
        if (getTile(x, y, ref tile))
        {
            float halfTileHeight = (tileSize / 2);
            float tileLeftX = (float)x;

            float t = (worldX - tileLeftX) / tileSize;
            float floorY = (1 - t) * TileLeftY(x, y) + t * TileRightY(x, y); //relative from bot of tile
            floorY = Mathf.Clamp(floorY, 0.0f, 1.0f);

            float slopeHeightAtPlayerMid = ((float)y + floorY);
            return slopeHeightAtPlayerMid;
        }
        return 0;
    }

    public int TileSlopeDir(int x, int y)
    {
        LevelTile tile = null;
        if (getTile(x, y, ref tile))
        {
            float tileLeftY = tile.leftY;
            float tileRightY = tile.rightY;
            if (tileLeftY > tileRightY)
            {
                return -1;
            }
            if (tileLeftY < tileRightY)
            {
                return 1;
            }
            return 0;
        }
        return 0;
    }

    public float TileLeftY(int x, int y)
    {
        LevelTile tile = null;
        if (getTile(x, y, ref tile))
        {
            return tile.leftY;
        }
        return 0;
    }

    public float TileRightY(int x, int y)
    {
        LevelTile tile = null;
        if (getTile(x, y, ref tile))
        {
            return tile.rightY;
        }
        return 0;
    }


    private bool getTile(int x, int y, ref LevelTile tile)
    {
        TileBase baseTile = tileMap.GetTile(new Vector3Int((int)x, (int)y, 0));
        if(baseTile == null)
        {
            tile = null;
            return false;
        }
        if(baseTile.GetType() != typeof(LevelTile)){
            tile = null;
            return false;
        }
        tile = (LevelTile)baseTile;
        return tile != null;
    }

    private void OnGUI()
    {
        //MyGlobal.DrawText(new Vector3(1,1,0), "dsfsd");
    }
    // Update is called once per frame
    void Update () {
		
	}
}
