using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour {
    public Vector3 direction;
    [SerializeField] private float bulletSpeed;
    private BoxCollider2D m_BoxCollider;
    public bool penetrateEnemy;


    void Start () {
        m_BoxCollider = GetComponent<BoxCollider2D>();
        Collideable c = GetComponent<Collideable>();
    }

    void FixedUpdate()
    {
        Transform transform = GetComponent<Transform>();
        bool hitTile = false, hitTileY = false;
        Vector2 velocity = (new Vector2(direction.x, direction.y)) * bulletSpeed;

        transform.position = MyGlobal.GetValidPosition(transform.position, m_BoxCollider, velocity, ref hitTile, ref hitTileY);
        if (hitTile || hitTileY)
        {
            Destroy(this.gameObject);
        }
    }
}
