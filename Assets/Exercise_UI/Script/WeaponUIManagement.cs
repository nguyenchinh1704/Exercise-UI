using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUIManagement : MonoBehaviour
{
    [Serializable] struct Game
    {
        public Sprite imageIcon;
    }
    /*public List<WeaponData> weaponDatas;
    public GameObject prefabItem;
    public Transform holder;*/
    [SerializeField] Game[] allGames;
    /*[SerializeField] Game[] allGames2;*/

    private void Start()
    {
        /*SetData(weaponDatas);*/
        GameObject buttonSkill = transform.GetChild(0).gameObject;
        GameObject s;

        int N = allGames.Length;
        for(int i= 0; i< N; i++)
        {
            s = Instantiate(buttonSkill, transform);
            s.transform.GetChild(0).GetComponent <Image>().sprite = allGames [i].imageIcon;
        }
        Destroy(buttonSkill);
    }

    /*private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("a");
            SetData(allGames);
        }
        if (Input.GetKey(KeyCode.B))
        {
            Debug.Log("b");
            SetData(allGames2); 
        }
    }*/

    /*private void SetData(Game[] allGames)
    {
        throw new NotImplementedException();
    }*/
    /* public void SetData(List<WeaponData> data)
{
    this.weaponDatas = data;
    DisplayData();
}*/

    /*private void DisplayData()
    {
        //clear holder
        //read data list
        //instance prefab
    }*/
}
/*[System.Serializable]*/
/*public class WeaponData
{
    public Sprite imgWeapon;
}*/