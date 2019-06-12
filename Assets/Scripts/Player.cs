using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System; 

public class Player : MonoBehaviour
{
	public static Action<int> OnTreeAttackEvent;
	private float maxHealth = 100f;
	public Animator anim;

	[SerializeField] private float health;

	private void Awake()
    {
		anim = GetComponentInChildren<Animator>();
		health = maxHealth;
	}

	private void TakeDamage(float damageAmount)
	{
		health -= damageAmount;
		if(health >= 0)
		{
			Destroy(this.gameObject);
		}
	}

	public void AttackHit_AnimationEvent()
	{	
		if(OnTreeAttackEvent != null)
		{
			OnTreeAttackEvent(1);
		}
	}

	private void OnEnable()
	{
		DeathFog.DeathFogEvent += TakeDamage;
	}

	private void OnDisable()
	{
		DeathFog.DeathFogEvent -= TakeDamage;
	}
}