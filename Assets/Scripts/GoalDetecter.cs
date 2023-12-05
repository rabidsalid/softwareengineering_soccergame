using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalDetecter : MonoBehaviour
{
    public int TeamIdentifier = 0;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "soccerball") {
            if (TeamIdentifier == 0) {
                GameManager.Instance.resetRound(0);
            } else if (TeamIdentifier == 1) {
                GameManager.Instance.resetRound(1);
            }
        }
    }
}
