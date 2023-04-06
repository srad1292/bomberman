using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    [SerializeField] float spriteDuration = 0.2f;
    [SerializeField] bool doReverse = true;

    SpriteRenderer mySpriteRenderer;

    int spriteIndex = 0;
    float counter = 0f;
    bool goingInReverse = false;


    private void Start() {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(sprites.Length == 0 || mySpriteRenderer == null) {
            print("Explosion: Missing sprites or renderer");
            Destroy(gameObject);
        }
        counter += Time.deltaTime;
        if(counter >= spriteDuration) {
            if (goingInReverse) {
                if(spriteIndex - 1 > 0) {
                    spriteIndex--;
                    mySpriteRenderer.sprite = sprites[spriteIndex];
                    counter = 0f;
                } else {
                    Destroy(gameObject);
                }
            }
            else if(doReverse) {
                if(spriteIndex+1 >= sprites.Length ) {
                    goingInReverse = true;
                    counter = 0f;
                } else {
                    spriteIndex++;
                    mySpriteRenderer.sprite = sprites[spriteIndex];
                    counter = 0f;
                }
            } else {
                spriteIndex++;
                if(spriteIndex >= sprites.Length) {
                    Destroy(gameObject);
                } else {
                    mySpriteRenderer.sprite = sprites[spriteIndex];
                    counter = 0f;
                }
            }
        }
    }
}
