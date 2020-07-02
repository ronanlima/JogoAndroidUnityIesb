using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonController : MonoBehaviour
{
    public SpriteRenderer skeletonSpriteRenderer;
    private float lastTime = 0f;
    public float timeLimit = 6f;
    private float movementAmount = 0.002f;
    private bool isMoving;
    void Start() {
        lastTime = Time.time;
        isMoving = true;
    }

    void Update() {
        if (isMoving) {
            Vector3 pos = skeletonSpriteRenderer.transform.position;
            if (Time.time - lastTime > timeLimit) {
                lastTime = Time.time;
                skeletonSpriteRenderer.flipX = !skeletonSpriteRenderer.flipX;
                movementAmount *= -1;
            }
            pos.x += movementAmount;
            skeletonSpriteRenderer.transform.position = pos;
        }
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.name == "PlayerIdle") {
            Vector3 contactPoint = col.contacts[0].point;
            Vector3 center = col.collider.bounds.center;
            bool top = contactPoint.y < center.y;
            if (top) {
                Destroy(skeletonSpriteRenderer.gameObject.GetComponent<BoxCollider2D>());
                skeletonSpriteRenderer.gameObject.GetComponent<SpriteRenderer>().sortingOrder = -1;
                // skeletonSpriteRenderer.gameObject.layer = -1;
                isMoving = false;
                skeletonSpriteRenderer.flipY = true;
                // skeletonSpriteRenderer.gameObject.GetComponent<Animator>().enabled = false;
                Vector3 pos = skeletonSpriteRenderer.transform.position;
                pos.y -= 0.35f;
                skeletonSpriteRenderer.transform.position = pos;
                skeletonSpriteRenderer.transform.rotation = Quaternion.Euler(skeletonSpriteRenderer.transform.rotation.eulerAngles.x, skeletonSpriteRenderer.transform.rotation.eulerAngles.y, skeletonSpriteRenderer.transform.rotation.z + 9f);
                // skeletonSpriteRenderer.gameObject.GetComponent<Animator>().speed = 0f;
            }
        }
    }
}