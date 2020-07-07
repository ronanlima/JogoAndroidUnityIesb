using UnityEngine;

public class PlayerTiro : MonoBehaviour
{
    // Start is called before the first frame update
    private float tiroSpeed = 15f;
    private float tiroDestroy = 1.5f;
    void Start()
    {
        Destroy(gameObject, tiroDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * tiroSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collider){

        if(collider.gameObject.CompareTag("InimigoTag")){
            Destroy(gameObject);
        }
    }
}