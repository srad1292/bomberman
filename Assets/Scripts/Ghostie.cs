using System.Collections;
using UnityEngine;

public class Ghostie : MonoBehaviour
{
    [SerializeField] float speed = 2f;
    [SerializeField] float restTime = 0.2f;

    private Vector2 targetPosition;
    private float tolerance = 0.05f;
    private bool resting = false;

    // Replace these with a real way to determine bounds
    private int minX = -8;
    private int maxX = 8;
    private int minY = -4;
    private int maxY = 5;

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
