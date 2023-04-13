using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Ghostie : MonoBehaviour
{
    [SerializeField] float speed = 2f;
    [SerializeField] float restTime = 0.2f;
    [SerializeField] Tilemap wallMap;

    private Vector2 targetPosition;
    private float tolerance = 0.05f;
    private bool resting = false;

    private int minX;
    private int maxX;
    private int minY;
    private int maxY;

    private void Start() {
        maxX = Mathf.RoundToInt(wallMap.localBounds.max.x - 2);
        maxY = Mathf.RoundToInt(wallMap.localBounds.max.y - 1);
        minX = Mathf.RoundToInt(wallMap.localBounds.min.x + 2);
        minY = Mathf.RoundToInt(wallMap.localBounds.min.y + 2);

    }

    private void Update() {

        if (!resting && targetPosition == null || Mathf.Abs(targetPosition.x - transform.position.x) <= tolerance && Mathf.Abs(targetPosition.y - transform.position.y) <= tolerance) {
            targetPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
            if (targetPosition.x >= transform.position.x) {
                transform.rotation = Quaternion.identity;
            } else {
                // Set Y rotation to 180 to flip tail
                transform.rotation = new Quaternion(0, 1, 0, 0);
            }
            resting = true;
            StartCoroutine(RestBetweenTrips());
        }

        if (!resting) {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
    }

    IEnumerator RestBetweenTrips() {
        yield return new WaitForSeconds(restTime);
        resting = false;
    }
}
