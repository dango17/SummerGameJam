using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] float time = 5f;

    public GameObject[] crowd;

    private Vector3 spawnPosition = Vector3.zero;

	private void Awake() {
        spawnPosition = GetComponentInChildren<Transform>().position;
	}

	// Start is called before the first frame update
	void Start()
    {
        StartCoroutine(theSpawner());
    }

    IEnumerator theSpawner()
    {
        Instantiate(crowd[Random.Range(0, crowd.Length)], spawnPosition, Quaternion.identity);

        yield return new WaitForSeconds(time);
        StartCoroutine(theSpawner());
    }
}
