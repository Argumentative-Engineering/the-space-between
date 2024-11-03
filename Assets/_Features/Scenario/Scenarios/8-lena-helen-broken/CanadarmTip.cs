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


    private void OnTriggerEnter(Collider other)
    {
        if (_fuelCell.transform == other.transform)
        {
            DetachFuelCell();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N)) DetachFuelCell();
    }

    private void DetachFuelCell()
    {
        _fuelCell.transform.parent = null;
        _fuelCell.GetComponent<Collider>().isTrigger = true;

        var rb = _fuelCell.GetComponent<Rigidbody>();
        var fuelCellDir = -_fuelCell.right;
        fuelCellDir.y = 0;
        var angle = Vector3.Angle(fuelCellDir, (_fuelCell.position - _scoria.position).normalized);

        if (angle < 180 && angle > 140)
        {
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
                        print("DIALOGUE: yes!");
                        _controller.Leave();
                    });
        }
        else
        {
            rb.velocity = fuelCellDir * 10;
        }
    }
}
