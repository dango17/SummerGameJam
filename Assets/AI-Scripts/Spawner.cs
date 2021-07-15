using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] float time = 5f;

    public GameObject[] crowd;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(theSpawner());
    }

    IEnumerator theSpawner()
    {
        Vector3 pos = GameObject.Find("Spawn").transform.position;

        Instantiate(crowd[Random.Range(0, crowd.Length - 1)], pos, Quaternion.identity);

        yield return new WaitForSeconds(time);
        StartCoroutine(theSpawner());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
