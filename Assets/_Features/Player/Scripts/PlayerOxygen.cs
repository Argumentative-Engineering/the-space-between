using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerOxygen : MonoBehaviour
{
    public static PlayerOxygen Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    [Header("Settings")]
    public bool StartDecreasingOxygen;

    [Header("References")]
    [SerializeField] TextMeshProUGUI _oxygenLevelText;
    Volume _ppVolume;

    public float CurrentOxygen { get; set; } = 100;

    private void Start()
    {
        _ppVolume = FindObjectOfType<Volume>();
    }

    void Update()
    {
        DecreaseOxygen();
        UpdateUI();
    }

    void DecreaseOxygen()
    {
        if (!StartDecreasingOxygen || PlayerSettings.Instance.IsFrozen || GameManager.Instance.IsReloading) return;
        CurrentOxygen -= Time.deltaTime * 0.2f;

        if (CurrentOxygen <= 0)
        {
            PlayerSettings.Instance.KillPlayer(KillType.NoOxygen);
            StartDecreasingOxygen = false;
        }
    }

    void UpdateUI()
    {
        _oxygenLevelText.text = $"{Mathf.Ceil(CurrentOxygen)}%";

        if (_ppVolume.profile.TryGet(out ChromaticAberration ca))
        {
            ca.intensity.value = Mathf.Lerp(1, 0, CurrentOxygen / 100);
        }

        if (_ppVolume.profile.TryGet(out MotionBlur blur))
        {
            blur.intensity.value = Mathf.Lerp(1, 0.51f, CurrentOxygen / 100);
        }
    }
}
