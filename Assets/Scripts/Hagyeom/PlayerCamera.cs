using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform playerTransform;
    public LayerMask groundLayer;

    public float cameraHeight = 3.5f;   
    public float damping = 5f;

    private void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
    }
    private void Update()
    {
        if (playerTransform == null)
        {
            return;
        }

        Vector3 targetPosition = new Vector3(playerTransform.position.x, playerTransform.position.y + cameraHeight, -10);
        // 카메라를 부드럽게 이동
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * damping);

        
    }
}
