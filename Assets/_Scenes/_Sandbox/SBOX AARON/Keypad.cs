using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keypad : MonoBehaviour
{
    public string password = "4716";
    private string userInput = "";

    private void Start()
    {
        userInput = "";
    }

    public void ButtonClicked(string num)
    {
        userInput += num;
        if (userInput.Length >= 4) 
        {
            if (userInput == password)
            {
                Debug.Log("Entry Allowed");
            }
            else
            {
                Debug.Log("Access Denied");
                userInput = "";
            }
        }
    }
}
