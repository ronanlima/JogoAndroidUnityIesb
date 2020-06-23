using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{

    //Propriedades de controle
    public float moveSpeed =10f;
    public float jumpForce = 0.5f;
    public float slideForce = 6;
    public int life = 100;
    private bool isJump = false;
    private bool isDead = false;
    private float inJump = 0;
    private float inSlide = 0;


    //Recursos usados no GameObject
    private SpriteRenderer playerSpriteRenderer;
    private Rigidbody2D playerRigidBody;
    private Animator playerAnimator;

    // Start is called before the first frame update
    void Start() {
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
            Mover();
            Jump();
            Slider();
    }

    bool playerDead() {
        Console.Write("playerDead");
        if (life < 1) {
            Console.Write("playerDead < 1");
            isDead = true;
            playerAnimator.SetBool("tgrDead", isDead);
        }
        if (isDead) {
            Console.Write("playerDead - está morto");
            return true;
        }
        return false;
    }

    void Mover() {
        Console.Write("Mover");
        if (playerDead()) {
            return;
        }
        float cmd = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(cmd, 0f, 0f);
        transform.position += movement * Time.deltaTime * moveSpeed;

        playerAnimator.SetBool("isRun", cmd!=0);

        if(cmd > 0 ){
            playerSpriteRenderer.flipX = false;
        }

        if(cmd < 0 ){
            playerSpriteRenderer.flipX = true;
        }

    }

    void Jump() {
        Console.Write("Jump");
        if (playerDead()) {
            return;
        }
        if(Input.GetButtonDown("Jump") && inJump <= 0){
            playerRigidBody.AddForce(
                new Vector2(0f, jumpForce),
                ForceMode2D.Impulse
            );
            isJump = true;
            playerAnimator.SetBool("isJump", isJump);
            inJump = jumpForce;
        }

        if(inJump <= 0){
             isJump = false;
             playerAnimator.SetBool("isJump", isJump);
        }

        if(inJump>0){
            inJump -= Time.deltaTime *(jumpForce/2);
        }
    }

    void Slider() {
        Console.Write("Slider");
        if (playerDead()) {
            return;
        }
        if(Input.GetButtonDown("Fire1") && inSlide <= 0 && !isJump){
            playerAnimator.SetBool("isSlide", true);
            inSlide = slideForce;
        }

        if(inSlide <= 0){
             playerAnimator.SetBool("isSlide", false);
        }

        if(inSlide>0){
            float direction = (playerSpriteRenderer.flipX)?-1f:1f;
            inSlide -= 1.7f * Time.deltaTime * slideForce;
            Vector3 movement = new Vector3(direction, 0f, 0f);
            transform.position += movement * Time.deltaTime * slideForce;
        }
    }
}
