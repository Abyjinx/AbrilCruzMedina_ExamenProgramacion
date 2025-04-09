using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victoria : MonoBehaviour
{

    [SerializeField] private int requiredItemCount = 8;
    private InventoryHandler inventoryHandler;

    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private GameObject victoryUIPanel;

    private bool condicionVictoria = false;

    void Start()
    {
        
        inventoryHandler = FindObjectOfType<InventoryHandler>();
    }

    void Update()
    {
        
        if (!condicionVictoria)
        {
            CheckInventoryCondition();
        }
    }

    void CheckInventoryCondition()
    {
        if (inventoryHandler != null && inventoryHandler._Inventario.Count >= requiredItemCount)
        {
            ActivarObjeto();
            condicionVictoria = true;
        }
    }

    void ActivarObjeto()
    {
        boxCollider.isTrigger = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("wa");
            PanelVictoria();
        }
    }

    void PanelVictoria()
    {
        if (victoryUIPanel != null)
        {
            victoryUIPanel.SetActive(true);

            
            Time.timeScale = 0f;

            
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            
        }
    }
}