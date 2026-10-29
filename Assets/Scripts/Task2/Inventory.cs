using System.Collections.Generic;
using UnityEngine;

namespace Task2
{
    public class Inventory
    {
        private Dictionary<string, int> _items = new Dictionary<string, int>();
        private const int MaxSlots = 10;

        public void AddItem(string item, int count = 1)
        {
            if (count <= 0)
            {
                Debug.Log("Количество должно быть больше нуля!");
                return;
            }
        
            if (_items.ContainsKey(item))
            {
                _items[item] += count;
                Debug.Log($"Добавлено: {item} x {count}");
            
                return;
            }

            if (_items.Count >= MaxSlots)
            {
                Debug.LogWarning("Отказано: инвентарь переполнен!");
                return;
            }
        
            _items.Add(item, count);
            Debug.Log($"Добавлено: {item} x {count}");
        }

        public void RemoveItem(string item, int count = 1)
        {
            if (count <= 0)
            {
                Debug.Log("Количество должно быть больше нуля!");
                return;
            }
        
            if (!_items.ContainsKey(item))
            {
                Debug.LogWarning($"Нельзя удалить {item}: предмета нет в инвентаре!");
                return;
            }

            if (_items[item] < count)
            {
                Debug.LogWarning($"Отказ");
                return;
            }

            if (_items[item] == count)
            {
                _items.Remove(item);
                Debug.Log($"Удалено: {item} x {count}");
            
                return;
            }

            _items[item] -= count;
            Debug.Log($"Удалено: {item} x {count}"); // тут хз пока с количеством
        }

        public void Print()
        {
            foreach (var item in _items)
            {
                Debug.Log($"{item.Key}: {item.Value}");
            }

            int finalslots = MaxSlots - _items.Count;
            Debug.Log($"Занято: {_items.Count}/{MaxSlots}. Свободно: {finalslots}");
        }
    }
}
