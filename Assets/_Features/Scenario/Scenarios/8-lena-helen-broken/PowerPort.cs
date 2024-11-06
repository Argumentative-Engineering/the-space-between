using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PowerPort : MonoBehaviour
{
    [SerializeField] bool _isPositive;

    [SerializeField] FixScoriaScenario _scenario;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fuel Cell") && (_isPositive ? other.name == "pos" : other.name == "neg"))
        {
            Destroy(GetComponent<GrabbableObject>());
            PlayerInteraction.Instance.StopInteracting();
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<Collider>().isTrigger = true;
            transform.DOMove(other.transform.position, 2);
            _scenario.ConnectPowercell();
        }
    }
}
