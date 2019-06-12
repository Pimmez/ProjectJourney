using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventReceiver : MonoBehaviour
{
	public Player player;

    public void TreeHitEvent()
	{
		player.AttackHit_AnimationEvent();
	}
}
