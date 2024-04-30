using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    public TextMeshProUGUI coinText; // ���� ������ ǥ���ϴ� UI Text ���
    private int coins = 0; // ������ ������ �����ϴ� ����

    // Start is called before the first frame update
    void Start()
    {
        UpdateCoinText(); // ������ ���۵� �� ���� �ؽ�Ʈ�� ������Ʈ�մϴ�.
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DecreaseCoins(int amount)
    {
        coins -= amount; // ������ �縸ŭ ���� ������ ���ҽ�ŵ�ϴ�.
        if (coins < 0) // ���� ������ ������ ���� �ʵ��� Ȯ���մϴ�.
        {
            coins = 0;
        }
        UpdateCoinText(); // ���� �ؽ�Ʈ�� ������Ʈ�մϴ�.
    }

    // ���� �ؽ�Ʈ�� ������Ʈ�ϴ� �Լ�
    void UpdateCoinText()
    {
        coinText.text = "$" + coins.ToString(); // ���� ���� ������ UI Text ��ҿ� ������Ʈ�մϴ�.
    }
}
