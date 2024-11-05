using UnityEngine;

public class GodrayMover : MonoBehaviour
{
    private void Update()
    {
        transform.Translate(50 * Time.deltaTime * Vector3.up);
    }
}