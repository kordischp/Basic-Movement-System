using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    Transform player;
    [SerializeField] float speed = 5f; // Speed of moving towards player
    [SerializeField] float pickUpDistance = 1.5f;
    [SerializeField] float ttl = 10f; // Time to live

    // Store the reference to the transform of the player character by calling singleton
    private void Awake()
    {
        player = GameManager.instance.player.transform; 
    }

    private void Update()
    {
        ttl -= Time.deltaTime;
        if(ttl < 0) { Destroy(gameObject); } // This two lines: if timer goes to 0, object vanishes

        float distance = Vector3.Distance(transform.position, player.position); // Distance between the player and object
        if (distance > pickUpDistance) // Check if the player is inside the pickup distance, if not return out of update
            // If this statement is true, everything below will not be executed - guard clause
        {
            return;
        }

        transform.position = Vector3.MoveTowards(
            transform.position,
            player.position,
            speed * Time.deltaTime
            );

        if(distance < 0.1f)
        {
            Destroy(gameObject);
        }
    }

}
