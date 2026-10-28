using UnityEngine;

public class InventoryTester : MonoBehaviour
{
    private Inventory _inventory = new Inventory();

    private void Start()
    {
        Debug.Log("1 — Добавить меч\n" +
                  "2 — Добавить лук\n" +
                  "3 — Добавить щит\n" +
                  "4 — Удалить меч\n" +
                  "5 — Удалить лук\n" +
                  "6 — Проверить инвентарь");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            _inventory.AddItem("Меч");

        if (Input.GetKeyDown(KeyCode.Alpha2))
            _inventory.AddItem("Лук");

        if (Input.GetKeyDown(KeyCode.Alpha3))
            _inventory.AddItem("Щит");

        if (Input.GetKeyDown(KeyCode.Alpha4))
            _inventory.RemoveItem("Меч");

        if (Input.GetKeyDown(KeyCode.Alpha5))
            _inventory.RemoveItem("Лук");

        if (Input.GetKeyDown(KeyCode.Alpha6))
            _inventory.Print();
    }
}