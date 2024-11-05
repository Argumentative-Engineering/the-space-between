using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Dolphin : MonoBehaviour
{
    void Start()
    {
        transform.DORotate(new Vector3(-86, 0, 0), 2).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }

    void Update()
    {

    }
}
