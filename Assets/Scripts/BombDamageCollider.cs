﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Checks if the player is within range and triggers damage
/// </summary>
public class BombDamageCollider : MonoBehaviour
{
    /// <summary>
    /// A reference to the parent bomb
    /// </summary>
    [SerializeField]
    BombTile bomb;

    /// <summary>
    /// Stores a reference to the parent bomb
    /// </summary>
    void Start()
    {
        this.bomb = GetComponentInParent<BombTile>();
    }

  
    /// <summary>
    /// If the parent is "active" then we want to trigger the bomb as the player
    /// has stepped into the bomb's range
    /// </summary>
    /// <param name="other"></param>
	void OnTriggerStay(Collider other)
    {
        // Wait until active
        if(!this.bomb.IsActive || this.bomb.isTriggered) {
            return;
        }
        
        if(other.tag == "Player") {
            this.bomb.isTriggered = true;
            Player player = other.GetComponent<Player>();
            player.TakeDamage();
        }
    }
}
