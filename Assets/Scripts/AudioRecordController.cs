using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioRecordController : MonoBehaviour
{
    public bool m_useAudio = false;

    public AudioClip m_testAudioTrack;
    public AudioSource m_audioSource;
    public float m_updateStep = 0.1f;
    public int m_sampleDataLength = 1024;

    public float m_minScale = 0.5f;
    public float m_maxScale = 8.0f;

    public float m_targetScale = 1.0f;
    public float m_currentScale = 1.0f;

    public Transform[] m_visualObjects;

    public GameObject m_player;
    public GameObject[] m_crowds;
    public float m_crowdsMoveSpeed = 1.0f;
    private Vector3[] m_crowdsTarget;

    private string m_inputDevice;

    private float m_currentUpdateTime = 0.0f;

    private float[] m_clipSampleData;
    private float m_clipLoudness = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        if (!m_useAudio && Microphone.devices.Length > 0)
        {
            m_inputDevice = Microphone.devices[0].ToString();
            Debug.Log("Audio input device " + m_inputDevice);
            m_audioSource.clip = Microphone.Start(m_inputDevice, true, 10, AudioSettings.outputSampleRate);
            m_audioSource.loop = true;
            while (!(Microphone.GetPosition(null) > 0)) { }
        }
        else
        {
            m_audioSource.loop = true;
            m_audioSource.clip = m_testAudioTrack;
        }
        m_audioSource.Play();
        m_clipSampleData = new float[m_sampleDataLength];

        m_crowdsTarget = new Vector3[m_crowds.Length];
    }

    // Update is called once per frame
    void Update()
    {
        if (m_audioSource)//&& !string.IsNullOrEmpty(m_inputDevice))
        {
            m_currentUpdateTime += Time.deltaTime;
            if (m_currentUpdateTime >= m_updateStep)
            {
                m_currentUpdateTime = 0.0f;
                m_audioSource.clip.GetData(m_clipSampleData, m_audioSource.timeSamples);

                m_clipLoudness = 0.0f;
                foreach (var sample in m_clipSampleData)
                {
                    m_clipLoudness += Mathf.Abs(sample);
                }

                // m_clipLoudness /= m_sampleDataLength;

                m_clipLoudness = m_clipLoudness / 10.0f;
                //Debug.Log("Voice loundness " + m_clipLoudness);

                if (m_clipLoudness > 20.0f)
                    m_targetScale = m_maxScale;
                else
                    m_targetScale = m_minScale;

                int crowdsMoveDir = -1;
                if (m_clipLoudness > 10.0f)
                    crowdsMoveDir = 1;

                for (var idx = 0; idx < m_crowds.Length; idx++)
                {
                    Vector3 dir = m_crowds[idx].transform.position - m_player.transform.position;
                    dir.Normalize();
                    m_crowdsTarget[idx] = m_crowds[idx].transform.position + dir * crowdsMoveDir * m_crowdsMoveSpeed * m_updateStep;
                }
            }

            float scaleSpeed = 0.0f;
            m_currentScale = Mathf.SmoothDamp(m_currentScale, m_targetScale, ref scaleSpeed, m_updateStep);
            foreach (var obj in m_visualObjects)
            {
                obj.localScale = new Vector3(1, m_currentScale, 1);
            }

            for (var idx = 0; idx < m_crowds.Length; idx++)
            {
                m_crowds[idx].transform.position = Vector3.MoveTowards(m_crowds[idx].transform.position, m_crowdsTarget[idx], m_updateStep);
            }
        }
    }
}
