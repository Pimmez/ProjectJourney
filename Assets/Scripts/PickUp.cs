using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PickUp : MonoBehaviour
{
	public static Action<int, int> PickUpEvent;
	public LayerMask playerMask;
	private bool isPicked = false;

    // Update is called once per frame
    void Update()
    {
		isPicked = Physics.CheckSphere(gameObject.transform.position, 1f, playerMask, QueryTriggerInteraction.Ignore);
		if (isPicked)
		{
			Destroy(this.gameObject);
			if(PickUpEvent != null)
			{
				PickUpEvent(1, 0);
			}
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(this.gameObject.transform.position, 1f);
	}
}
