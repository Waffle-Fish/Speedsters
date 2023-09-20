using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // calls when player colision box touches item colision box
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player1") || other.gameObject.CompareTag("Player2"))
        {
            Destroy(gameObject);
                                 // <--- maybe an effect when the item is collected so it dont just vanish 
                                 // <--- function call to activate item here
        }
    }
}
