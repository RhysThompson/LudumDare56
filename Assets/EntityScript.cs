using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityScript : MonoBehaviour
{
    public bool IsBig = true;
    public Vector3 BigSize;
    public Vector3 SmallSize;
    public float SizeChangeSpeed = 0.1f;
    public float MoveSpeed = 10f;
    
    private Rigidbody RB;
    // Start is called before the first frame update
    void Start()
    {
        RB = this.GetComponent<Rigidbody>();
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
            if(!IsBig && !CanGrow())
                return;

            IsBig = !IsBig;
        }
    }

    public virtual void Move()
    {
        RB.MovePosition(this.transform.position + new Vector3(MoveSpeed * Time.fixedDeltaTime, RB.velocity.y, 0));
    }

    public virtual bool CanGrow()
    {
        Vector3 pos = this.transform.position;
        pos.y += 0.01f + (BigSize.y/ 2f);
        Collider[] cols = Physics.OverlapBox(pos, BigSize / 2, this.transform.rotation);

        foreach (Collider col in cols)
        {
            if (col.gameObject != this.gameObject)
            {
                return false; // Hit another Object - Can't Grow
            }
        }
        return true;
    }
    public virtual void AdjustSize()
    {
        if (IsBig)
        {
            if (this.transform.localScale != BigSize)
            {
                this.transform.localScale = Vector3.MoveTowards(this.transform.localScale, BigSize, SizeChangeSpeed);
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
}
