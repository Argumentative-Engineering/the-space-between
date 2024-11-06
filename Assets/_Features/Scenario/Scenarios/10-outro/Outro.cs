using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Outro : MonoBehaviour
{
    [SerializeField] DialogueData _outro;
    [SerializeField] GameObject _title;
    [SerializeField] Image _fade;

    void Start()
    {
        NarrativeManager.Instance.PlayDialogue(_outro);
        EventManager.Instance.RegisterListener("title", OnTitle);
    }

    private void OnDisable()
    {
        EventManager.Instance.UnregisterListener("title", OnTitle);
    }

    private void OnTitle(object[] obj)
    {
        _title.SetActive(true);
    }

    public void SwitchToMainMenu()
    {
        _fade.DOFade(1, 5).OnComplete(() =>
        {
            SceneManager.LoadScene("SCN_MainMenu");
        });
    }

}
