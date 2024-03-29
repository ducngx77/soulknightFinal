using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PigHitBox : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private int damage = 10;
    [SerializeField]
    public Animator animator;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player" && animator.GetBool("isCharging") == false)
        {
            PlayerHealth playerhealth = collider.GetComponent<PlayerHealth>();
            playerhealth.Damage(damage);
            animator.SetBool("isCharging", true);
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player" && animator.GetBool("isCharging") == false)
        {
            Invoke("turnOffHitBox", 2f);
        }
    }

    void turnOffHitBox()
    {
        
        gameObject.SetActive(false);
    }
}
