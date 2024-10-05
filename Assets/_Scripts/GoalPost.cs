using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class GoalPost : MonoBehaviour
{
    private int animalsInLevel = 2;
    private ParticleSystem particleEffect;
    private TMPro.TMP_Text numberDisplay;

    private void Start()
    {
        animalsInLevel = GameObject.FindGameObjectsWithTag("animal").Length;
        particleEffect = GetComponentInChildren<ParticleSystem>();
        numberDisplay = GetComponentInChildren<TMPro.TMP_Text>();
        numberDisplay.text = animalsInLevel.ToString();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "animal")
        {
            collision.gameObject.SetActive(false);
            particleEffect.Play();
            animalsInLevel--;
            numberDisplay.text = animalsInLevel.ToString();
            if (animalsInLevel == 0)
            {
                GameManager.Instance.ChangeState(GameState.Win);
            }
        }
    }
}
