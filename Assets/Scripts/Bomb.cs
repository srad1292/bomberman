using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] int reach = 3;

    public void ExplodeBomb() {
        print("I exploded!");
        Destroy(gameObject);
    }

}
