
using System;
using DG.Tweening;
using UnityEngine;

public class LenaMackScreen : DialogueInteractable
{
    EventManager _evt;
    [SerializeField] Light _light;
    [SerializeField] Renderer _beeperButton;

    bool _isBeeping;

    private void Start()
    {
        _evt = EventManager.Instance;
        _evt.RegisterListener("start-beeping", StartBeeping);
    }

    private void OnDisable()
    {
        _evt.UnregisterListener("start-beeping", StartBeeping);
    }

    private void StartBeeping(object[] obj)
    {
        _isBeeping = (bool)obj[0];
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
    }
    public override bool TryInteract()
    {
        if (!_isBeeping) return false;
        return base.TryInteract();
    }
}
