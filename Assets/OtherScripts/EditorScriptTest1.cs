using UnityEngine;
[ExecuteInEditMode]
public class EditorScriptTest1 : MonoBehaviour
{
    public Vector3 lookAtPoint = Vector3.zero;
    [SerializeField]
    public GameObject prefab;
    [SerializeField]
    public Sprite sprite;
    public void Update()
    {
        //transform.position = new Vector3(6, 6, 0);
        //transform.position = lookAtPoint;
    }
    public void createTest()
    {
      //  GameObject.Instantiate(prefab, new Vector3(6,6,0), Quaternion.identity);
    }
}