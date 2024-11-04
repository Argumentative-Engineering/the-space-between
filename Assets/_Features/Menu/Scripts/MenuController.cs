using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    bool _canStart = false;
    GameInput _input;
    [SerializeField] Image _fadeImage;

    private void OnEnable()
    {
        _input = new();
        _input.UI.Start.performed += OnStartPressed;
        _input.UI.Enable();
    }

    private void OnDisable()
    {
        _input.UI.Start.performed -= OnStartPressed;
        _input.UI.Disable();
    }

    void Start()
    {
        StartCoroutine(MenuAnimationSequence());
    }

    IEnumerator MenuAnimationSequence()
    {
        Cursor.visible = false;
        yield return new WaitForSeconds(3); // wait 3 seconds then show cursor and show other stuff lol
        Cursor.visible = true;
        _canStart = true;
    }

    private void OnStartPressed(InputAction.CallbackContext context)
    {
        if (!_canStart) return;
        _fadeImage.DOFade(1, 2).OnComplete(() =>
        {
            StartCoroutine(LoadLevels());
        });
    }

    IEnumerator LoadLevels()
    {
        yield return new WaitForSeconds(2);
        var core = SceneManager.LoadSceneAsync("SCN_Core");
        var scene = SceneManager.LoadSceneAsync("SCN_lena-inside-helen", LoadSceneMode.Additive);
        scene.completed += (_) =>
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("SCN_lena-inside-helen"));
            ScenarioManager.Instance.LoadScenarios();
            ScenarioManager.Instance.RunNextScenario(movePlayer: true);
        };
    }

}
