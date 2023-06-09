using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PickupThing : MonoBehaviour
{
    public bool isCollectableToInv;
    public bool isCarryable;
    public bool isRunOverPickUp;

    private GameObject player;
    public bool isScoreItem;
    public int itemScore;

    public GameObject levelManagerObj;
    public LevelManager levelManager;

    [Header("Events")]
    public GameEvent onThingAcquired;
    



    // NOTE - You MUST have an event assigned here if you want to delete up the object, even if you don't need one, because the code will exit on a null event.

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        levelManagerObj = GameObject.FindGameObjectWithTag("LevelManager");
        levelManager = levelManagerObj.GetComponent<LevelManager>();   
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && isRunOverPickUp)
        {
            if (isScoreItem)
            {
                AddScoreFromPickup();
            }

            CollectionEvent();
            Destroy(gameObject);

            if (this.GetComponent<InvItemID>() != null)
            {
                GameObject itemToGrab = this.gameObject;
                player.GetComponent<BasicInteract>().RunoverPickup(itemToGrab); // ADD TO INVENTORY LIST
            }

        }
    }

    public void RemoveObject()
    {
        Destroy(gameObject);
    }

    public void CollectionEvent()
    {
        onThingAcquired.Raise(this, null);
    }

    public void AddScoreFromPickup()
    {
        levelManager.score += itemScore;
    }
}
