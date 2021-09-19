using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenTuch : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            PlayerController.Instance.StartTurn();
        }
    }
}
