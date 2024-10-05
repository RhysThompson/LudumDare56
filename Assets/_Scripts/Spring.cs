using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class Spring : MonoBehaviour
{
    public float SpringForce = 20f;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "animal")
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            rb.AddForce(Vector3.up * SpringForce, ForceMode.Impulse);
        }
    }
}
