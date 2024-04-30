using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    public TextMeshProUGUI coinText; // 코인 수량을 표시하는 UI Text 요소
    private int coins = 0; // 코인의 수량을 저장하는 변수

    // Start is called before the first frame update
    void Start()
    {
        UpdateCoinText(); // 게임이 시작될 때 코인 텍스트를 업데이트합니다.
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DecreaseCoins(int amount)
    {
        coins -= amount; // 지정된 양만큼 코인 수량을 감소시킵니다.
        if (coins < 0) // 코인 수량이 음수가 되지 않도록 확인합니다.
        {
            coins = 0;
        }
        UpdateCoinText(); // 코인 텍스트를 업데이트합니다.
    }

    // 코인 텍스트를 업데이트하는 함수
    void UpdateCoinText()
    {
        coinText.text = "$" + coins.ToString(); // 현재 코인 수량을 UI Text 요소에 업데이트합니다.
    }
}
