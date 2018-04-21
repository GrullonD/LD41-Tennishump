using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour {

    [SerializeField] GameObject Zombie;
    [SerializeField] GameObject SpawnArea;

    [SerializeField] float ZombieSpawnTime = 2f;

    [SerializeField] GameObject SpawnFL;
    [SerializeField] GameObject SpawnRL;
    [SerializeField] GameObject SpawnFR;
    [SerializeField] GameObject SpawnRR;

    Transform SpawnAreaScale;

    private void Awake()
    {
        SpawnFL = GameObject.Find("FrontLeft");
        SpawnRL = GameObject.Find("BackLeft");
        SpawnFR = GameObject.Find("FrontRight");
        SpawnRR = GameObject.Find("BackRight");
    }

    // Use this for initialization
    void Start () {
        SpawnAreaScale = SpawnArea.transform;
        StartCoroutine(SpawnTheZombies());
    }
	
	// Update is called once per frame
	void Update () {
    }

    IEnumerator SpawnTheZombies()
    {
        while (true)
        {
            SpawnZombie();
            yield return new WaitForSeconds(ZombieSpawnTime);
        }
    }

    private void SpawnZombie() {
        Vector3 SpawnPoint = new Vector3(Random.Range(SpawnFL.transform.position.x,SpawnFR.transform.position.x), 
                                        0.75f, 
                                        Random.Range(SpawnFL.transform.position.z,SpawnRL.transform.position.z));
        var zombie = (GameObject)Instantiate(Zombie, SpawnPoint, new Quaternion(0f,-180f,0f,0f));
    }
}
