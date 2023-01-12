using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUIManagement : MonoBehaviour
{
    public List<WeaponData> weaponDatas;
    public GameObject prefabItem;
    public Transform holder;
    private void Start()
    {
        SetData(weaponDatas);
    }
    public void SetData(List<WeaponData> data)
    {
        this.weaponDatas = data;
        DisplayData();
    }

    private void DisplayData()
    {
        //clear holder
        //read data list
        //instance prefab
    }
}
[System.Serializable]
public class WeaponData
{
    public Sprite imgWeapon;
}