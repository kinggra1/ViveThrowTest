using UnityEngine;
using System.Collections;

public class Sweepable : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision col)
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        Debug.Log("Collision with " + col.gameObject.name + " " + col.gameObject.tag);

        if (rb && col.gameObject.tag == "BroomHead")
        {
            Debug.Log(col.impulse);
            rb.AddForceAtPosition(col.impulse * 10f, col.contacts[0].point, ForceMode.Impulse);
        }
    }
}
