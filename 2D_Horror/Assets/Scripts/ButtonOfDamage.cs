using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonOfDamage : MonoBehaviour
{
    public List<Button> buttons; // ��ư�� ���� ����Ʈ ����
    public Image NPC_HP;
    public Image Player_HP;

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
            case 0: // ù ��° ��ư (50v)
                Damage50V(); // �α� ���
                break;
            case 1: // �� ��° ��ư (100v)
                Damage100V(); // 100V ����� �Լ� ȣ��
                break;
            case 2: // �� ��° ��ư (200v)
                Damage200V(); // �α� ���
                break;
            case 3: // �� ��° ��ư (300v)
                Damage300V(); // �α� ���
                break;
            case 4: // �ټ� ��° ��ư (400v)
                Damage400V(); // �α� ���
                break;
            case 5: // ���� ��° ��ư (500v)
                Damage500V(); // �α� ���
                break;
        }
    }

   

    public void Damage50V() {
        Debug.Log("50V"); // �α� ���
        NPC_HP.fillAmount -= 2f / 100f;
        Player_HP.fillAmount -= 2f / 100f;
    }
    public void Damage100V()
    {
        Debug.Log("100V"); // �α� ���
        NPC_HP.fillAmount -= 5f / 100f;
        Player_HP.fillAmount -= 2f / 100f;
    }

    public void Damage200V()
    {
        NPC_HP.fillAmount -= 15f / 100f;
        Player_HP.fillAmount -= 2f / 100f;
    }
    public void Damage300V()
    {
        NPC_HP.fillAmount -= 20f / 100f;
        Player_HP.fillAmount -= 10f / 100f;
    }
    public void Damage400V()
    {
        NPC_HP.fillAmount -= 50f / 100f;
        Player_HP.fillAmount -= 20f / 100f;
    }
    public void Damage500V()
    {
        NPC_HP.fillAmount -= 75f / 100f;
        Player_HP.fillAmount -= 35f / 100f;

    }



}


