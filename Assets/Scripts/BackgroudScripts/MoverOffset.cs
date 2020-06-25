using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverOffset : MonoBehaviour
{
    private Material currentMaterial;
    private float offsetX = 0f;
    private float offsetY = 0f;

    // Start is called before the first frame update
    void Start()
    {
        currentMaterial = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
            offsetX += 0.002f * Time.deltaTime;
            currentMaterial.SetTextureOffset("_MainTex", new Vector2(offsetX, offsetY));
    }
}
