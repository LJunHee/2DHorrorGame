using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemOfDamage : MonoBehaviour
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
            case 0: 
                ButtonItem1(); 
                break;
            case 1: 
                ButtonItem2(); 
                break;
            case 2: 
                ButtonItem3(); 
                break;
            case 3: 
                ButtonItem4(); 
                break;

        }
    }



    public void ButtonItem1()
    {
        Debug.Log("HP포션"); // 로그 출력
        hpBar.fillAmount += 2f / 100f;

    }
    public void ButtonItem2()
    {
        Debug.Log("돋보기"); // 로그 출력

    }

    public void ButtonItem3()
    {
        
    }
    public void ButtonItem4()
    {

        myhp.fillAmount += 2f / 100f;
    }
}
