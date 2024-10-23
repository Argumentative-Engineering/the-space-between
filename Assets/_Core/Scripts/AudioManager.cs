using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    public void PlayEvent(EventReference evt)
    {
        RuntimeManager.PlayOneShot(evt);
    }
}
