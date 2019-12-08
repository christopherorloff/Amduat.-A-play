using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagerScript_Hour8 : MonoBehaviour
{
    public Transform boat;
    public Transform blessedDeadParent;
    public float boatMaxSpeed;

    private int numberOfBlessedDead;
    private int blessedDeadPushingCount = 0;
    private int minimumAmountForPushing = 3;

    void Start()
    {
        numberOfBlessedDead = blessedDeadParent.childCount;
    }
    public void AddBlessedDeadPushing()
    {
        blessedDeadPushingCount++;
    }

    private void MoveBoat()
    {
        if (blessedDeadPushingCount > minimumAmountForPushing)
        {

        }
    }
}
