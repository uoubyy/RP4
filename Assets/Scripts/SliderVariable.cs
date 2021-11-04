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
        NPCRadius = ((int)RadiusSlider.value);
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
                status.text = "Player currently very calm";
            }
            else if (NPCRadius == 2)
            {
                status.text = "Player currently relaxed";
            }
            else if (NPCRadius == 3)
            {
                status.text = "Player currently tense";
            }
            else if (NPCRadius == 4)
            {
                status.text = "Player currently anxious";
            }
            else if (NPCRadius == 5)
            {
                status.text = "Player currently overwhelmed";
            }
        }
        
    }

}
