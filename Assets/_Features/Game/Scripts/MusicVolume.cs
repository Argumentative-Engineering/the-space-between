using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicVolume : TriggerInteractable
{
    bool _isPlaying;
    private void OnTriggerEnter(Collider other)
    {
        if (_isPlaying) return;
        if (other.CompareTag("Player"))
        {
            MusicManager.Instance.PlayMusic();
            _isPlaying = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!_isPlaying) return;
        if (other.CompareTag("Player"))
        {
            MusicManager.Instance.StopMusic();
            _isPlaying = false;
        }

    }
}
