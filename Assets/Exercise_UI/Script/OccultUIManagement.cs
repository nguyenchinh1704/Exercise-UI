using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OccultUIManagement : MonoBehaviour
{
    [Serializable]
    struct Game
    {
        public Sprite imageIcon;
    }
    [SerializeField] Game[] allGames;

    private void Start()
    {
        GameObject buttonSkill = transform.GetChild(0).gameObject;
        GameObject s;

        int N = allGames.Length;
        for (int i = 0; i < N; i++)
        {
            s = Instantiate(buttonSkill, transform);
            s.transform.GetChild(0).GetComponent<Image>().sprite = allGames[i].imageIcon;
        }
        Destroy(buttonSkill);
    }
}