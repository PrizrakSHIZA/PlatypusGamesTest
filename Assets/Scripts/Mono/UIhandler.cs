using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIhandler : MonoBehaviour
{
    static TextMeshProUGUI moneyText, incomeText;

    private void Start()
    {
        moneyText = GameObject.Find("UI").transform.Find("Money").GetComponent<TextMeshProUGUI>();
        incomeText = GameObject.Find("UI").transform.Find("Income").GetComponent<TextMeshProUGUI>();
    }

    public static void UpdateMoney()
    {
        moneyText.text = "Money: " + PlayerPrefs.GetInt("Money");
    }

    public static void UpdateIncome()
    {
        incomeText.text = "Income: +" + Income.GetIncome().ToString();
    }
}