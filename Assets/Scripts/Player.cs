using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	private float maxHealth = 100f;

	[SerializeField] private float health;

    // Start is called before the first frame update
    private void Awake()
    {
		health = maxHealth;
    }

    // Update is called once per frame
	private void Update()
    {
    
    }

	private void TakeDamage(float damageAmount)
	{
		health -= damageAmount;
		if(health >= 0)
		{
			Destroy(this.gameObject);
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
