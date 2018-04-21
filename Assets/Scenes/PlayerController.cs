using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField] float PlayerSpeed = 1f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        GetInput();
	}

    private void GetInput()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * PlayerSpeed * Time.deltaTime);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * PlayerSpeed * Time.deltaTime);
        }
    }
}
