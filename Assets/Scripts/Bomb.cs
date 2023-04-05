using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] int reach = 3;
    [SerializeField] GameObject warningPrefab;

    List<Vector2> validHitPoints;
    List<GameObject> warnings;

    private void Start() {
        warnings = new List<GameObject>();
        validHitPoints = GetValidHitPoints();
        SpawnWarnings(validHitPoints);
        validHitPoints.Insert(0, new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y)));
    }

    private List<Vector2> GetValidHitPoints() {
        List<Vector2> result = new List<Vector2>();
        Vector2 centeredPoint = new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y));
        // Get each valid point and add to result
        return result;
    }

    private void SpawnWarnings(List<Vector2> hitPoints) {
        foreach(Vector2 hitPoint in hitPoints) {
            warnings.Add(Instantiate(warningPrefab, hitPoint, Quaternion.identity));
        }
    }

    public void ExplodeBomb() {
        print("I exploded!");
        foreach(GameObject warning in warnings) {
            Destroy(warning);
        }
        Destroy(gameObject);
    }

}
