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
    void Update()
    {
        //RB.velocity = new Vector3(MoveSpeed * Time.deltaTime, RB.velocity.y);
        this.transform.Translate(MoveSpeed * Time.deltaTime, 0, 0);

        if (Input.GetKeyDown(KeyCode.Space))
            IsBig = !IsBig;

        if(IsBig)
        {
            if(this.transform.localScale != BigSize)
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
