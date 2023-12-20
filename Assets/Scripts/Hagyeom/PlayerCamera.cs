using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform playerTransform;
    public LayerMask groundLayer;
    private GameObject cameraWall;
    private CinemachineConfiner _confiner;

    public float cameraHeight = 3.5f;   
    public float damping = 5f;

    IEnumerator Start()
    {
        //cameraWall = GameManager.Instance.CameraWall;
        _confiner = GetComponent<CinemachineConfiner>();
        yield return new WaitForSeconds(0.5f); //0.5�� ���� (�ӽ���ġ-��õ���� ����)
        playerTransform = GameObject.FindWithTag("Player").transform;
        cameraWall = GameObject.FindWithTag("CameraWall").gameObject;
        _confiner.m_BoundingShape2D = cameraWall.GetComponent<PolygonCollider2D>();

    }
    private void Update()
    {
        if (playerTransform == null)
        {
            return;
        }

        Vector3 targetPosition = new Vector3(playerTransform.position.x, playerTransform.position.y + cameraHeight, -10);
        // ī�޶� �ε巴�� �̵�
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * damping);
    }
}
