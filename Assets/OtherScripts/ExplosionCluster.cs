using UnityEngine;
using System.Collections;

public class ExplosionCluster : MonoBehaviour
{

    public GameObject explosionPrefab;
    public float frequency = 0.4f;
    float xSize = 1.0f;
    float ySize = 1.0f;
    // Use this for initialization
    void Start()
    {
        InvokeRepeating("createExplosion", 0.0f, frequency);
    }
    private void createExplosion()
    {

        Instantiate(explosionPrefab, getRandomPos(), Quaternion.identity);
    }
    public void setSize(float x, float y)
    {
        xSize = x;
        ySize = y;
    }
    private Vector3 getRandomPos()
    {
        Vector3 myPos = transform.position;
        Vector3 randomPos = new Vector3();
        int xNeg = 1;
        if (Random.Range(0, 2) == 0)
        {
            xNeg = -1;
        }
        int yNeg = 1;
        if(Random.Range(0, 2) == 0)
        {
            yNeg = -1;
        }

        randomPos.x = myPos.x + Random.insideUnitCircle.x * xSize;
        randomPos.y = myPos.y + Random.insideUnitCircle.x * ySize;
        randomPos.z = myPos.z;

        return randomPos;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
