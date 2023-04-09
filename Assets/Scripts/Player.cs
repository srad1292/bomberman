using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    [SerializeField] GameObject bombPrefab;
    [SerializeField] float reloadTime = 0.8f;

    private bool canSpawnBomb = true;

    private void OnTriggerEnter2D(Collider2D otherObject) {
        if (otherObject.gameObject.tag == "Stairs") {
            LevelManager.Instance.LevelCompleted();
        }
    }

    void OnFire(InputValue value) {
        SpawnBomb();
    }

    private void SpawnBomb() {
        if(!canSpawnBomb) { return; }

        canSpawnBomb = false;
        Vector2 centerPoint = new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y));
        Instantiate(bombPrefab, centerPoint, Quaternion.identity);
        StartCoroutine(Reload());
    }

    IEnumerator Reload() {
        yield return new WaitForSeconds(reloadTime);
        canSpawnBomb = true;
    }


    
}
