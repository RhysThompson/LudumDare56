using UnityEngine;

public class ObjectDeleter : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "animal")
        {
            Destroy(collision.gameObject);
        }
    }
}
