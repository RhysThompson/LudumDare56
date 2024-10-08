using System.Collections;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
[SelectionBase]
public class GoalPost : MonoBehaviour
{
    public int AnimalsNeeded = 1;
    private ParticleSystem ParticleEffect;
    private TMPro.TMP_Text NumberDisplay;

    //private IEnumerator Coroutine;
    private void Start()
    {
        ParticleEffect = GetComponentInChildren<ParticleSystem>();
        NumberDisplay = GetComponentInChildren<TMPro.TMP_Text>();
        NumberDisplay.text = AnimalsNeeded.ToString();
        GameManager.Instance.RegisterAnimalsNeeded(AnimalsNeeded);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "animal")
        {
            ParticleEffect.Play();
            AnimalsNeeded--;
            if (AnimalsNeeded <= 0)
            {
                AudioSystem.Instance.PlaySFX("success");
                NumberDisplay.text = "W";
            }
            else
                NumberDisplay.text = AnimalsNeeded.ToString();
            collision.gameObject.SetActive(false);
            GameManager.Instance.RegisterAnimalsNeeded(-1);
            GameManager.Instance.RegisterAnimal(-1);
            /*if (AnimalsNeeded == 0)
            {
                Coroutine = WinAfterDelay();
                StartCoroutine(Coroutine);
            }*/
        }
    }
    /*private IEnumerator WinAfterDelay()
    {
        yield return new WaitForSeconds(2f);
        GameManager.Instance.ChangeState(GameState.Win);
    }*/
}
