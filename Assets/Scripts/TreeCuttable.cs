using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeCuttable : ToolHit
{
    [SerializeField] GameObject pickUpDrop;
    [SerializeField] int dropCount = 5;
    [SerializeField] float spread = 0.7f;

    public override void Hit()
    {
        while(dropCount > 0)
        {
            dropCount -= 1;

            // Calculate the position of dropped logs
            Vector3 position = transform.position;
            position.x += spread * UnityEngine.Random.value - spread / 2; //Randomize position to +-0.5spead
            position.y += spread * UnityEngine.Random.value - spread / 2;
            GameObject go = Instantiate(pickUpDrop);
            go.transform.position = position;
        }

        Destroy(gameObject);
    }
}
