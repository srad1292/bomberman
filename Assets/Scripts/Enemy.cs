using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] int startingHealth = 1;
    private int currentHealth;
    private bool dying = false;

    private void Start() {
        currentHealth = startingHealth;
    }


    private void TakeDamage(int damage) {
        currentHealth -= damage;
        if(currentHealth <= 0) {
            TimeToDie();
        }
    }

    private void TimeToDie() {
        if(dying) { return; }
        dying = true;
        // Disable collider
        // Play death animation
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Explosion") {
            Explosion explosion = other.GetComponent<Explosion>();
            if(explosion != null) {
                TakeDamage(explosion.GetDamage());
            }

        }
    }
}
