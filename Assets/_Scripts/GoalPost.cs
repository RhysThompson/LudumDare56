using System.Collections;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class GoalPost : MonoBehaviour
{
    public static int animalsInLevel = 2;
    private ParticleSystem particleEffect;

    private void Start()
    {
        animalsInLevel = GameObject.FindGameObjectsWithTag("animal").Length;
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
    private IEnumerator WinAfterDelay()
    {
        yield return new WaitForSeconds(1f);
        GameManager.Instance.ChangeState(GameState.Win);
    }
}
