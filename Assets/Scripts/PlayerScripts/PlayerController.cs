using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    //Propriedades de controle
    public float moveSpeed =10f;
    public float jumpForce = 6f;
    public float slideForce = 6;
    public int life = 100;
    private bool isJump = false;
    private bool isDead = false;
    private float inSlide = 0;

    //Recursos usados no GameObject
    private SpriteRenderer playerSpriteRenderer;
    private Rigidbody2D playerRigidBody;
    private Animator playerAnimator;
    public SpriteRenderer skeletonSpriteRenderer;

    void Start() {
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    void Update() {
        Mover();
        Jump();
        Slider();
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name.Equals("InitScene")) {
            StayInsideScene();
        }
    }

    void StayInsideScene() {
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -8.62f, 8.62f),
            Mathf.Clamp(transform.position.y, -10f, 4.4f));
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.layer == 8) {
            setJumpFalse();
        }
        if (col.gameObject.layer == 9)
        {
            life = 0;
            playerAnimator.SetTrigger("tgrDead");
            SceneManager.LoadScene("GameOver");
        }
        if (col.gameObject.name == "Skeleton") {
            Vector3 contactPoint = col.contacts[0].point;
            Vector3 center = col.collider.bounds.center;
            bool top = contactPoint.y > center.y;
            if (top) {
                setJumpFalse();
            }
        }
    }

    void setJumpFalse() {
        isJump = false;
        playerAnimator.SetBool("isJump", isJump);
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
        if(Input.GetButtonDown("Jump") && !isJump){
            playerRigidBody.AddForce(
                new Vector2(0f, jumpForce),
                ForceMode2D.Impulse
            );
            isJump = true;
            playerAnimator.SetBool("isJump", isJump);
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
