using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "animal")
        {
            collision.gameObject.GetComponent<EntityScript>().Die();
        }
    }
}
