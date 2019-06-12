using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;

public class InteractionBehaviour : MonoBehaviour
{
	private Animator anim;

	public static Action TreeEventHit;
	[SerializeField] private NavMeshAgent agent;

	private bool inRadius = false;

	private void Start()
	{
		anim = GetComponentInChildren<Animator>();
		agent = GetComponent<NavMeshAgent>();

	}

	private void Update()
	{
		anim.SetFloat("Movement", agent.velocity.magnitude / agent.speed);

		if (Input.GetMouseButtonDown(0))
		{
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			
			if (Physics.Raycast(ray, out hit))
			{
				if (hit.collider.tag == Tags.Ground)
				{
					agent.destination = hit.point;
					agent.isStopped = false;


				}	
				if(hit.collider.tag == Tags.Tree)
				{
					agent.destination = hit.point;
				}
				if (hit.collider.tag == Tags.Wood)
				{
					agent.destination = hit.point;
				}

			}
		}
	}
}