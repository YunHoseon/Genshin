using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatDatabase : MonoBehaviour
{
    private static PlayerStatDatabase psInstance = null;

    public static PlayerStatDatabase PsInstance
    {
        get
        {
            if (psInstance == null)
            {
                GameObject newGameObject = new GameObject("_PlayerStatDatabase");
                psInstance = newGameObject.AddComponent<PlayerStatDatabase>();
            }
            return psInstance;
        }
    }

    public List<Dictionary<string, object>> playerStatusDB;

    private void Awake()
    {
        playerStatusDB = CSVReader.Read("Datas/PlayerStatusData");
        /*for (var i = 0; i < playerStatusDB.Count; i++)
        {
            print("Level " + playerStatusDB[i]["Level"] + " " +
                   "MaxHp " + playerStatusDB[i]["MaxHp"] + " " +
                   "Atk " + playerStatusDB[i]["Atk"] + " " +
                   "Grd " + playerStatusDB[i]["Grd"] + " " +
                   "MaxExp " + playerStatusDB[i]["MaxExp"]);
        }*/
    }
}
