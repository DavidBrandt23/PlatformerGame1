using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class EntityManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame


    private void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown("Quit"))
        {
            Application.Quit();
        }
    }


    private List<BoxCollider2D> getAllColliders()
    {
        List<BoxCollider2D> objects = new List<BoxCollider2D>();
        objects.AddRange(gameObject.GetComponentsInChildren<BoxCollider2D>());
        return objects;
    }

    private void FixedUpdate()
    {
        List<BoxCollider2D> checkedColliders = new List<BoxCollider2D>();
        List<BoxCollider2D> colliders = getAllColliders();
        for(int i = 0; i < colliders.Count; i++)
        {
            checkedColliders.Add(colliders[i]);
            //Debug.Log(colliders[i].gameObject.name);
            List<BoxCollider2D> touchingCols = MyGlobal.GetTouchingColliders(colliders[i]);
            for (int j = 0; j < touchingCols.Count; j++)
            {
                if (checkedColliders.Contains(touchingCols[j]))
                {
                    continue;
                }
                GameObject objI = colliders[i].gameObject;
                GameObject objJ = touchingCols[j].gameObject;
                tellCollision(objI, objJ);
                tellCollision(objJ, objI);
            }
        }
    }
    private void tellCollision(GameObject A, GameObject B)
    {
        Collideable objI = A.gameObject.GetComponent<Collideable>();
        if (objI)
        {
            objI.InformCollision(B);
        }
    }

}
