using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keypad : MonoBehaviour
{
    public string password = "4716";
    private string userInput = "";

    bool _success = false;

    [SerializeField] DialogueData _rightPasswordVO, _wrongPasswordVO;

    private void Start()
    {
        userInput = "";
    }

    public void ButtonClicked(string num)
    {
        if (_success) return;
        userInput += num;
        if (userInput.Length >= 4)
        {
            if (userInput == password)
            {
                _success = true;
                NarrativeManager.Instance.PlayDialogue(_rightPasswordVO);
                ScenarioManager.Instance.RunNextScenario();
            }
            else
            {
                NarrativeManager.Instance.PlayDialogue(_wrongPasswordVO);
                userInput = "";
            }
        }
    }
}
