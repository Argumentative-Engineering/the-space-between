using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LenaMackScreen : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.Instance.RegisterListener("start-beeping", StartBeeping);
    }

    private void StartBeeping(object[] obj)
    {
        bool beeping = (bool)obj[0];

        if (beeping)
        {

        }
    }

    [SerializeField] Light _light;
}
