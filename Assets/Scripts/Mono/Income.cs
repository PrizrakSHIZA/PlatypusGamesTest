using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Income : MonoBehaviour
{
    [SerializeField]
    float incomePeroid;

    static List<Hex> capturedHexes = new List<Hex>();

    static int total;
    float lastTime;

    private void Start()
    {
        PlayerPrefs.SetInt("Money", 0);
        UIhandler.UpdateMoney();
        lastTime = Time.timeSinceLevelLoad;
    }

    private void Update()
    {
        if (Time.timeSinceLevelLoad - lastTime > incomePeroid)
        {
            lastTime = Time.timeSinceLevelLoad;
            AddIncome();
        }
    }

    public void AddIncome()
    {
        PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + GetIncome());
        UIhandler.UpdateMoney();
    }

    public static int GetIncome()
    {
        total = 0;
        foreach (Hex hex in capturedHexes)
        {
            total += hex.income;
        }
        return total;
    }

    public static void AddHex(Hex hex)
    {
        capturedHexes.Add(hex);
        UIhandler.UpdateIncome();
    }

    //for future purposes
    public static void RemoveHex(Hex hex)
    {
        capturedHexes.Remove(hex);
    }

}
