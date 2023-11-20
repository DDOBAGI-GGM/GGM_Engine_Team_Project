using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticle : MonoBehaviour
{
    [SerializeField] ParticleSystem movementParticle;

    [Range(0f, 10f)]
    [SerializeField] float velocity;

    [Range(0, 0.2f)]
    [SerializeField] float period;

    [SerializeField] GameObject player;
    private Rigidbody2D playerBody;
    private PlayerMovement playermovement;

    float counter;

    private void Start()
    {
        if (player != null)
        {
            playerBody = player.GetComponent<Rigidbody2D>();
            playermovement = player.GetComponent<PlayerMovement>();
        }
    }

    private void Update()
    {
        counter += Time.deltaTime;

        if (playermovement.Is_onGround && Mathf.Abs(playerBody.velocity.x) > velocity)
        {
            if (counter > period)
            {
                movementParticle.Play();
                counter = 0;
            }
        }
    }
}
