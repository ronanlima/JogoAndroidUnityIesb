using UnityEngine.SceneManagement;
using UnityEngine;

public class SpawnInimigo : MonoBehaviour
{
    public GameObject inimigoObject;

    public float timeSpawn = 10;
    public int delaySpawn = 5; 
    void Start()
    {
        InvokeRepeating ("GerarInimigo", delaySpawn, timeSpawn);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GerarInimigo(){
        Instantiate (inimigoObject, transform.position, transform.rotation);
        Debug.Log("Gerando");
    }
}
