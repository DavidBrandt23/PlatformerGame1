using UnityEngine;
using System.Collections;

public class MoveInDirection : MonoBehaviour
{
    public Vector3 direction;
    public FloatReference speed;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        Vector3 currentPost = transform.position;
        currentPost += direction * speed.Value;
        transform.position = currentPost;
    }
}
