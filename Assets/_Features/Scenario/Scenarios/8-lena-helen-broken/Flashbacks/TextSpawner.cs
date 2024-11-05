using System;
using System.Linq;
using DG.Tweening;
using FMOD.Studio;
using FMODUnity;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(AudioSystem))]
public class TextSpawner : MonoBehaviour
{
    [SerializeField] DialogueData _dialogue;
    [SerializeField] AudioSystem _source;

    EventInstance _instance;

    [SerializeField] GameObject _textPrefab;
    [NonSerialized] readonly GameObject[] _pool = new GameObject[4];
    int _poolPointer = 0;

    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            var text = Instantiate(_textPrefab, transform.position, Quaternion.identity, transform);
            text.SetActive(false);
            _pool[i] = text;
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
        text.GetComponent<TextMeshPro>().text = line.Text;

        text.SetActive(true);
        text.transform.position = new(
            UnityEngine.Random.Range(-15, 8),
            1,
            transform.position.z
        );
        text.transform.LookAt(Camera.main.transform.position);
        text.transform.localScale = new Vector3(-1, 1, 1);
        text.transform.DOMoveZ(-20, 10).SetEase(Ease.Linear).OnComplete(() =>
        {
            text.SetActive(false);
        });
        _poolPointer = (_poolPointer + 1) % _pool.Length;
    }

    public void StopSequence()
    {
        _instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        _instance.release();
    }
}
