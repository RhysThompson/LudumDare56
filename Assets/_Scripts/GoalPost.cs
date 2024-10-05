using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class GoalPost : MonoBehaviour
{
    private int animalsInLevel = 2;
    private ParticleSystem particleEffect;

    private void Start()
    {
        animalsInLevel = GameObject.FindGameObjectsWithTag("turtle").Length;
        particleEffect = GetComponentInChildren<ParticleSystem>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "animal")
        {
            collision.gameObject.SetActive(false);
            particleEffect.Play();
            if (--animalsInLevel == 0)
            {
                GameManager.Instance.ChangeState(GameState.Win);
            }
        }
    }
}
