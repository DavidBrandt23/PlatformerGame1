using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour
{
    // Don't use Tile class outside of LevelMap
    public bool IsSolid;

    [SerializeField]
    private int _leftY;
    public int LeftY
    {
        get{ return _leftY; }
        private set{  _leftY = value; }
    }

    [SerializeField]
    private int _rightY;
    public int RightY
    {
        get { return _rightY; }
        private set { _rightY = value; }
    }


    // Use this for initialization
    void Start()
    {
        IsSolid = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
