using System.Collections.Generic;
using UnityEngine;

namespace Task2
{
    public class Inventory
    {
        private Dictionary<string, int> m_items = new Dictionary<string, int>();
        private const int MaxSlots = 10;

        public void AddItem(string item, int count = 1)
        {
            if (count <= 0)
            {
                Debug.LogWarning("Количество должно быть больше нуля!");
                return;
            }
        
            if (m_items.ContainsKey(item))
            {
                m_items[item] += count;
                Debug.Log($"Добавлено: {item} x {count}");
            
                return;
            }

            if (m_items.Count >= MaxSlots)
            {
                Debug.LogWarning($"Не удалось добавить {item}: инвентарь переполнен!");
                return;
            }
        
            m_items.Add(item, count);
            Debug.Log($"Добавлено: {item} x {count}");
        }

        public void RemoveItem(string item, int count = 1)
        {
            if (count <= 0)
            {
                Debug.Log("Количество должно быть больше нуля!");
                return;
            }
        
            if (!m_items.ContainsKey(item))
            {
                Debug.LogWarning($"Не удалось удалить {item}: предмета нет в инвентаре!");
                return;
            }

            if (m_items[item] < count)
            {
                Debug.LogWarning($"Не удалось удалить {item}: требуется {count}, доступно {m_items[item]}.");
                return;
            }

            if (m_items[item] == count)
            {
                m_items.Remove(item);
                Debug.Log($"Удалено: {item} x {count}");
            
                return;
            }

            m_items[item] -= count;
            Debug.Log($"Удалено: {item} x {count}");
        }

        public void Print()
        {
            foreach (var item in m_items)
            {
                Debug.Log($"{item.Key}: {item.Value}");
            }

            int freeSlots = MaxSlots - m_items.Count;
            Debug.Log($"Занято: {m_items.Count}/{MaxSlots}. Свободно: {freeSlots}");
        }
    }
}
