using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleItem : MonoBehaviour
{
    [SerializeField] GameObject objBehindBox;


    SpriteFlipbook mySpriteFlipBook;

    private bool beingDestroyed;

    private void Start() {
        mySpriteFlipBook = GetComponent<SpriteFlipbook>();
        beingDestroyed = false;
    }

    public void DestroyItem() {
        if(beingDestroyed == true) { return; }
        beingDestroyed = true;
        if (objBehindBox != null) {
            Vector2 centerPoint = new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y));
            Instantiate(objBehindBox, centerPoint, Quaternion.identity);
        }
        mySpriteFlipBook.enabled = true;
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Explosion") {
            DestroyItem();
        }
    }
}
