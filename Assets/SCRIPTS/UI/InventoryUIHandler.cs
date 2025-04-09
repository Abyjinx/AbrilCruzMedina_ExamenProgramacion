using UnityEngine;

/// <summary>
/// EJERCICIO/TAREA
/// 
/// Una vez que haya mas de 8 objetos, se debe de activar todos los objetos de el arreglo que van de la posicion 8 la 15
/// Se deben desactivar los 8 anteriores. Si avanzo se activan los que siguen y se desactivan los anteriores. Si retrocedo se activan los anteriores y se desactivan los siguientes
/// </summary>
public class InventoryUIHandler : MonoBehaviour
{
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject uiItem;
    [SerializeField] private GameObject instanceDestination;

    private GameObject[] itemsInstanciados = new GameObject[8]; 
    private int itemIndexCount = 0;

    private InventoryHandler inventory;
    private bool inventoryOpened = false;

    private int actualPage = 0;
    [SerializeField] private int maxPages = 2;

    private void Start()
    {
        inventory = FindObjectOfType<InventoryHandler>();
    }

    private void Update()
    {
        ToggleInventory();
    }

    private void ToggleInventory()
    {
        if (OpenInventoryInput())
        {
            inventoryOpened = !inventoryOpened;
            inventoryPanel.SetActive(inventoryOpened);

            if (inventoryOpened)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                UpdateInventory();
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    private void UpdateInventory()
    {
        for (int i = itemIndexCount; i < inventory._Inventario.Count && i < 8; i++)
        {
            GameObject newUiItem = Instantiate(uiItem);
            newUiItem.transform.parent = instanceDestination.transform;
            newUiItem.GetComponent<UIItem>().SetItemInfo(inventory._Inventario[itemIndexCount]);
            newUiItem.transform.localScale = Vector3.one;
            itemsInstanciados[i] = newUiItem;

            if (itemIndexCount >= 4)
            {
                newUiItem.SetActive(false);
            }

            itemIndexCount++;
        }
    }

    public void NextPage()
    {
        actualPage++;

        if (actualPage >= maxPages - 1)
        {
            actualPage = maxPages - 1;
        }

        int endIndex = Mathf.Min((actualPage * 4) + 4, inventory.maxCapacity);

        for (int i = (actualPage - 1) * 4; i < endIndex - 4; i++) 
        {
            if (itemsInstanciados[i] != null)
                itemsInstanciados[i].SetActive(false);
        }

        for (int i = actualPage * 4; i < endIndex; i++)
        {
            if (itemsInstanciados[i] != null)
                itemsInstanciados[i].SetActive(true);
            else
                Debug.Log("No existe el objeto " + i);
        }
    }

    public void PreviousPage()
    {
        actualPage--;

        if (actualPage <= 0)
        {
            actualPage = 0;
        }

        int endIndex = Mathf.Min((actualPage * 4) + 4, inventory.maxCapacity);

        for (int i = (actualPage + 1) * 4; i < Mathf.Min(endIndex + 4, inventory.maxCapacity); i++) 
        {
            if (itemsInstanciados[i] != null)
                itemsInstanciados[i].SetActive(false);
        }

        for (int i = actualPage * 4; i < endIndex; i++)
        {
            if (itemsInstanciados[i] != null)
                itemsInstanciados[i].SetActive(true);
            else
                Debug.Log("No existe el objeto " + i);
        }
    }

    private bool OpenInventoryInput()
    {
        return Input.GetKeyDown(KeyCode.I);
    }
}

