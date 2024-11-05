using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using FMOD.Studio;
using FMODUnity;
using TMPro;
using UnityEngine;

public class VerticalTextSpawner : MonoBehaviour
{
    [SerializeField] DialogueData _dialogue;
    [SerializeField] AudioSystem _source;

    EventInstance _instance;

    [SerializeField] GameObject[] _pool;
    int _poolPointer = 0;

    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            _pool[i].SetActive(false);
        }

        _instance = RuntimeManager.CreateInstance(_dialogue.DialogueEvent);
        _instance.start();
        _source.AssignEvents(_instance);
    }

    private void OnEnable()
    {
        _source.OnMarker += OnMarker;
    }

    private void OnDisable()
    {
        _source.OnMarker -= OnMarker;
    }

    private void OnMarker(string marker)
    {
        if (marker == "end")
        {
            StopSequence();
            EventManager.Instance.BroadcastEvent("finished-fb");
            return;
        }

        var line = _dialogue.Subtitles.Where(d => d.Key == marker).FirstOrDefault();
        if (line == null)
        {
            Debug.LogError($"Missing dialogue for: {marker}; Current dialogue: {_dialogue.DialogueEvent.Path}");
            return;
        }

        var text = _pool[_poolPointer];
        var tmp = text.GetComponent<TextMeshPro>();
        tmp.text = line.Text;
        tmp.DOKill(complete: true);
        DOTween.Sequence().Append(tmp.DOFade(1, 1).From(0)).AppendInterval(2).Append(tmp.DOFade(0, 1));
        text.SetActive(true);

        _poolPointer = (_poolPointer + 1) % _pool.Length;
    }

    public void StopSequence()
    {
        _instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        _instance.release();
    }
}
