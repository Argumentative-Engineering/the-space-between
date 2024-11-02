using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Buttons : MonoBehaviour
{
    public int keypadNum = 1;

    public UnityEvent KeypadClicked;

    private void OnMouseDown()
    {

        KeypadClicked.Invoke();
    }
}
