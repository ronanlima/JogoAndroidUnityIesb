using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonController : MonoBehaviour
{
    private SpriteRenderer skeletonSpriteRenderer;
    
    void Start()
    {
        skeletonSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.name == "PlayerIdle") {
            Vector3 contactPoint = col.contacts[0].point;
            Vector3 center = col.collider.bounds.center;
            bool top = contactPoint.y < center.y;
            if (top) {
                skeletonSpriteRenderer.flipY = true;
                skeletonSpriteRenderer.gameObject.GetComponent<Animator>().speed = 0f;
            }
        }
    }
}