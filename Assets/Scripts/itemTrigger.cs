using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemTrigger : MonoBehaviour
{
    //this is a template / test script for activating items its not ment to be used.
    //If you are making an item, coppy and paste this code into a new script and replace this script in the
    //actual final product

    public GameObject itemObj;

    // calls when player colision box touches item colision box
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player1") || other.gameObject.CompareTag("Player2"))
        {
            itemObj.SetActive(false);


                                     // <--- maybe an effect when the item is collected so it dont just vanish 
                                     // <--- function call to activate item here
            Destroy(gameObject);
        }
    }
}
