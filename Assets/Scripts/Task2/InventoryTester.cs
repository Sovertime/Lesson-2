using UnityEngine;

namespace Task2
{
    public class InventoryTester : MonoBehaviour
    {
        private Inventory m_inventory = new Inventory();

        private void Start()
        {
            Debug.Log("1 — Добавить меч\n" +
                      "2 — Добавить лук\n" +
                      "3 — Добавить щит\n" +
                      "4 — Удалить меч\n" +
                      "5 — Удалить лук\n" +
                      "6 — Удалить щит\n" +
                      "7 — Проверить инвентарь");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                m_inventory.AddItem("Меч");

            if (Input.GetKeyDown(KeyCode.Alpha2))
                m_inventory.AddItem("Лук");

            if (Input.GetKeyDown(KeyCode.Alpha3))
                m_inventory.AddItem("Щит");

            if (Input.GetKeyDown(KeyCode.Alpha4))
                m_inventory.RemoveItem("Меч");

            if (Input.GetKeyDown(KeyCode.Alpha5))
                m_inventory.RemoveItem("Лук");
            
            if (Input.GetKeyDown(KeyCode.Alpha6))
                m_inventory.RemoveItem("Щит");

            if (Input.GetKeyDown(KeyCode.Alpha7))
                m_inventory.Print();
        }
    }
}