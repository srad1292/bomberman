using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] int damage = 1;
    [SerializeField] bool canHurtEnemies = true;

    public int GetDamage() {
        return damage;
    }

    public bool GetCanHurtEnemies() {
        return canHurtEnemies;
    }
}
