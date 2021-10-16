using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchScript : MonoBehaviour
{
    public Text m_Text;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Update the Text on the screen depending on current position of the touch each frame
            m_Text.text = "Touch Position : " + touch.position;
        }
        else
        {
            m_Text.text = "No touch contacts";
        }
    }
}
