using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Box : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public GameObject[] itemPrefabs;
    private bool isOpen = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isOpen)
        {
            OpenBox();
        }
    }

    private void OpenBox()
    {
        isOpen = true;
        animator.SetBool("IsOpened", true);
        InstantiateRandomItem();
    }

    private void InstantiateRandomItem()
    {
        int randomIndex = Random.Range(0, itemPrefabs.Length);
        Instantiate(itemPrefabs[randomIndex], transform.position, Quaternion.identity);
    }
}
