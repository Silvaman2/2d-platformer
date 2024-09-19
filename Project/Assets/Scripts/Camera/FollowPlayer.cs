using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    Vector3 targetPosition;
    [SerializeField] public float cameraDrag;
    [SerializeField] public Vector3 cameraOffset;
    [SerializeField] public GameObject player;

    void FixedUpdate()
    {
        Calculate();
        SetPosition();
    }

    private void Calculate()
    {
        Vector2 playerPosition = player.transform.position;
        Vector2 cameraPosition = transform.position;

        Vector2 distance = playerPosition - cameraPosition;

        Vector2 resultVelocity = distance / cameraDrag;

        targetPosition = transform.position + new Vector3(resultVelocity.x, resultVelocity.y, 0);
    }

    private void SetPosition()
    {
        transform.position = targetPosition;
        transform.position = transform.position + cameraOffset;
    }
}
