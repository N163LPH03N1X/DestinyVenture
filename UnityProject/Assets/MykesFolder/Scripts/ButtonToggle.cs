using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonToggle : MonoBehaviour
{
    public Button InventoryButton;
    Color InventReturnColor;

    public Button EquipmentButton;
    Color EquipReturnColor;

    private void Start()
    {
        InventReturnColor = InventoryButton.image.color;
        EquipReturnColor = EquipmentButton.image.color;

    }


    public void ClickedInventory()
    {
        InventoryButton.image.color = Color.blue;
        EquipmentButton.image.color = EquipReturnColor;
    }
    public void ClickedEquipment()
    {
        EquipmentButton.image.color = Color.blue;
        InventoryButton.image.color = InventReturnColor;
    }

}
