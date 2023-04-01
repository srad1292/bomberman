using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] float playerSpeed = 2f;

    private Vector2 movementInput = Vector2.zero;
    private Rigidbody2D myRigidbody2D;
    
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        MovePlayer();
    }

    void OnMove(InputValue value) {
        movementInput = value.Get<Vector2>();
    }

    void MovePlayer() {
        myRigidbody2D.velocity = movementInput * playerSpeed * Time.deltaTime;
    }
}
