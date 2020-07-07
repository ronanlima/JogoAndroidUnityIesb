using UnityEngine.SceneManagement;
using UnityEngine;

public class SpawnInimigo : MonoBehaviour
{
    public GameObject inimigoObject;
    public Transform inimigoSpawn;// Start is called before the first frame update

    public float timeSpawn = 10;
    public int contTimeSpawn = 5; 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timeSpawn<=0){
            GerarInimigo();
            timeSpawn = 10f;
        }else{
            timeSpawn -= 1 * Time.deltaTime;
        }

        Debug.Log(timeSpawn);
    }

    void GerarInimigo(){
        GameObject zumb = Instantiate(inimigoObject, inimigoSpawn.position, inimigoSpawn.rotation);
    }
}
