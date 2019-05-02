using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class MyGlobal : MonoBehaviour
{
    private static string playerObjectName = "PlayerMan";
    private static string mainCameraName = "MainCamera";
    public class RectPoints
    {
        public Vector2 topLeft;
        public Vector2 topRight;
        public Vector2 botLeft;
        public Vector2 botRight;
        public float top;
        public float bottom;
        public float left;
        public float right;
        public float width;
        public float height;
    }

    public static float tileSize = 1.0f;
    private static Vector2? noCol = null;
    public static Vector3 WorldToScreen(Vector3 worldPos)
    {
        Camera mainCam = GameObject.Find(mainCameraName).GetComponent<Camera>();
        return mainCam.WorldToScreenPoint(worldPos);
        
    }

    public static GameObject AddEntityToScene(GameObject prefab, Vector3 position)
    {
        GameObject newBullet = Instantiate(prefab, position, Quaternion.identity);
        newBullet.transform.parent = GameObject.Find("Entities").transform;
        return newBullet;
    }
    public static bool ColliderOnCamera(GameObject obj)
    {
        BoxCollider2D boxCollider = obj.GetComponent<BoxCollider2D>();
        Vector3 position = obj.GetComponent<Transform>().position;
        Vector2 curPosm = new Vector2(position.x, position.y);

        RectPoints RP = BoxColliderPoints(curPosm, boxCollider);
        float worldScreenLeft = 1.0f;
        float worldScreenRight = 1.0f;
        if ((RP.left < worldScreenRight && RP.right > worldScreenLeft) || 
            (RP.left < worldScreenRight && RP.right > worldScreenLeft))
        {
            return true;
        }
        return false;
    }

    public static GameObject GetPlayerObject()
    {
        return GameObject.Find(playerObjectName);
    }
    public static GameObject GetGameControllerObject()
    {
        return GameObject.Find("GameController");
    }
    public static void PlayGlobalSound(AudioClip clip)
    {
        GameObject controller = GameObject.Find("GameController");
        controller.GetComponent<AudioSource>().PlayOneShot(clip);
    }
    public static void DrawText(Vector3 worldPos, string text)
    {
        Vector3 screenPos = WorldToScreen(worldPos);
        GUI.Label(new Rect(screenPos.x, screenPos.y, 33, 33), text);
    }
    public static bool OnGroundObj(GameObject o, ref GameObject touchedPlat)
    {
        Vector3 position = o.GetComponent<Transform>().position;
        Vector2 pos = new Vector2(position.x, position.y);
        BoxCollider2D box = o.GetComponent<BoxCollider2D>();
        return OnGround(pos, box, ref touchedPlat);
    }
    public static bool OnGround(Vector2 curPosm, BoxCollider2D boxCollider)
    {
        GameObject plat = null;
        return OnGround(curPosm, boxCollider, ref plat);
    }

    public static bool facingEdge(Vector3 curPosm, BoxCollider2D boxCollider, int facingDir)
    {
        const float leadAmount = 0.2f;
       // RectPoints boxColliderPoints = BoxColliderPoints(curPosm, boxCollider);
       // float bottomY = boxColliderPoints.bottom;
        //float leadingX = boxColliderPoints.bottom;

        Vector2 checkPos = new Vector2(curPosm.x + facingDir * leadAmount, curPosm.y);
        RectPoints checkPoints = BoxColliderPointsNoComp(checkPos, boxCollider.size.x, boxCollider.size.y, boxCollider.offset.x, boxCollider.offset.y);
        GameObject plat = null;
        return !OnGroundCor(checkPos, checkPoints, ref plat);
    }

    public static bool OnGround(Vector2 curPosm, BoxCollider2D boxCollider, ref GameObject touchedPlat)
    {
        RectPoints boxColliderPoints = BoxColliderPoints(curPosm, boxCollider);
        return OnGroundCor(curPosm, boxColliderPoints, ref touchedPlat);
    }

    private static bool OnGroundCor(Vector2 curPosm, RectPoints boxColliderPoints, ref GameObject touchedPlat)
    {
        int yDir = -1;
        float searchDistance = 0.03f;
        float boxLeadingY = (yDir == -1) ? boxColliderPoints.botLeft.y : boxColliderPoints.topLeft.y;
        float boxDesiredY = boxLeadingY - searchDistance; //pos of leading edge if move full vel was .03
        float boxRightX = boxColliderPoints.botRight.x;
        float boxLeftX = boxColliderPoints.botLeft.x;
        int startY = RoundFloat(boxLeadingY);
        int endY = RoundFloat(boxDesiredY);
        int startX = RoundFloat(boxRightX);
        int endX = RoundFloat(boxLeftX);

        Vector2? colTileY = null;
        int? curX, curY;
        curY = null;
        curX = null;
        LevelMap levelMap = LevelMap.GetLevelMapObject();

        //check level tiles
        while (true)
        {
            NextTileToCheck(ref curY, ref curX, startY, endY, startX, endX);
            if (curY == null)
            {
                break;
            }
            if (levelMap.TileIsSolid(curX, curY))
            {
                int slopeDir = levelMap.TileSlopeDir((int)curX, (int)curY);
                if (RoundFloat(curPosm.x) != curX && slopeDir != 0)
                {
                    //ignore slopes if x is not inside
                    continue;
                }
                colTileY = new Vector2((float)curX, (float)curY);
                float slopeHeightAtPlayerMid = levelMap.getTileTop((int)colTileY.Value.x, (int)colTileY.Value.y, curPosm.x);

                if (boxDesiredY <= slopeHeightAtPlayerMid)
                {
                    return true;
                }
            }
        }

        //check other blocks  

        if (true)
        {
            foreach (GameObject block in GetBlocks())
            {
                BoxCollider2D blockCollider = block.GetComponent<BoxCollider2D>();
                RectPoints blockPoints = BoxColliderPoints(block.transform.position, blockCollider);
                bool aboveBlock = curPosm.y > block.transform.position.y;
                bool belowBlock = curPosm.y < block.transform.position.y;
                bool horizontalWithBlock = (boxColliderPoints.left < blockPoints.right) && (boxColliderPoints.right > blockPoints.left);
                if (horizontalWithBlock)
                {
                    bool willBePastTopSide = boxDesiredY < blockPoints.top;
                    if (willBePastTopSide && aboveBlock)
                    {
                        touchedPlat = block;
                        return true;
                    }
                }
            }
        }

        return false;
    }

    private static List<GameObject> GetBlocks()
    {

        // List<GameObject> children = new List<GameObject>();
        //GameObject blockHolder = GameObject.Find("Blocks");
        //if(blockHolder == null)
        //{
        //    return children; //blank list
        //}
        //Transform[] childTransforms = blockHolder.GetComponentsInChildren<Transform>();
        //foreach (Transform t in blockHolder.transform)
        //{
        //    children.Add(t.gameObject);
        //}
        // return children;
        return GetGameControllerObject().GetComponent<GameController>().GetNonGridBlocks();

    }

    private static float GetNewX(Vector2 curPosm, BoxCollider2D boxCollider, Vector2 velocity, ref bool hitTile)
    {
        LevelMap levelMap = LevelMap.GetLevelMapObject();
        RectPoints boxColliderPoints = BoxColliderPoints(curPosm, boxCollider);
        float halfPlayerWidth = boxColliderPoints.width / 2.0f;
        float realX = curPosm.x;
        float realLeadingEdgeX = 0;
        int xDir = (velocity.x > 0) ? 1 : -1;
        int? curY = null, curX = null;
        List<int> yValsToSkip = new List<int>();
        if (velocity.x == 0)
        {
            xDir = 0;
        }
        else
        {
            float boxLeadingX = (xDir == -1) ? boxColliderPoints.topLeft.x : boxColliderPoints.topRight.x;
            float boxDesiredX = boxLeadingX + velocity.x; //pos of leading edge if move full vel
            float boxTopY = boxColliderPoints.topLeft.y;
            float boxBotY = boxColliderPoints.botLeft.y;
           // if (xDir == -1) boxDesiredX += 0.5f;
            int startX = RoundFloat(curPosm.x);
            int endX = RoundFloat(boxDesiredX);
            int startY = RoundFloat(boxTopY);
            int endY = RoundFloat(boxBotY);
            Vector2? colTile = null;
            while (true)
            {
                NextTileToCheck(ref curX, ref curY, startX, endX, startY, endY);
                if (curX == null)
                {
                    break;
                }
                {
                    if (!yValsToSkip.Contains((int)curY) && levelMap.TileIsSolid(curX, curY))
                    {
                        if (levelMap.TileSlopeDir((int)curX, (int)curY) == 0)
                        {
                            //tile is flat
                            colTile = new Vector2((float)curX, (float)curY);
                            hitTile = true;
                            break;
                        }
                        else
                        {
                            //is slope, ignore this tile and every other tile we find in this row
                            yValsToSkip.Add((int)curY);
                        }
                    }
                }
            }
            float extraSpace = 0.01f;
            if(hitTile)
            {
                if (xDir == 1)
                {
                    realLeadingEdgeX = colTile.Value.x;
                    realLeadingEdgeX -= extraSpace;
                }
                else if(xDir == -1)
                {
                    realLeadingEdgeX = colTile.Value.x + 1.0f;
                    realLeadingEdgeX += extraSpace;
                }
            }

            //check with blocks
            if (!hitTile)
            {
                foreach (GameObject block in GetBlocks())
                {
                    BoxCollider2D blockCollider = block.GetComponent<BoxCollider2D>();
                    RectPoints blockPoints = BoxColliderPoints(block.transform.position, blockCollider);
                    bool rightOfBlock = curPosm.x > block.transform.position.x;
                    bool leftOfBlock = curPosm.x < block.transform.position.x;
                    bool verticalWithBlock = (boxColliderPoints.top > blockPoints.bottom) && (boxColliderPoints.bottom < blockPoints.top);
                    if (verticalWithBlock)
                    {
                        if (xDir == -1 && rightOfBlock)
                        {
                            bool willBePastRightSide = boxDesiredX < blockPoints.right;
                            if (willBePastRightSide)
                            {
                                realLeadingEdgeX = blockPoints.right + extraSpace;
                                hitTile = true;
                            }
                        }
                        else if (xDir == 1 && leftOfBlock)
                        {
                            bool willBePastLeftSide = boxDesiredX > blockPoints.left;
                            if (willBePastLeftSide)
                            {
                                realLeadingEdgeX = blockPoints.left - extraSpace;
                                hitTile = true;
                            }
                        }
                    }
                }
             }
        }
        if (hitTile)
        {
            realX = realLeadingEdgeX + -xDir * halfPlayerWidth;
        }
        else
        {
            realX = curPosm.x + velocity.x;
        }
        return realX;
    }

    private static float GetNewY(Vector2 curPosm, BoxCollider2D boxCollider, Vector2 velocity, ref bool hitTile, bool wasOnGround = false)
    {
        LevelMap levelMap = LevelMap.GetLevelMapObject();
        RectPoints boxColliderPoints = BoxColliderPoints(curPosm, boxCollider);
        bool onGround = OnGround(curPosm, boxCollider);
        int yDir = (velocity.y > 0) ? 1 : -1;
        if (velocity.y == 0)
        {
            yDir = -1;
        }
        float boxLeadingY = (yDir == -1) ? boxColliderPoints.bottom : boxColliderPoints.top;
        float boxDesiredY = boxLeadingY + velocity.y; //pos of leading edge if move full vel
        if (wasOnGround) boxDesiredY -= 0.2f;
        float boxRightX = boxColliderPoints.right;
        float boxLeftX = boxColliderPoints.left;
        int ystartY = RoundFloat(curPosm.y); //used to be leading
        int yendY = RoundFloat(boxDesiredY);
        int ystartX = RoundFloat(boxRightX);
        int yendX = RoundFloat(boxLeftX);
        int? curXIt, curYIt;
        curYIt = null;
        curXIt = null;
        int curX, curY;
        float slopeHeightToUse = 0;
        float nonSlopeHeightToUse = 0;
        float halfPlayerHeight = (boxCollider.size.y / 2.0f);
        bool hitBlock = false;
        float blockBottomY = 0;
        while (true)
        {
         //   Debug.Log("search tiles for ydir = " + yDir + "  y= " + ystartY + "," + yendY + ", and x" + ystartX + "," + yendX);
            NextTileToCheck(ref curYIt, ref curXIt, ystartY, yendY, ystartX, yendX);
            if (curYIt == null)
            {
                break;
            }
            curX = (int)curXIt;
            curY = (int)curYIt;

            if (levelMap.TileIsSolid(curX, curY))
            {
                if (yDir == 1)
                {
                    //when going up, ignore slopes completely
                    //assume that every slope will have a solid block below it
                    //assume that when moving in -y direction, will only intersect non-slopes of the same y
                    //should be fine unless moving very fast
                    if (levelMap.TileSlopeDir(curX, curY) == 0)
                    {
                        //blockBottomY = (float)curY - (tileSize / 2) - 0.02f;
                        blockBottomY = (float)curY - 0.02f;
                        hitBlock = true;
                        hitTile = true;
                        break;
                    }
                }
                else // 0 or -1
                {
                    float playerBotMidY = curPosm.y - (halfPlayerHeight) + velocity.y - 0.01f ; //was .03
                    if (wasOnGround) playerBotMidY -= 0.2f;
                    float slopeHeightAtPlayerMid = levelMap.getTileTop(curX, curY, curPosm.x);
                   // Debug.Log("ydir= " + yDir + "  col with " + curX + "," + curY + " slopeHeightAtPlayerMid=" + slopeHeightAtPlayerMid + " playerBotMidY= " + playerBotMidY + " playerX=" + curPosm.x);
                    if (levelMap.TileIsBelowSlope(curX, curY))
                    {
                        //completely ignore tiles that are below slopes
                        continue;
                    }

                    if (playerBotMidY > slopeHeightAtPlayerMid)
                    {
                        //inside a slope tile, but bottom of player box doesn't touch slope height
                        continue;
                    }
                    
                    int slopeDir = levelMap.TileSlopeDir(curX, curY);
                   // Debug.Log("ydir= " + yDir + "  col with " + curX + "," + curY + " slopeHeightAtPlayerMid=" + slopeHeightAtPlayerMid + " playerBotMidY= " + playerBotMidY + " playerX=" + curPosm.x + "  hitblock");
                    if (RoundFloat(curPosm.x) == curX && slopeDir != 0)
                    {
                        //assume there will be only one slope that matches x and can collide with player
                        //should be safe since it wouldn't make sense to stack multiple slopes in same x
                        //assume that every slope will have a solid block below it, otherwise could fall through sometimes
                        hitBlock = true;
                        hitTile = true;
                        slopeHeightToUse = slopeHeightAtPlayerMid;
                    }
                    else if (slopeDir == 0)
                    {
                        //assume that when moving in -y direction, will only intersect non-slopes of the same y
                        //should be fine unless moving very fast
                        hitBlock = true;
                        hitTile = true;
                        nonSlopeHeightToUse = slopeHeightAtPlayerMid;
                    }
                }
            }
        }
        float realY = 0;
        float realLeadingEdgeY = 0;
        float extraSpace = 0.01f;

        //If  hit a level tile, set realLeadingEdgeY
        if (hitBlock)
        { 
            if (yDir == 1)
            {
                realLeadingEdgeY = blockBottomY - extraSpace;
            }
            else //yDir 0 or 1
            {
                //if hitting a slope and a non-slope, use slope height
                if (slopeHeightToUse != 0)
                {
                    realLeadingEdgeY = (slopeHeightToUse) + extraSpace;
                }
                else
                {
                    realLeadingEdgeY = (nonSlopeHeightToUse) + extraSpace;
                }
            }
        }

        //If didn't hit a level tile, set realLeadingEdgeY if hit a non-level block
        if (!hitBlock)
        {
            foreach (GameObject block in GetBlocks())
            {
                BoxCollider2D blockCollider = block.GetComponent<BoxCollider2D>();
                RectPoints blockPoints = BoxColliderPoints(block.transform.position, blockCollider);
                bool aboveBlock = curPosm.y > block.transform.position.y;
                bool belowBlock = curPosm.y < block.transform.position.y;
                bool horizontalWithBlock = (boxColliderPoints.left < blockPoints.right) && (boxColliderPoints.right > blockPoints.left);
                if (horizontalWithBlock)
                {
                    if (yDir == -1 && aboveBlock)
                    {
                        bool willBePastTopSide = boxDesiredY < blockPoints.top;
                        if (willBePastTopSide)
                        {
                            realLeadingEdgeY = blockPoints.top + (extraSpace);
                            hitBlock = true;
                            hitTile = true;
                        }
                    }
                    else if (yDir == 1 && belowBlock)
                    {
                        bool willBePastBotSide = boxDesiredY > blockPoints.bottom;
                        if (willBePastBotSide)
                        {
                            realLeadingEdgeY = blockPoints.bottom - (extraSpace);
                            hitBlock = true;
                            hitTile = true;
                        }
                    }
                }
            }
        }

        if (hitBlock)
        {
            realY = realLeadingEdgeY + (yDir * -1) * halfPlayerHeight;
        }
        else
        {
            realY = curPosm.y + velocity.y;
        }
        return realY;
    }

    public static List<GameObject> GetTouchingObjects(BoxCollider2D boxCollider)
    {
        int numColliders = 100;
        BoxCollider2D[] colliders = new BoxCollider2D[numColliders];
        ContactFilter2D contactFilter = new ContactFilter2D();
        // Set you filters here according to https://docs.unity3d.com/ScriptReference/ContactFilter2D.html
        int colliderCount = boxCollider.OverlapCollider(contactFilter, colliders);

        List<GameObject> objectList = new List<GameObject>();
        for (int i = 0; i < colliderCount; i++)
        {
            BoxCollider2D col = colliders[i];
            GameObject g = col.gameObject;
            objectList.Add(g);
        }
        return objectList;
    }

    public static List<BoxCollider2D> GetTouchingColliders(BoxCollider2D boxCollider)
    {
        int numColliders = 100;
        BoxCollider2D[] colliders = new BoxCollider2D[numColliders];
        ContactFilter2D contactFilter = new ContactFilter2D();
        // Set your filters here according to https://docs.unity3d.com/ScriptReference/ContactFilter2D.html
        int colliderCount = boxCollider.OverlapCollider(contactFilter, colliders);

        List<BoxCollider2D> objectList = new List<BoxCollider2D>();
        for (int i = 0; i < colliderCount; i++)
        {
            BoxCollider2D col = colliders[i];
            objectList.Add(col);
        }
        return objectList;
    }


    public static bool GetTouchingObjectsWithTag(GameObject mainObj, string tag, ref List<GameObject> touchingObjects)
    {
        List<GameObject> objectList = GetTouchingObjects(mainObj.GetComponent<BoxCollider2D>());
        touchingObjects = new List<GameObject>();
        foreach (GameObject obj in objectList)
        {
            if (obj.tag.Equals(tag))
            {
                touchingObjects.Add(obj);
            }
        }
        return touchingObjects.Count > 0;
    }
    public static bool GetFirstTouchingObjectWithTag(GameObject mainObj, string tag, ref GameObject touchingObj)
    {
        List<GameObject> objectList = new List<GameObject>();
        GetTouchingObjectsWithTag(mainObj, tag, ref objectList);
        if(objectList.Count > 0)
        {
            touchingObj = objectList[0];
            return true;
        }
        touchingObj = null;
        return false;
    }

    public static bool IsTouchingObjectWithTag(GameObject mainObj, string tag)
    {
        List<GameObject> objectList = GetTouchingObjects(mainObj.GetComponent<BoxCollider2D>());
        foreach (GameObject obj in objectList)
        {
            if (obj.tag.Equals(tag))
            {
                return true;
            }
        }
        return false;
    }

    public static Vector2 GetValidPosition(Vector2 curPosm, BoxCollider2D boxCollider, Vector2 velocity, ref bool hitTileX, ref bool hitTileY, bool wasOnGround = false)
    {
        //currently assumes box and player colboxes have no offset
        float newX = GetNewX(curPosm, boxCollider, velocity, ref hitTileX);
        curPosm = new Vector2(newX, curPosm.y);
        float newY = GetNewY(curPosm, boxCollider, velocity, ref hitTileY, wasOnGround);
        
        return new Vector2(newX, newY);
    }

    private static void NextTileToCheck(ref int? curPri, ref int? curSec, int startPri, int endPri, int startSec, int endSec)
    {
        LevelMap levelMap = LevelMap.GetLevelMapObject();
        int dirPri = ((startPri - endPri) < 0) ? 1 : -1;
        int dirSec = -1;
        if (curPri == null)
        {
            curPri = startPri;
            curSec = startSec;
            return;
        }
        curSec += dirSec;
        if (curSec < endSec)
        {
            curPri += dirPri;
            curSec = startSec;
        }
        if(((dirPri == -1) && (curPri < endPri)) || ((dirPri == 1) && (curPri > endPri)))
        {
            curPri = null;
            curSec = null;
        }
    }

    private static RectPoints TilePoints(RectPoints worldPoints)
    {
        RectPoints TilePoints = new RectPoints();
        TilePoints.topLeft = GetTileForPoint(TilePoints.topLeft);
        TilePoints.topRight = GetTileForPoint(TilePoints.topRight);
        TilePoints.botLeft = GetTileForPoint(TilePoints.botLeft);
        TilePoints.botRight = GetTileForPoint(TilePoints.botRight);
        return TilePoints;
    }

    private static RectPoints BoxColliderPoints(Vector2 transformPos, BoxCollider2D boxCollider)
    {
        return BoxColliderPointsNoComp(transformPos, boxCollider.size.x, boxCollider.size.y, boxCollider.offset.x, boxCollider.offset.x);
    }

    private static RectPoints BoxColliderPointsNoComp(Vector2 transformPos, float width, float height, float offsetX = 0.0f, float offsetY = 0.0f)
    {
        RectPoints point = new RectPoints();
        Vector2 colCenterPos = new Vector2(transformPos.x + offsetX, transformPos.y + offsetY);
        float halfWidth = (width / 2);
        float halfHeight = (height / 2);
        float leftX = colCenterPos.x - halfWidth;
        float rightX = colCenterPos.x + halfWidth;
        float topY = colCenterPos.y + halfHeight;
        float bottomY = colCenterPos.y - halfHeight;
        point.topLeft = new Vector2(leftX, topY);
        point.topRight = new Vector2(rightX, topY);
        point.botLeft = new Vector2(leftX, bottomY);
        point.botRight = new Vector2(rightX, bottomY);
        point.top = topY;
        point.bottom = bottomY;
        point.left = leftX;
        point.right = rightX;
        point.width = width;
        point.height = height;
        return point;
    }


    public static Vector2 GetTileForPoint(Vector2 worldPoint)
    {
        int x = (int)(worldPoint.x);//was +.5
        int y = (int)(worldPoint.y);
        return new Vector2(x, y);
    }

    private static int RoundFloat(float num)
    {
        return (int)(num + 0.0f);//was .5
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
