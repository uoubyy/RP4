using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMic : MonoBehaviour
{
    public AudioSource AudioSrc;
    public AudioMixerGroup groupMaster, groupMic;
    public bool useMic;
    public AudioClip audioMicClip;
    public string selectedDevice;


    // Start is called before the first frame update
    void Start()
    {
        //Mic
        if (useMic)
        {
            if(Microphone.devices.Length > 0)
            {
                selectedDevice = Microphone.devices[0].ToString();
                AudioSrc.outputAudioMixerGroup = groupMic;
                AudioSrc.clip = Microphone.Start(selectedDevice, true, 10, AudioSettings.outputSampleRate);
            }
            else
            {
                useMic = false;
            }
        }
        if(!useMic)
        {
            AudioSrc.outputAudioMixerGroup = groupMaster;
            AudioSrc.clip = audioMicClip;
        }

        AudioSrc.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
