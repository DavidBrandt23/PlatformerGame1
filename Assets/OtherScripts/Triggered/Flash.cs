using UnityEngine;
using System.Collections;
using System;

public class Flash : MonoBehaviour
{
    private float litTimeLeft = 0.0f;
    public float flashInterval = 0.03f;
    private bool flashing = false;
    private bool lit = false;
    public bool StartFlashing;
    public Color flashColor = new Color(1.0f, 0.0f, 0.0f);
    private SpriteRenderer m_SpriteRenderer;
    public void FlashOnce()
    {
        Debug.Log("FLASH");
        lit = true;
        litTimeLeft = flashInterval;
    }
    public void SetIsFlashing(bool value)
    {
        flashing = value;
    }

    // Use this for initialization
    void Start()
    {
        m_SpriteRenderer = this.GetComponent<SpriteRenderer>();
        if (StartFlashing)
        {
            flashing = true;
            litTimeLeft = 0.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        if (flashing)
        {
            if (lit)
            {
                m_SpriteRenderer.color = flashColor;

            }
            else
            {
                m_SpriteRenderer.color = new Color(1.0f, 1.0f, 1.0f);
            }

            if (litTimeLeft > 0)
            {
                litTimeLeft -= Time.fixedDeltaTime;
            }
            else
            {
                lit = !lit;
                litTimeLeft = flashInterval;
            }
        }
        else
        {
            m_SpriteRenderer.color = new Color(1.0f, 1.0f, 1.0f);
        }
    }
}
