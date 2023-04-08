using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleItem : MonoBehaviour
{
    SpriteFlipbook mySpriteFlipBook;

    private bool beingDestroyed;

    private void Start() {
        mySpriteFlipBook = GetComponent<SpriteFlipbook>();
        beingDestroyed = false;
    }

    public void DestroyItem() {
        if(beingDestroyed == true) { return; }
        beingDestroyed = true;
        mySpriteFlipBook.enabled = true;
    }
}
