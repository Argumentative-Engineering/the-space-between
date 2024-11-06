using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CanadarmTip : MonoBehaviour
{
    [SerializeField] CanadarmController _controller;
    [SerializeField] Transform _fuelCell;
    [SerializeField] Transform _scoria;
    [SerializeField] Transform _path1;
    [SerializeField] Transform _path2;

    Vector3 _angularVel;

    bool _didHit;

    private void OnTriggerEnter(Collider other)
    {
        if (_fuelCell.transform == other.transform)
        {
            DetachFuelCell();
        }
    }

    public IEnumerator WaitDialogue(DialogueData success, DialogueData fail)
    {
        float elapsed = 0;
        while (elapsed < 4 && !_didHit)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }

        NarrativeManager.Instance.PlayDialogue(_didHit ? success : fail);
    }

    public void DetachFuelCell()
    {
        var rb = _fuelCell.GetComponent<Rigidbody>();
        var fuelCellDir = -_fuelCell.right;
        fuelCellDir.y = 0;
        var angle = Vector3.Angle(fuelCellDir, (_fuelCell.position - _scoria.position).normalized);

        if (angle < 180 && angle > 140)
        {
            _fuelCell.transform.parent = null;
            _fuelCell.GetComponent<Collider>().isTrigger = true;

            _didHit = true;

            rb.angularVelocity = transform.up * 5;
            DOTween.Sequence()
                    .Append(_fuelCell.transform.DOMove(_path1.position, 10).SetEase(Ease.Linear)
                            .OnComplete(() => rb.angularVelocity = Vector3.zero))
                    .Join(DOTween.To(() => _angularVel, x => _angularVel = x, _fuelCell.up * 5, 0.01f))
                    .Append(_fuelCell.transform.DOMove(_path2.position, 3)).SetEase(Ease.OutSine)
                    .Join(_fuelCell.DORotate(new Vector3(-3, -155, 12), 3))
                    .AppendCallback(() =>
                    {
                        _fuelCell.GetComponent<Collider>().isTrigger = false;
                        rb.isKinematic = true;
                        _controller.LeaveDialogue();
                        _controller.Leave();
                    });
        }
    }
}
