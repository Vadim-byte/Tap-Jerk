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

    public static PlayerController Instance;

    private void Awake() => Instance = this;

    private void FixedUpdate()
    {
        Mover();
    }

    private void Mover()
    {
        playerRigidbody.velocity = transform.forward * speed;
    }

    public void StartTurn()
    {
        Turn activeTurn = allTurns[TurnIndex];

        RotatePlayer(activeTurn.TurnTransform.rotation);
        activeTurn.SetInActive();

        TurnIndex++;

    }

    private void RotatePlayer(Quaternion newRotation)
    {
        StartCoroutine(RotatePlayerCor());

        IEnumerator RotatePlayerCor()
        {
            Quaternion startRotation = transform.rotation;

            for (float t = 0; t < 1; t += Time.deltaTime / rotateTime)
            {
                transform.rotation = Quaternion.Slerp(startRotation, newRotation, t);
                yield return null;
            }
            transform.rotation = newRotation;
        }
    }

}
