using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Vendor/Item")]
public class VendorItem : ScriptableObject
{
    public CShield shieldItem;
    public CHealth healthItem;
    public Weapon WeaponToSell;
    public int Cost;
}
