using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slimer : MonoBehaviour
{
    [SerializeField] float moveSpeed = 8f;
    [SerializeField] float restTime = 0.2f;

    private Vector2 movementDirection = Vector2.zero;
    private Rigidbody2D myRigidbody2D;

    private float tolerance = 0.05f;
    private float destination;


    void Start() {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        PickNewDestination();
    }

    void Update() {
        if(DestinationReached()) {
            StartCoroutine(DelayedPickNewDestination());
        } else {
            Debug.DrawRay(transform.position, movementDirection, Color.red, 0.75f);
            RaycastHit2D hitSomething = Physics2D.Raycast(transform.position, movementDirection, 0.75f);
            if(hitSomething.collider != null) {
                print("Slimer detected tag: " + hitSomething.collider.gameObject.tag.ToString());
            }
            if (hitSomething.collider != null && hitSomething.collider.gameObject.tag != "Explosion"
                && hitSomething.collider.gameObject.tag != "Player" 
                && (hitSomething.collider.gameObject.tag == "Blockable" || hitSomething.collider.gameObject.tag == "Destructible")
            ) {
                print("Slimer stopped because tag: " + hitSomething.collider.gameObject.tag.ToString());
                StartCoroutine(DelayedPickNewDestination());
            } else {
                MoveSlimer();
            }
        }
    }

    bool DestinationReached() {
        if (movementDirection == Vector2.left || movementDirection == Vector2.right) {
            return Mathf.Abs(transform.position.x - destination) <= tolerance;
        }
        else {
            return Mathf.Abs(transform.position.y - destination) <= tolerance;
        }
    }

    IEnumerator DelayedPickNewDestination() {
        myRigidbody2D.velocity = Vector2.zero;
        Vector2 centerPoint = new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y));
        transform.position = centerPoint;
        yield return new WaitForSeconds(restTime);
        PickNewDestination();
    }
    
    void PickNewDestination() {
        SetDirection();
        SetRotation();
        SetDestination();
    }

    void SetDirection() {
        List<Vector2> directions = new List<Vector2>();
        bool foundValidDirection = false;
        while (!foundValidDirection) {
            if (directions.Count == 0) {
                directions.Add(Vector2.left);
                directions.Add(Vector2.right);
                directions.Add(Vector2.up);
                directions.Add(Vector2.down);
            }
            

            int idx = Random.Range(0, directions.Count);
            RaycastHit2D hitSomething = Physics2D.Raycast(transform.position, directions[idx], 0.75f);
            if(hitSomething.collider != null && (hitSomething.collider.gameObject.tag == "Blockable" || hitSomething.collider.gameObject.tag == "Destructible")) {
                directions.RemoveAt(idx);
            } else {
                movementDirection = directions[idx];
                foundValidDirection = true;
            }
        }
    }

    void SetRotation() {
        if (movementDirection == Vector2.right) {
            transform.rotation = Quaternion.identity;
        }
        else if(movementDirection == Vector2.left) {
            // Set Y rotation to 180 to flip
            transform.rotation = new Quaternion(0, 1, 0, 0);
        }
    }

    void SetDestination() {
        int distance = Random.Range(2, 9);
        if (movementDirection == Vector2.left) {
            destination = transform.position.x - distance;
        }
        else if (movementDirection == Vector2.right) {
            destination = transform.position.x + distance;
        }
        else if (movementDirection == Vector2.up) {
            destination = transform.position.y + distance;
        }
        else {
            destination = transform.position.y - distance;
        }

    }

    void MoveSlimer() {
        myRigidbody2D.velocity = movementDirection * moveSpeed * Time.deltaTime;
    }
}
