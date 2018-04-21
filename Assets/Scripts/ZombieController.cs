using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour {

    [SerializeField] float ZombieRotationSpeed = 500f;
    [SerializeField] float WalkingSpeed = 2f;

    [SerializeField] GameObject ZombiesTarget; // What Zombie will rotate towards

    Transform PlayerTransform;

    private void Awake()
    {
        ZombiesTarget = GameObject.Find("/Player");
    }

    // Use this for initialization
    void Start () {
        PlayerTransform = ZombiesTarget.transform;
        //ZombiesTarget = GameObject.Find("/Player");
        print(ZombiesTarget);
    }
	
	// Update is called once per frame
	void Update ()
    {
        MoveForward();
        //RotateTowardsPlayer();
        //MoveTowardsPlayer();
    }

    private void RotateTowardsPlayer() {
        Vector3 targetDir = PlayerTransform.position - transform.position;
        float step = WalkingSpeed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    private void MoveForward() {
        float step = WalkingSpeed * Time.deltaTime;

        transform.position += -Vector3.forward * step;
    }

    private void MoveTowardsPlayer() {
        float step = WalkingSpeed * Time.deltaTime;
        Vector3 targetPosition = new Vector3(PlayerTransform.position.x,
                                       this.transform.position.y,
                                       PlayerTransform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
    }
}
