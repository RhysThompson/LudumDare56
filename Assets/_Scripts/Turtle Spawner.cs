using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleSpawner : MonoBehaviour
{
    public GameObject TurtlePrefabBig;
    public GameObject TurtlePrefabSmall;
    private void OnEnable()
    {
        StartCoroutine("SpawnTurtles");
    }
    public IEnumerator SpawnTurtles()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            int rand = Random.Range(0, 2);
            if(rand == 0)
                Instantiate(TurtlePrefabBig, this.transform.position, Quaternion.identity);
            else if (rand == 1)
                Instantiate(TurtlePrefabSmall, this.transform.position, Quaternion.identity);
        }
        
    }
    private void OnDisable()
    {
        StopCoroutine("SpawnTurtles");
    }
}
