using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioRecordController : MonoBehaviour
{
    public bool m_useAudio = false;

    public AudioClip m_testAudioTrack;
    [Range(0.0f, 1.0f)]
    public float m_testAudioVolume = 1.0f;
    public AudioSource m_audioSource;
    public float m_updateStep = 0.1f;
    public int m_sampleDataLength = 1024;

    public float m_minScale = 0.5f;
    public float m_maxScale = 8.0f;

    public float m_breathThreshold = 20.0f;

    private float m_targetScale = 1.0f;
    private float m_currentScale = 1.0f;

    public Transform[] m_visualObjects;
    public Text m_audioVolumeNum;

    public GameObject m_player;
    public NPCController[] m_crowd;
    public float m_crowdMoveSpeed = 1.0f;
    private Vector3[] m_crowdTarget;

    private string m_inputDevice;

    private float m_currentUpdateTime = 0.0f;

    private float[] m_clipSampleData;

    private float m_rhythmSimilarity = 0.0f;

    public BreathIndicator m_breathIndictor;

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

        m_crowdTarget = new Vector3[m_crowd.Length];
    }

    void CalculateRhythmSimilarity(float loudness)
    {
        int state = m_breathIndictor.GetBreathState();
        if ((state == 1 && loudness >= m_breathThreshold) ||
        (state == -1 && loudness < m_breathThreshold))
            m_rhythmSimilarity += 1;
        else
            m_rhythmSimilarity -= 1;

        m_rhythmSimilarity = Mathf.Clamp(m_rhythmSimilarity, 0, 2.0f / m_updateStep);
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

                float clipLoudness = 0.0f;
                foreach (var sample in m_clipSampleData)
                {
                    clipLoudness += Mathf.Abs(sample);
                }

                // m_clipLoudness /= m_sampleDataLength;

                clipLoudness = clipLoudness * (m_useAudio ? m_testAudioVolume : 100.0f);

                // after an interval, calculate similarity again
                // similarity > 0.8 * 1.0f / m_updateStep move away
                CalculateRhythmSimilarity(clipLoudness);

                m_audioVolumeNum.text = string.Format("Audio input device {0}\nloudness {1}\nsimilarity{2}", m_inputDevice, clipLoudness, m_rhythmSimilarity);

                if (clipLoudness > m_breathThreshold)
                    m_targetScale = m_maxScale;
                else
                    m_targetScale = m_minScale;

                for (var idx = 0; idx < m_crowd.Length; idx++)
                {
                    if (m_rhythmSimilarity > 2.0f / m_updateStep * 0.6f)
                        m_crowd[idx].MoveOutPlayer();
                    else
                        m_crowd[idx].MoveToPlayer();
                }
            }

            float scaleSpeed = 0.0f;
            m_currentScale = Mathf.SmoothDamp(m_currentScale, m_targetScale, ref scaleSpeed, m_updateStep);
            foreach (var obj in m_visualObjects) // update the real-time voice indicator
            {
                obj.localScale = new Vector3(1, m_currentScale, 1);
            }
        }
    }
}
