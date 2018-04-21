using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    [SerializeField] bool active = false;
    public bool Active
    {
        get
        {
            return active;
        }

        set
        {
            active = value;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "BallPassive":
                break;
            case "BallActive":
                Destroy(this.gameObject);
                break;
            default:
                break;
        }
    }
}
