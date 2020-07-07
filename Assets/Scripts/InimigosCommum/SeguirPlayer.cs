using UnityEngine;

public class SeguirPlayer : MonoBehaviour
{

    private Animator inimigoAnimator;
    public Transform localPlayer;
    public float velocidade;

    void Start()
    {
        inimigoAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distancia = Vector2.Distance(transform.position, localPlayer.position);

        if(distancia > 5f && distancia<14){
            transform.position = Vector2.MoveTowards(transform.position, localPlayer.position, velocidade * Time.deltaTime);
            inimigoAnimator.SetBool("isWalking", true);

            inimigoAnimator.SetBool("isAtack", (distancia<5f));

        }else{
            inimigoAnimator.SetBool("isWalking", false);
        }

        inimigoAnimator.SetBool("isAtack", (distancia<5f));
    }
}
