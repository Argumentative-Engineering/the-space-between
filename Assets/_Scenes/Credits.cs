using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    [SerializeField] Image _creditimage;
    [SerializeField] Sprite[] _creditsprites;
    [SerializeField] float _switchtime = 3.0f;
    [SerializeField] string _menuscene;

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

        MenuScene();
    }

    void MenuScene()
    {
        SceneManager.LoadScene(_menuscene);
    }
}
