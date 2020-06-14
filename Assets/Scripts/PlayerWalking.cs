using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalking : MonoBehaviour {

    public SpriteRenderer rendererRef;

    // Start is called before the first frame update
    void Start()
    {
        // renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        if (Input.GetKey(KeyCode.RightArrow)) {
            pos.x += 0.1f;
            rendererRef.flipX = false;
        } else if (Input.GetKey(KeyCode.LeftArrow)) {
            pos.x += -0.1f;
            rendererRef.flipX = true;
        } else if (Input.GetKey(KeyCode.UpArrow)) {
            pos.y += 0.1f;
        }
        
        transform.position = pos;
    }
}
