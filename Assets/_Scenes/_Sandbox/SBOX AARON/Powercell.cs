using UnityEngine;

public class PowerCell : MonoBehaviour
{
    public GameObject ropeEnd;
    public bool isPuzzleComplete = false;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject == ropeEnd)
        {
            ConnectRopeToPowerCell();


            isPuzzleComplete = true;

            OnPuzzleComplete();
        }
    }

    private void ConnectRopeToPowerCell()
    {
        Rigidbody ropeEndRb = ropeEnd.GetComponent<Rigidbody>();
        if (ropeEndRb != null)
        {

            ropeEndRb.GetComponent<GrabbableObject>().DropItem();

            ropeEndRb.isKinematic = true;

            FixedJoint fixedJoint = gameObject.AddComponent<FixedJoint>();
            fixedJoint.connectedBody = ropeEndRb;

            fixedJoint.breakForce = Mathf.Infinity;
        }
    }


    private void OnPuzzleComplete()
    {
        Debug.Log("Puzzle completed! The Power Cell is now connected.");
    }
}
