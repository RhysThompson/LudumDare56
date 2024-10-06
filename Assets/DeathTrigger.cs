using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour
{

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "animal")
        {
            EntityScript animal = collision.gameObject.GetComponent<EntityScript>();
            StartCoroutine(Death(animal));
        }
    }

    private IEnumerator Death(EntityScript animal)
    {
        animal.Die();
        yield return new WaitForSeconds(1);
        GameManager.Instance.ChangeState(GameState.Lose);
    }
}
