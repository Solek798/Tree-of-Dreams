using UnityEngine;

public class PopupCropUi : MonoBehaviour
{
    private Inventory _inventory = null;

    public void OpenUiMenu(Inventory inventory)
    {
        _inventory = inventory;
        this.gameObject.SetActive(true);
        UIStatus.Instance.DialogOpened = true;
    }

    public void CloseUiMenu()
    {
        this.gameObject.SetActive(false);
        _inventory = null;
        UIStatus.Instance.DialogOpened = false;
    }

    public void Buy(GameObject plant)
    {
        if (_inventory != null)
        {
            var price = plant.GetComponent<PlantState>()?.plantObject.buyPrice ?? _inventory.MaxCurrency + 1;
            
            if (_inventory.Currency - price >= 0)
            {
                _inventory.Currency -= price;
                GetComponentInParent<SeedFlute>().Plant(plant);
            }
        }

        CloseUiMenu();
    }
}
