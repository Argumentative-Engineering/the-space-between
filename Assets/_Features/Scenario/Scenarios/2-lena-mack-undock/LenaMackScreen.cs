using System;
using DG.Tweening;
using FMODUnity;
using UnityEngine;

public class LenaMackScreen : DialogueInteractable
{
    EventManager _evt;
    [SerializeField] Light _light;
    [SerializeField] Renderer _beeperButton;
    [SerializeField] StudioEventEmitter _sound;

    bool _isBeeping;

    private void Start()
    {
        _evt = EventManager.Instance;
        _evt.RegisterListener(EventDefinitions.StartBeeping, OnStartBeeping);
        Tooltip = "";
    }

    private void OnDisable()
    {
        _evt.UnregisterListener(EventDefinitions.StartBeeping, OnStartBeeping);
    }

    private void OnStartBeeping(object[] obj)
    {
        _isBeeping = (bool)obj[0];
        if (!_isBeeping)
        {
            StopBeeping();
            return;
        }
        Tooltip = "Talk to Mack";
        float duration = 1f;

        _light.intensity = 0;
        _light.DOIntensity(2, duration).SetLoops(-1, LoopType.Yoyo);

        Color initialEmission = Color.red * 1;
        Color targetEmission = Color.red * 20;
        var targetMaterial = _beeperButton.material;

        DOTween.To(
                  () => initialEmission,
                  x => targetMaterial.SetColor("_EmissionColor", x),
                  targetEmission,
                  duration
              ).SetLoops(-1, LoopType.Yoyo);

        _sound.Play();
    }

    public override bool TryInteract()
    {
        if (!_isBeeping) return false;
        StopBeeping();
        return base.TryInteract();
    }

    void StopBeeping()
    {
        _light.DOColor(Color.white * 0, 0.5f);
        _light.DOKill();
        _sound.Stop();
    }
}
