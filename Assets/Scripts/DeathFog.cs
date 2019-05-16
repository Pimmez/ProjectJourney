using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DeathFog : MonoBehaviour
{
	public static Action<float> DeathFogEvent;

	[SerializeField] private float damageRate = 3;
	[SerializeField] float damageAmount = 1f;
	private int ticks = 0;
	private int ticksPerSec = 60;

	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			ApplyDamageOverTime();
		}
	}

	private void ApplyDamageOverTime()
	{
		// ticks increments 60 times per second, as an example
		ticks++;
		// Condition is true once every second
		if (ticks % (ticksPerSec * damageRate) == 0)
		{
			// Apply damage
			if (DeathFogEvent != null)
			{
				DeathFogEvent(damageAmount);
			}
		}
	}
}