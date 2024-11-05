using FMODUnity;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] StudioEventEmitter _emitter;
    public static MusicManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    public void PlayMusic()
    {
        _emitter.Play();
    }

    public void StopMusic()
    {
        _emitter.Stop();
    }
}