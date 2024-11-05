using DG.Tweening;
using TMPro;
using UnityEngine;

public class GameHints : MonoBehaviour
{
    public bool HasShownJumpHint;

    [SerializeField] TextMeshProUGUI _hintUI;
    public static GameHints Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    public void ShowHint(string hint)
    {
        _hintUI.text = $"<color=yellow><b>!</b></color>  {hint}";
        _hintUI.DOKill();
        DOTween.Sequence().Append(

        _hintUI.DOFade(1, 0.5f).From(0)
        ).AppendInterval(4).Append(
        _hintUI.DOFade(0, 0.5f).From(1));
    }

}