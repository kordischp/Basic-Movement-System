using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootContainerInteract : Interactable
{
    [SerializeField] GameObject closedChest;
    [SerializeField] GameObject openedChest;
    [SerializeField] bool opened;

    public override void Interact(Character character)
    {
        // Switch the container state from closed to open

        if (opened == false)
        {
            opened = true; 
            closedChest.SetActive(false);
            openedChest.SetActive(true);
        }
    }
}
