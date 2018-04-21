using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineController : MonoBehaviour {

    // For when randomizing tennis ball shot locations
    // Make sure it does not shoot pass these angles
    // Reference will be object "PlayerBlockers"
    [SerializeField] float MaxLeftAngle;
    [SerializeField] float MaxRightAngle;

    [SerializeField] float MachineRotationSpeed = 1f;
    [SerializeField] float ProjectileSpeed = 150f;

    [SerializeField] GameObject MachinesTarget; // What Machine will rotate towards
    [SerializeField] GameObject Projectile;
    [SerializeField] Transform ProjectileSpawn;

    [SerializeField] float ProjectileSpawnTime = 1f;

    Transform PlayerTransform;

    // Use this for initialization
    void Start () {
        PlayerTransform = MachinesTarget.transform;
        StartCoroutine(ShootTheProjectile());
    }
	
	// Update is called once per frame
	void Update () {
        RotateTowardsPlayer();
    }

    IEnumerator ShootTheProjectile()
    {
        while(true)
        {
            ShootProjectile();
            yield return new WaitForSeconds(ProjectileSpawnTime);
        }
    }

    private void RotateTowardsPlayer()
    {
        Vector3 targetPostition = new Vector3(PlayerTransform.position.x,
                                       this.transform.position.y,
                                       PlayerTransform.position.z);
        this.transform.LookAt(targetPostition);
    }

    private void ShootProjectile()
    {
        Vector3 newDirection = GetPlayerRotationAngle();
        var projectile = (GameObject)Instantiate(Projectile, ProjectileSpawn.position, Quaternion.LookRotation(newDirection));
        projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * ProjectileSpeed);

        // TODO: Change destruction period
        Destroy(projectile, 20.0f);
    }

    private Vector3 GetPlayerRotationAngle()
    {
        Vector3 targetDirection = transform.position - new Vector3(PlayerTransform.position.x, 0, 0); 
        float step = MachineRotationSpeed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, step, 0.0f);

        return newDirection;
    }
}
