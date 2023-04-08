using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D otherObject) {
        if (otherObject.gameObject.tag == "Stairs") {
            LevelManager.Instance.LevelCompleted();
        }
    }
}
