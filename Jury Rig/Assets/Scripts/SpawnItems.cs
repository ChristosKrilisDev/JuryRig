using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItems : MonoBehaviour
{


    public Transform[] pos;
    [Range(0 , 100)] public float randomness = 50;

    [Space]
    public GameObject itemPrefab;

    private bool flag = false;

    void Start()
    {

    }

    void Update()
    {
        if(!flag)
            StartCoroutine(Spawn());
    }


    IEnumerator Spawn()
    {
        flag = true;

        while(true)
        {
            GenerateItems();

            float randT = Random.Range(1 , 3);
            yield return new WaitForSeconds(randT);
        }



    }
    void GenerateItems()
    {
        int rand = Random.Range(0 , 100);

        if(rand > randomness)
        {

            int randX = Random.Range(0 , pos.Length);
            //Spawn
            GameObject item = Instantiate(itemPrefab);
            item.transform.localPosition = pos[randX].transform.position;
            //Debug.Log("spawn");
        }


    }
}
