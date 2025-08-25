using TMPro;
using UnityEngine;

public class Money : MonoBehaviour
{
    [SerializeField] private int m_startMoney;
    private TextMeshProUGUI m_moneyText;
    private static int m_money;

    private static readonly int ClearValue = 100000;

    private void Start()
    {
        m_money = m_startMoney;
        m_moneyText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        m_moneyText.text = "" + m_money.ToString();
    }

    public static int money
    {
        get { return m_money; }
    }

    public static void AddMoney(int value)
    {
        m_money += value;
    }

    public static bool UseMoney(int value)
    {
        if (m_money >= value)
        {
            m_money -= value;
            return true;
        }
        return false;
    }

    public static bool ClearMoney()
    {
        return m_money >= ClearValue;
    }
}
