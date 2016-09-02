using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Hand : MonoBehaviour {

    public GameObject cube_prefab;


    private Valve.VR.EVRButtonId trigger = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
    private Valve.VR.EVRButtonId grip = Valve.VR.EVRButtonId.k_EButton_Grip;

	private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input ((int)trackedObj.index); } }
    private SteamVR_TrackedObject trackedObj;


    private HashSet<InteractableItem> touchedItems = new HashSet<InteractableItem>();

    private InteractableItem closestItem = null;
    private InteractableItem pickup = null;

	void Start () {
		trackedObj = GetComponent<SteamVR_TrackedObject> ();
	}

	void Update () {
        if (controller.GetPressDown(trigger) && touchedItems.Count > 0)
        {
            float minDistance = Mathf.Infinity;
            float distance;
            foreach (InteractableItem item in touchedItems)
            {
                distance = (item.transform.position - transform.position).sqrMagnitude;
                if (distance < minDistance)
                {
                    minDistance = distance;
                    pickup = item;
                }
            }

            pickup.Grab(this);
        }

        if (controller.GetPressUp(trigger))
        {
            pickup.Release(this);
            pickup = null;
        }



        if (controller.GetPressDown(grip))
        {
            GameObject cube = Instantiate(cube_prefab) as GameObject;
            cube.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            cube.transform.position = transform.position;
        }
    }

	void OnTriggerEnter(Collider other) {
        InteractableItem item = other.GetComponent<InteractableItem>();
        if (item)
        {
            touchedItems.Add(item);
        }
    }

    void OnTriggerExit(Collider other)
    {
        InteractableItem item = other.GetComponent<InteractableItem>();
        if (item)
        {
            touchedItems.Remove(item);
        }
    }
}
