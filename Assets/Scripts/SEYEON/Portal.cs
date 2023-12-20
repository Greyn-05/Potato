using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public int portalIndex;
    private bool isActive;
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player") && isActive)
        {
            Debug.Log("good");
            GameManager.Instance.EnterPortal(portalIndex);
        }
    }

    public void SetActiveState(bool state)
    {
        isActive = state;
    }
}
