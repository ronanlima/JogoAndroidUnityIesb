using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed =10f;
    public float jumpForce = 6f;
    public float slideForce = 6;
    public int life = 100;
    private bool isJump = false;
    private bool isDead = false;
    private float inSlide = 0;
    private int quantItens = 4;
    private int quantCollected = 0;
    public SpriteRenderer door;
    public Sprite openedDoor;
    public Sprite closedDoor;
    private SpriteRenderer playerSpriteRenderer;
    private Rigidbody2D playerRigidBody;
    private Animator playerAnimator;
    public SpriteRenderer skeletonSpriteRenderer;
    private int countSkeleton =  0;
    public SpriteRenderer monster;
    private int totalSkeleton = 7;
    private bool isInitScene = false;

    public GameObject tiroObjeto;
    public Transform tiroSpawn;
    public float tiroIntervalo = 0.05f;
    private float tiroProximo = 0;

    void Start() {
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        monster.gameObject.SetActive(false);
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name.Equals("InitScene")) {
            isInitScene = true;
            monster.gameObject.SetActive(true);
        }
    }

    void Update() {
        Mover();
        Jump();
        Slider();
        Atirar();
        if (isInitScene) {
            StayInsideScene();
            isInitScene = true;
        } else {
            isInitScene = false;
        }
    }

    void StayInsideScene() {
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -8.62f, 8.62f),
            Mathf.Clamp(transform.position.y, -10f, 4.4f));
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.CompareTag("Collectable")) {
            quantCollected++;
            collider.gameObject.SetActive(false);
        } else if (collider.gameObject.CompareTag("PointNextScene") && quantCollected == quantItens) {
            SceneManager.LoadScene("Fase001_01");
            return;
        }
        if (quantCollected == quantItens) {
            door.sprite = openedDoor;
        }
    }

    void OnCollisionEnter2D(Collision2D col) {

        if (col.gameObject.layer == 8) {
            setJumpFalse();
        }
        if (col.gameObject.layer == 9) {
            life = 0;
            playerDead();
            
        }
        if (col.gameObject.layer == 10)
        {
            life = 0;
            playerDead();

        }
        if (col.gameObject.layer == 11)
        {
            life = 0;
            playerDead();
        }
        if (col.gameObject.name == "Skeleton") {
            Collision2DSideType collisionSide = col.GetContactSide();
            if (collisionSide == Collision2DSideType.Right || collisionSide == Collision2DSideType.Left) {
                life = 0;
                playerDead();
            } else {
                setJumpFalse();
                this.countSkeleton += 1;
            }
        }
        if (col.gameObject.name == "SkeletonVertical") {
            Collision2DSideType collisionSide = col.GetContactSide();
            if (collisionSide == Collision2DSideType.Bottom) {
                life = 0;
                playerDead();
            }
        }

        if(monster != null && monster.gameObject != null) {
            if (!isInitScene) {
                if (countSkeleton < totalSkeleton) {
                    monster.gameObject.SetActive(false);
                } else {
                    monster.gameObject.SetActive(true);
                }
            } else {
                monster.gameObject.SetActive(true);
            }
        }
    }

    void setJumpFalse() {
        isJump = false;
        playerAnimator.SetBool("isJump", isJump);
    }

    bool playerDead() {
        if (life < 1) {
            isDead = true;
            playerAnimator.SetBool("tgrDead", isDead);
        }
        if (isDead) {
            if (door != null) {
                door.sprite = closedDoor;
            }
            Destroy(playerSpriteRenderer.gameObject.GetComponent<CapsuleCollider2D>());
            Destroy(playerSpriteRenderer.gameObject.GetComponent<Rigidbody2D>());
            playerSpriteRenderer.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 0;
            // SceneManager.LoadScene("GameOver"); Deixar comentado até criarmos um contador. Esse trecho não deixa nem executar a animação de morrer, do player
            return true;
        }
        return false;
    }

    void Mover() {
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

    void Atirar()
    {
        if (playerDead()) {
            return;
        }

        if(Input.GetButtonDown("Fire2") && !isJump ){
            tiroProximo = tiroIntervalo + Time.time;

            playerAnimator.SetBool("isShoot", true);

            GameObject tiro = Instantiate(tiroObjeto, tiroSpawn.position, tiroSpawn.rotation);

            if(playerSpriteRenderer.flipX){
                tiroSpawn.position = new Vector3(this.transform.position.x - 1.7f, this.transform.position.y - 0.41f, this.transform.position.z);
                tiro.transform.eulerAngles = new Vector3(0, 0, 180);
            }else{
                tiroSpawn.position = new Vector3(this.transform.position.x + 1.7f, this.transform.position.y - 0.41f, this.transform.position.z);
            }
        }

        if(Time.time > tiroProximo){
             playerAnimator.SetBool("isShoot", false);
        }
    }
}
