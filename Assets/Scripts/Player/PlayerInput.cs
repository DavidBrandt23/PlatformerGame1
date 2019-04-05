using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerInput : MonoBehaviour
{
    private PlayerController m_Character;
    private bool m_Jump;
    private bool m_Boost;
    private bool m_Shoot;


    private void Awake()
    {
        m_Character = GetComponent<PlayerController>();
    }

    private void Update()
    {
        bool pausePressed;
        if (!m_Jump)
        {
            // Read the jump input in Update so button presses aren't missed.
           // m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
        }
        m_Jump = CrossPlatformInputManager.GetButton("Jump");
        if (!m_Shoot)
        {
            // Read the jump input in Update so button presses aren't missed.
            m_Shoot = CrossPlatformInputManager.GetButtonDown("Shoot");
        }
        m_Boost = Input.GetButton("Run");
    }


    private void FixedUpdate()
    {
        // Read the inputs.
        bool crouch = Input.GetKey(KeyCode.LeftControl);
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        // Pass all parameters to the character control script.
        m_Character.Move(h, crouch, m_Jump, m_Boost);
        if(m_Shoot)m_Character.shootPressed();
        //m_Jump = false;
        m_Shoot = false;
    }
    
}
