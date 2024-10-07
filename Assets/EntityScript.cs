using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class EntityScript : MonoBehaviour
{
    public bool IsBig = true;
    public Vector3 BigSize;
    public Vector3 SmallSize;
    public float SizeChangeSpeed = 0.1f;
    public float MoveSpeed = 10f;
    public float MaxFallSpeed = -20f;

    private Rigidbody RB;
    private bool Launched = false;
    private float LaunchTargetHeight = 0f;
    private ParticleSystem deathParticles;
    // Start is called before the first frame update
    void Start()
    {
        RB = this.GetComponent<Rigidbody>();
        GameManager.Instance.RegisterAnimal(1);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        AdjustSize();
        Move();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            IsBig = !IsBig;
        }
    }

    public virtual void Move()
    {
        if (Launched)
        {
            Vector3 targetPos = transform.position;
            targetPos.x += MoveSpeed * Time.fixedDeltaTime;
            targetPos.y += (/*Mathf.Min(*/Mathf.Pow(LaunchTargetHeight - transform.position.y, 2f) + 0.1f/*, 15)*/) * Time.fixedDeltaTime;
            RB.MovePosition(targetPos); 
            if (LaunchTargetHeight - transform.position.y < 1.7f)
            {
                RB.useGravity = true;
                Launched = false;
            }
        }
        else
        {
            RB.velocity = new Vector3(RB.velocity.x, Mathf.Max(RB.velocity.y, MaxFallSpeed), RB.velocity.z); // Cap falling speed

            RB.MovePosition(this.transform.position + new Vector3(MoveSpeed * Time.fixedDeltaTime, 0, 0)); // Move left
        }
    }

    public virtual void Launch(float launchPower)
    {
        //RB.AddForce(Vector3.up * launchPower, ForceMode.Impulse);
        Launched = true;
        RB.useGravity = false;
        LaunchTargetHeight = this.transform.position.y + launchPower + 1.5f;
    }

    public virtual bool CanGrow(Vector3 newScale)
    {
        Vector3 pos = this.transform.position;
        pos.y += 0.01f + (newScale.y / 2f);
        Collider[] cols = Physics.OverlapBox(pos, newScale / 2, this.transform.rotation, ~LayerMask.NameToLayer("Stage"));

        foreach (Collider col in cols)
        {
            if (col.gameObject != this.gameObject)
                return false; // Hit another Object - Can't Grow
        }
        return true;
    }
    public virtual void AdjustSize()
    {
        if (IsBig)
        {
            if (this.transform.localScale != BigSize)
            {
                Vector3 newScale = Vector3.MoveTowards(this.transform.localScale, BigSize, SizeChangeSpeed);

                if (CanGrow(newScale))
                    this.transform.localScale = newScale;
                else
                    IsBig = false; // If can't grow then go back to being small
            }
        }
        else
        {
            if (this.transform.localScale != SmallSize)
            {
                this.transform.localScale = Vector3.MoveTowards(this.transform.localScale, SmallSize, SizeChangeSpeed);
            }
        }
    }
    public void Die()
    {
        deathParticles = GetComponentInChildren<ParticleSystem>();
        if(deathParticles.isPlaying == false)
            deathParticles.Play();
        GetComponentInChildren<MeshRenderer>().enabled = false;
        GameManager.Instance.RegisterAnimal(-1);
    }
}
