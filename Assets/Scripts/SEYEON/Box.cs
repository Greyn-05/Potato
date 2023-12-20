using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Box : MonoBehaviour
{
    public Animator _animator;
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
        _animator.SetBool("IsOpened", true);
        InstantiateRandomItem();
        Destroy(gameObject, 2f);
    }

    private void InstantiateRandomItem()
    {
        int randomIndex = Random.Range(0, itemPrefabs.Length);
        Instantiate(itemPrefabs[randomIndex], transform.position, Quaternion.identity);
    }
}
