using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn : MonoBehaviour
{
    public Transform TurnTransform;

    public void SetInActive()
    {
        gameObject.SetActive(false);
    }
}
