using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject[] Monsters = new GameObject[3];

    [SerializeField]
    private ParticleSystem effect;

    public void OnQuestClear()
    {
        effect.Play();
       for (int i = 0; i < Monsters.Length; i++)
            Monsters[i].SetActive(true);
    }
}
