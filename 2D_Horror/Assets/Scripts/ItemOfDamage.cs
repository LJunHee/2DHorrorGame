using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemOfDamage : MonoBehaviour
{
    public List<Button> buttons; // ��ư�� ���� ����Ʈ ����
    public Image hpBar;
    public Image myhp;

    // ��ư�� Ŭ���� �� ȣ��Ǵ� �Լ�
    public void OnclickButton(int buttonNumber)
    {
        // ��� ��ư�� ������ ������� ����
        foreach (var button in buttons)
        {
            button.gameObject.GetComponent<Image>().color = Color.white;
        }

        // Ŭ���� ��ư�� ������ ���������� ����
        /*buttons[buttonNumber].GetComponent<Image>().color = Color.red;*/

        // Ŭ���� ��ư�� ��ȣ�� ���� �ٸ� ���� ����
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
        Debug.Log("HP����"); // �α� ���
        hpBar.fillAmount += 2f / 100f;

    }
    public void ButtonItem2()
    {
        Debug.Log("������"); // �α� ���

    }

    public void ButtonItem3()
    {
        
    }
    public void ButtonItem4()
    {

        myhp.fillAmount += 2f / 100f;
    }
}
