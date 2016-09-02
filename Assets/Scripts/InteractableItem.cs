using UnityEngine;
using System.Collections;

public class InteractableItem : MonoBehaviour {

    private Rigidbody rb;
    private Hand holdingHand;

    private Vector3 lastPos;
    private Quaternion lastRot;
    private float angle;
    private Vector3 axis;

    public float velocity_factor = 100f;
    public float rotation_factor = 100f;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();

        lastPos = transform.position;
        lastRot = transform.rotation;
	}
	
	// Update is called once per frame
	void LateUpdate () {
	    if (isHeld())
        {
            lastPos = transform.position;
            lastRot = transform.rotation;
        }
	}




    public void Grab(Hand hand)
    {
        holdingHand = hand;
        transform.parent = hand.transform;
        rb.isKinematic = true;
    }

    public void Release(Hand hand)
    {
        rb.isKinematic = false;

        holdingHand = null;
        transform.parent = null;

        rb.velocity = (transform.position - lastPos) * velocity_factor;
        (transform.rotation * Quaternion.Inverse(lastRot)).ToAngleAxis(out angle, out axis);
        rb.angularVelocity = (angle * axis * rotation_factor);

    }

    public bool isHeld()
    {
        return holdingHand != null;
    }

}
