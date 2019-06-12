using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class TreeBehaviour : MonoBehaviour
{
	[SerializeField] private float fallingSpeed = 2f;
	private enum States { Idle, Falling, Destroyed };
	private States myStates;

	public GenericLootDropTableGameObject lootDropTable;
	public int numItemsToDrop;
	public Player player;
	public LayerMask playerMask;
	private bool isChopping;

	private float hitPoints;
	private float maxHitPoints = 2;

	private void Start()
	{
		myStates = States.Idle;
		hitPoints = maxHitPoints;
	}

	private void Update()
    {
		if(myStates == States.Idle)
		{
			isChopping = Physics.CheckSphere(gameObject.transform.position - new Vector3(0, -3f, 0), 2f, playerMask, QueryTriggerInteraction.Ignore);
			if (isChopping)
			{
				player.anim.SetBool("isChopping", true);


				if (hitPoints <= 0)
				{
					player.anim.SetBool("isChopping", false);
					isChopping = false;
					myStates = States.Falling;
				}	
			}
			else
			{
				isChopping = false;
				player.anim.SetBool("isChopping", false);
			}
		}

		if (myStates == States.Falling)
		{
			gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation.normalized, Quaternion.Euler(90, 0, 0).normalized, fallingSpeed * Time.deltaTime);
			StartCoroutine("NextState");
		}

		if (myStates == States.Destroyed)
		{
			Destroy(this.gameObject);
			DropLootNearTree(numItemsToDrop);
		}	
	}

	

	IEnumerator NextState()
	{
		yield return new WaitForSeconds(2f);
		myStates = States.Destroyed;
	}

	/// <summary>
	/// Spawning objects in horizontal line
	/// </summary>
	/// <param name="numItemsToDrop"></param>
	void DropLootNearTree(int numItemsToDrop)
	{
		for (int i = 0; i < numItemsToDrop; i++)
		{
			GenericLootDropItemGameObject selectedItem = lootDropTable.PickLootDropItem();
			GameObject selectedItemGameObject = Instantiate(selectedItem.item);
			selectedItemGameObject.transform.position = gameObject.transform.position + new Vector3(UnityEngine.Random.Range(-2, 2), 0.5f, UnityEngine.Random.Range(-2, 2));
		}
	}

	private void DamageTree(int damage)
	{
		hitPoints -= damage;
	}

	private void OnValidate()
	{
		// Validate table and notify the programmer / designer if something went wrong.
		lootDropTable.ValidateTable();

	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(this.gameObject.transform.position, 2f);
	}

	private void OnEnable()
	{
		Player.OnTreeAttackEvent += DamageTree;
	}

	private void OnDisable()
	{
		Player.OnTreeAttackEvent -= DamageTree;
	}
}