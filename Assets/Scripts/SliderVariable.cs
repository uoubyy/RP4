using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SliderVariable : MonoBehaviour
{
    public static int NPCRadius;
    public Slider RadiusSlider;
    public Text status;

    public void start()
    {
        NPCRadius = NPCRadius = ((int)RadiusSlider.value);
    }

    public void OnValueChanged()
    {
        NPCRadius = ((int)RadiusSlider.value);
    }

    public void FixedUpdate()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MainScene"))
        {
            if (NPCRadius == 1)
            {
                status.text = "Very Calm";
            }
            else if (NPCRadius == 2)
            {
                status.text = "Relaxed";
            }
            else if (NPCRadius == 3)
            {
                status.text = "Nervous";
            }
            else if (NPCRadius == 4)
            {
                status.text = "Anxious";
            }
            else if (NPCRadius == 5)
            {
                status.text = "Overwhelmed";
            }
        }
        
    }

}
