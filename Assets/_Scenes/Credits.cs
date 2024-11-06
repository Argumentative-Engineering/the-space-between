using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class Credits : MonoBehaviour
{
    [SerializeField] Image _creditimage;
    [SerializeField] Sprite[] _creditsprites;
    [SerializeField] float _switchtime = 3.0f;
    [SerializeField] string _menuscene;
    [SerializeField] bool _switchOnEnd = true;

    public UnityEvent OnCreditsEnd;

    private int currentImageIndex = 0;

    void Start()
    {
        if (_creditsprites.Length > 0)
        {
            StartCoroutine(SwitchImage());
        }
    }

    IEnumerator SwitchImage()
    {
        while (currentImageIndex < _creditsprites.Length)
        {
            _creditimage.sprite = _creditsprites[currentImageIndex];
            yield return new WaitForSeconds(_switchtime);
            currentImageIndex++;
        }

        OnCreditsEnd?.Invoke();
        if (_switchOnEnd)
            MenuScene();
    }

    void MenuScene()
    {
        SceneManager.LoadScene(_menuscene);
    }
}
