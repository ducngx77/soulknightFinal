using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PetMovement : MonoBehaviour
{

    [SerializeField]
    private float speed = 1.5f;
    [SerializeField]
    private float Vision = 10f;
    [SerializeField]
    private float FollowRange = 7f;

    public Animator animator;

    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isRun", false);
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);

        if (player.transform.position.x > this.transform.position.x)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        else if (player.transform.position.x < this.transform.position.x)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }

        if (distanceFromPlayer < Vision && distanceFromPlayer > FollowRange)
        {
            animator.SetBool("isRun", true);
            Vector3 moveDirection = (player.position - transform.position).normalized;
            this.transform.position += moveDirection * speed * Time.deltaTime;
        } else if (distanceFromPlayer > Vision)
        {
            //Invoke("TeleToPlayer", 1f);
            this.transform.position = player.transform.position;
        }

    }

    void TeleToPlayer() {
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Vision);
        Gizmos.DrawWireSphere(transform.position, FollowRange);
    }
}
