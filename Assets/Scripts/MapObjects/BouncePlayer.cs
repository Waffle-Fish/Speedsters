using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePlayer : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] float leanth = 0.5f;

    public float BouceForce = 5f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player1" || collision.gameObject.tag == "Player2")
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * BouceForce, ForceMode2D.Impulse);
            StartCoroutine(AnimationLeanth());
        }

    }

    private IEnumerator AnimationLeanth()
    {
        anim.SetBool( "IsBouncingPlayer" ,true);
        yield return new WaitForSeconds(leanth);
        anim.SetBool("IsBouncingPlayer", false);

    }

}
