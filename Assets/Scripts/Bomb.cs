using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] int reach = 3;
    [SerializeField] GameObject warningPrefab;
    [SerializeField] GameObject originExplosionPrefab;
    [SerializeField] GameObject directionalExplosionPrefab;
    [SerializeField] LayerMask blockingMask;
    [SerializeField] LayerMask destructibleMask;

    List<Vector2> validHitPointsAndDirections;
    List<GameObject> warnings;

    Quaternion upRotation = new Quaternion(0, 0, 0.707106829f, -0.707106829f);
    Quaternion rightRotation = new Quaternion(0, 0, 1, 0);
    Quaternion downRotation = new Quaternion(0, 0, 0.707106829f, 0.707106829f);
    Quaternion leftRotation = new Quaternion(0, 0, 0, 1);

    private void Start() {
        warnings = new List<GameObject>();
        validHitPointsAndDirections = GetValidHitPoints();
        SpawnWarnings(validHitPointsAndDirections);
    }

    private List<Vector2> GetValidHitPoints() {
        List<Vector2> result = new List<Vector2>();
        Vector2 centerPoint = new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y));
        // Get each valid point and add to result
        Vector2[] directions = { Vector2.up, Vector2.right, Vector2.down, Vector2.left };
        foreach(Vector2 direction in directions) {
            Vector2 currentPoint = centerPoint;
            int distance = 0;
            bool blocked = false;
            while(distance < reach && !blocked) {
                // raycast in direction
                RaycastHit2D blockHit = Physics2D.Raycast(currentPoint, direction, 1f, blockingMask);
                RaycastHit2D destructibleHit = Physics2D.Raycast(currentPoint, direction, 1f, destructibleMask);
                if (blockHit.collider != null) {
                    blocked = true;
                }
                else if (destructibleHit.collider != null) {
                    currentPoint += direction;
                    result.Add(currentPoint);
                    result.Add(direction);
                    blocked = true;

                }
                else {
                    currentPoint += direction;
                    result.Add(currentPoint);
                    result.Add(direction);
                }
                distance++;
            }
        }
        return result;
    }

    private void SpawnWarnings(List<Vector2> hitPoints) {
        for(int idx = 0; idx < hitPoints.Count; idx+=2) { 
            warnings.Add(Instantiate(warningPrefab, hitPoints[idx], Quaternion.identity));
        }
    }

    public void ExplodeBomb() {
        foreach(GameObject warning in warnings) {
            Destroy(warning);
        }
        SpawnExplosions(validHitPointsAndDirections);
        Destroy(gameObject);
    }

    private void SpawnExplosions(List<Vector2> hitPoints) {
        Vector2 centerPoint = new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y));
        gameObject.SetActive(false);
        Instantiate(originExplosionPrefab, centerPoint, Quaternion.identity);
        Vector2 hitPosition, hitDirection;
        Quaternion hitRotation;
        for (int idx = 0; idx < hitPoints.Count; idx+=2) {
            hitPosition = hitPoints[idx];
            if(idx+1 < hitPoints.Count) {
                hitDirection = hitPoints[idx+1];
                if(hitDirection == Vector2.up) {
                    hitRotation = upRotation;
                } else if (hitDirection == Vector2.right) {
                    hitRotation = rightRotation;
                } else if (hitDirection == Vector2.down) {
                    hitRotation = downRotation;
                } else {
                    hitRotation = leftRotation;
                }
                Instantiate(directionalExplosionPrefab, hitPosition, hitRotation);

            }
        }
    }

}
