using UnityEngine;
using UnityEngine.SceneManagement;

public class SeguirPlayer : MonoBehaviour
{


    private Animator inimigoAnimator;
    public Transform localPlayer;

    private SpriteRenderer renderInimigo;
    private float ultimoMovimento;
    public float velocidade;

    public int lifeInimigo = 100;
    public float distAtack = 5f;
    public float distPerseguicao = 16f;
    public int danoTiro = 50;

    private bool isDead = false;

    public bool isMonster = false;

    // public GameObject youWin;


    void Start()
    {
        renderInimigo = GetComponent<SpriteRenderer>();
        inimigoAnimator = GetComponent<Animator>();
        // youWin.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        if(isDead){return;}

        float distancia = Vector2.Distance(transform.position, localPlayer.position);

        if(distancia > distAtack && distancia<distPerseguicao){
            transform.position = Vector2.MoveTowards(transform.position, localPlayer.position, velocidade * Time.deltaTime);
            inimigoAnimator.SetBool("isWalking", true);

            inimigoAnimator.SetBool("isAtack", (distancia<distAtack));

            if (!isMonster) {
                renderInimigo.flipX = transform.position.x < ultimoMovimento? true: false;
            }

            ultimoMovimento = transform.position.x;
        }else{
            inimigoAnimator.SetBool("isWalking", false);
        }

        inimigoAnimator.SetBool("isAtack", (distancia<distAtack));

        if(lifeInimigo<=0){
            InimigoDead();
        }

    }

     private void OnTriggerEnter2D(Collider2D collider){

        Debug.Log("Inimigo:");
        if(collider.gameObject.CompareTag("TiroTag")){
            lifeInimigo -= danoTiro;
        }
    }

    private void OnCollisionEnter2D(Collision2D col) {
        Debug.Log(col.gameObject.name);

        if (col.gameObject.layer == 10) {
            lifeInimigo = 0;
            InimigoDead();
        }
    }

    void InimigoDead() {
        isDead = true;
        inimigoAnimator.SetTrigger("tgrDead");
        Destroy(gameObject, 2f);
        

        Scene scene = SceneManager.GetActiveScene();
        /*if (scene.name.Equals("Fase003_01")) {
            SceneManager.LoadScene("MenuScene");
            return;
        }*/

        // if (scene.name.Equals("Fase003_01")) {
        //     youWin.gameObject.SetActive(true);
        // }
        

    }
}

