using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody playerRigidbody;

    [SerializeField] private List<Turn> allTurns;
    [SerializeField] private float rotateTime;

    private int _turnIndex = 0;
    public int TurnIndex
    {
        get { return _turnIndex; }
        set { if ((value > 0) && (value < allTurns.Count)) _turnIndex = value; }
    }

    private bool isRotating = false;

    public static PlayerController Instance;

    private void Awake() => Instance = this;

    private void FixedUpdate()
    {
        Mover();
    }

    private void Mover()
    {
        playerRigidbody.velocity = new Vector3(transform.forward.x * speed, playerRigidbody.velocity.y, transform.forward.z * speed);
    }

    public void StartTurn()
    {
        if (isRotating) return;

        Turn activeTurn = allTurns[TurnIndex];

        RotatePlayer(activeTurn.TurnTransform.rotation);
        activeTurn.SetInActive();
    }

    private void RotatePlayer(Quaternion newRotation)
    {
        StartCoroutine(RotatePlayerCor());

        IEnumerator RotatePlayerCor()
        {
            isRotating = true;
            float tempSpeed = speed;
            speed = 0f;
            Quaternion startRotation = transform.rotation;


            for (float t = 0; t < 1; t += Time.deltaTime / rotateTime)
            {
                transform.rotation = Quaternion.Slerp(startRotation, newRotation, t);
                yield return null;
            }
            transform.rotation = newRotation;
            speed = tempSpeed;
            isRotating = false;

            TurnIndex++;
        }
    }

}
