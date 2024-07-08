using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private float moveTime = 0f;
    private float turnTime = 0f;

    public float turn;
    public float moveSpeed = 0f;

    void Update()
    {
        MonsterMove();
    }

    private void MonsterMove()
    {
        moveTime += Time.deltaTime;

        if (moveTime <= turnTime)
        {
            this.transform.Translate(moveSpeed  * Time.deltaTime, 0, 0);
        }
        else
        {
            turnTime = turn;
            moveTime = 0;

            transform.Rotate(0, 180, 0);
        }
    }
}
