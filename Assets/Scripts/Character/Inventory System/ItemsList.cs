using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsList : MonoBehaviour
{
    [SerializeField] private List<Armor> _serializedArmorList;
    [SerializeField] private List<Texture2D> _serializedArmorTextures;
    internal static List<Armor> ArmorList;

    private void Awake()
    {
        FillList();
        ArmorList = _serializedArmorList;
    }

    private void FillList()
    {
        foreach (var item in _serializedArmorTextures)
        {
            _serializedArmorList.Add(new Armor(item, item.width, item.height));
        }
    }

    public static Armor GetArmor(int id)
    {
        Armor armor = new Armor(ItemsList.ArmorList[id].Image, ItemsList.ArmorList[id].Image.width, ItemsList.ArmorList[id].Image.height);
        return armor;
    }
}
