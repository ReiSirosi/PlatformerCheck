using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    [SerializeField] private GameObject displayCanvas;
    [SerializeField] private GameObject winCanvas;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            displayCanvas.SetActive(false);
            winCanvas.SetActive(true);
        }
    }
}
