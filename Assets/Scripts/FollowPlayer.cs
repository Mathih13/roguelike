﻿using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour
{

    public GameObject target { get; private set; }
    public float verticalOffset;
    public float lookAheadDstX;
    public float lookSmoothTimeX;
    public float verticalSmoothTime;
    public Vector2 focusAreaSize;
    private Bounds playerBounds;
    FocusArea focusArea;

    float currentLookAheadX;
    float targetLookAheadX;
    float lookAheadDirX;
    float smoothLookVelocityX;
    float smoothVelocityY;

    bool lookAheadStopped;

    void Start()
    {
        BoardEventHub.Instance.onBoardReady += LookForTarget;
    }

    void LookForTarget()
    {
        if (target == null)
        {
            if (BoardManager.Instance.player != null)
            {
                target = BoardManager.Instance.player.gameObject;
                playerBounds = target.GetComponent<BoxCollider2D>().bounds;
                focusArea = new FocusArea(playerBounds, focusAreaSize);
            }
        }
    }

    void LateUpdate()
    {
        if (target != null)
        {
            //focusArea.Update(target.GetComponent<BoxCollider2D>().bounds);

            //Vector2 focusPosition = focusArea.centre + Vector2.up * verticalOffset;

            ////focusPosition.x = Mathf.SmoothDamp(transform.position.x, focusPosition.x, ref smoothLookVelocityX, lookSmoothTimeX);

            ////focusPosition.y = Mathf.SmoothDamp(transform.position.y, focusPosition.y, ref smoothVelocityY, verticalSmoothTime);
            //focusPosition += Vector2.right * currentLookAheadX;
            //transform.position = (Vector3)focusPosition + Vector3.forward * -10;

            transform.position = target.transform.position + new Vector3(0, 0, -10);

        } else
        {
            LookForTarget();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, .5f);
        Gizmos.DrawCube(focusArea.centre, focusAreaSize);
    }

    struct FocusArea
    {
        public Vector2 centre;
        public Vector2 velocity;
        float left, right;
        float top, bottom;


        public FocusArea(Bounds targetBounds, Vector2 size)
        {
            left = targetBounds.center.x - size.x / 2;
            right = targetBounds.center.x + size.x / 2;
            bottom = targetBounds.min.y;
            top = targetBounds.min.y + size.y;

            velocity = Vector2.zero;
            centre = new Vector2((left + right) / 2, (top + bottom) / 2);
        }

        public void Update(Bounds targetBounds)
        {
            float shiftX = 0;
            if (targetBounds.min.x < left)
            {
                shiftX = targetBounds.min.x - left;
            }
            else if (targetBounds.max.x > right)
            {
                shiftX = targetBounds.max.x - right;
            }
            left += shiftX;
            right += shiftX;

            float shiftY = 0;
            if (targetBounds.min.y < bottom)
            {
                shiftY = targetBounds.min.y - bottom;
            }
            else if (targetBounds.max.y > top)
            {
                shiftY = targetBounds.max.y - top;
            }
            top += shiftY;
            bottom += shiftY;
            centre = new Vector2((left + right) / 2, (top + bottom) / 2);
            velocity = new Vector2(shiftX, shiftY);
        }
    }

}
