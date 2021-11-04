using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BreathIndicator : MonoBehaviour
{
    public float m_minScale = 1.0f;
    public float m_maxScale = 3.0f;

    public float m_timeElapsed = 0.0f;

    public float m_timeDuration = 3.0f;

    public float m_rotationSpeed = 1.0f;
    private float m_rotationZ = 0.0f;
    private int m_changeDir = 1;
    private Image[] m_childImages;

    public static float scale_target = 1.0f;
    private float currentScale = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        m_childImages = GetComponentsInChildren<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        m_rotationZ += Time.deltaTime * m_rotationSpeed;

/*        m_timeElapsed += Time.deltaTime;
        if (m_timeElapsed >= m_timeDuration)
        {
            m_timeElapsed = 0.0f;
            m_changeDir = -m_changeDir;
        }
*/
/*        foreach (Image img in m_childImages)
        {
            if (img.transform.parent == this.transform)
            {
                *//*                float start = m_changeDir > 0 ? m_minScale : m_maxScale;
                                float end = m_minScale + m_maxScale - start;
                                float scale = Mathf.Lerp(start, end, m_timeElapsed / m_timeDuration);*//*

                float veo = 0.0f;
                currentScale = Mathf.SmoothDamp(currentScale, scale_target, ref veo, Time.deltaTime);
                float scale = 1.1f * currentScale + 0.8f;
                img.transform.localScale = new Vector3(scale, scale, 1);
            }
        }
*/
        this.transform.localRotation = Quaternion.Euler(0, 0, m_rotationZ);
    }

    // 1 for speaking
    // -1 for quiet
    public int GetBreathState()
    {
        return m_changeDir;
    }
}
