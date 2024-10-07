using System.Linq;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class Spring : MonoBehaviour
{
    public float SpringForce = 20f;
    private Animator Anim;

    private void Start()
    {
        Anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "animal" && !collision.gameObject.GetComponentsInChildren<Spring>().Contains(this)) //prevent spring turtles from jumping infinitely
        {
            collision.gameObject.GetComponent<EntityScript>().Launch(SpringForce);
        }
        Anim.Play("SpringPlate");
        AudioSystem.Instance.PlaySFX("spring");
    }
}
