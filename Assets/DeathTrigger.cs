using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "animal" && !collision.gameObject.GetComponentsInChildren<DeathTrigger>().Contains(this)) //prevent spike turtles from killing themselves
        {
            collision.gameObject.GetComponent<EntityScript>().Die();
            AudioSystem.Instance.PlaySFX("spike");
        }
    }
}
