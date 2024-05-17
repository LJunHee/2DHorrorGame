using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonOfDamage : MonoBehaviour
{
    public List<Button> buttons; // 버튼을 담을 리스트 선언
    public Image hpBar;
    public Image myhp;

    // 버튼이 클릭될 때 호출되는 함수
    public void OnclickButton(int buttonNumber)
    {
        // 모든 버튼의 색상을 흰색으로 변경
        foreach (var button in buttons)
        {
            button.gameObject.GetComponent<Image>().color = Color.white;
        }

        // 클릭된 버튼의 색상을 빨간색으로 변경
        /*buttons[buttonNumber].GetComponent<Image>().color = Color.red;*/

        // 클릭된 버튼의 번호에 따라 다른 동작 수행
        switch (buttonNumber)
        {
            case 0: // 첫 번째 버튼 (50v)
                Damage50V(); // 로그 출력
                break;
            case 1: // 두 번째 버튼 (100v)
                Damage100V(); // 100V 대미지 함수 호출
                break;
            case 2: // 세 번째 버튼 (200v)
                Damage200V(); // 로그 출력
                break;
            case 3: // 네 번째 버튼 (300v)
                Damage300V(); // 로그 출력
                break;
            case 4: // 다섯 번째 버튼 (400v)
                Damage400V(); // 로그 출력
                break;
            case 5: // 여섯 번째 버튼 (500v)
                Damage500V(); // 로그 출력
                break;
        }
    }

   

    public void Damage50V() {
        Debug.Log("50V"); // 로그 출력
        hpBar.fillAmount -= 2f / 100f;
        myhp.fillAmount -= 2f / 100f;
    }
    public void Damage100V()
    {
        Debug.Log("100V"); // 로그 출력
        hpBar.fillAmount -= 5f / 100f;
        myhp.fillAmount -= 2f / 100f;
    }

    public void Damage200V()
    {
        hpBar.fillAmount -= 15f / 100f;
        myhp.fillAmount -= 2f / 100f;
    }
    public void Damage300V()
    {
        hpBar.fillAmount -= 30f / 100f;
        myhp.fillAmount -= 2f / 100f;
    }
    public void Damage400V()
    {
        hpBar.fillAmount -= 50f / 100f;
        myhp.fillAmount -= 2f / 100f;
    }
    public void Damage500V()
    {
        hpBar.fillAmount -= 75f / 100f;
        myhp.fillAmount -= 2f / 100f;
    }

}


