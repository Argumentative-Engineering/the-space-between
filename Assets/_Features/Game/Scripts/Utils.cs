using System;
using DG.Tweening;
using UnityEngine;

public class AnimationUtils
{
    public static void AnimateLookAt(
        Vector3 position,
        bool zoom = false,
        Action onComplete = null,
        float duration = 3,
        float delay = 2
    )
    {
        var input = PlayerInteraction.Instance.GetComponent<PlayerLocalInput>();
        PlayerSettings.Instance.OverrideCameraRotation = true;
        PlayerSettings.FreezePlayer(true);
        var lastRot = Camera.main.transform.localRotation;
        var lastZoom = input.IsZooming;
        var seq = DOTween.Sequence()
            .Append(Camera.main.transform.DOLookAt(position, duration))
            .JoinCallback(() => input.IsZooming = zoom)
            .AppendInterval(delay)
            .Append(Camera.main.transform.DOLocalRotate(lastRot.eulerAngles, duration))
            .JoinCallback(() => input.IsZooming = false)
            .AppendCallback(() =>
            {
                PlayerSettings.Instance.OverrideCameraRotation = false;
                PlayerSettings.FreezePlayer(false);
                onComplete?.Invoke();
            });
    }
}