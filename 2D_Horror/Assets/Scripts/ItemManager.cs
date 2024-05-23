using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemManager : MonoBehaviour
{
    public Image Player_HP;
    public Image NPC_HP;

    // �� �������� ������ �����ϴ� ����
    public int item1Count = 0; // HP����
    public int item2Count = 0; // ������
    public int item3Count = 0; // ü����
    public int item4Count = 0; // ���ž�

    // �� �������� ��ư�� ������ ǥ���� �ؽ�Ʈ
    public Button HP_Potion_Button;
    public TMP_Text HP_Potion_Text;

    public Button Magnifying_Glass_Button;
    public TMP_Text Magnifying_Glass_Text;

    public Button Change_Button;
    public TMP_Text Change_Text;

    public Button Mental_Medication_Button;
    public TMP_Text Mental_Medication_Text;

    // ������ Ȯ�� ����
    private float commonItemProbability = 0.3f; // HP����, ������, ���ž� Ȯ��
    private float changeItemProbability = 0.1f; // ü���� Ȯ��

    void Start()
    {
        // ��ư�� Ŭ�� �̺�Ʈ �߰� (���� ����)
        HP_Potion_Button.onClick.AddListener(() => DecrementItem(ref item1Count, HP_Potion_Text, "HP����", HP_Potion_Button));
        Magnifying_Glass_Button.onClick.AddListener(() => DecrementItem(ref item2Count, Magnifying_Glass_Text, "������", Magnifying_Glass_Button));
        Change_Button.onClick.AddListener(() => DecrementItem(ref item3Count, Change_Text, "ü����", Change_Button));
        Mental_Medication_Button.onClick.AddListener(() => DecrementItem(ref item4Count, Mental_Medication_Text, "���ž�", Mental_Medication_Button));

        // �������� �� ���� ������ ���� ����
        IncreaseTwoRandomItems();

        // �ʱ� ������ �ؽ�Ʈ�� ǥ��
        UpdateItemText(item1Count, HP_Potion_Text, "HP����", HP_Potion_Button);
        UpdateItemText(item2Count, Magnifying_Glass_Text, "������", Magnifying_Glass_Button);
        UpdateItemText(item3Count, Change_Text, "ü����", Change_Button);
        UpdateItemText(item4Count, Mental_Medication_Text, "���ž�", Mental_Medication_Button);
    }

    // ������ ������ ������Ű�� UI�� ������Ʈ�ϴ� �Լ�
    void IncrementItem(ref int itemCount, TMP_Text itemText, string itemName, Button itemButton)
    {
        itemCount++;
        UpdateItemText(itemCount, itemText, itemName, itemButton);
    }

    // ������ ������ ���ҽ�Ű�� UI�� ������Ʈ�ϴ� �Լ�
    void DecrementItem(ref int itemCount, TMP_Text itemText, string itemName, Button itemButton)
    {
        if (itemCount > 0)
        {
            itemCount--;
            UpdateItemText(itemCount, itemText, itemName, itemButton);
        }
    }

    // ������ ������ �ؽ�Ʈ�� ǥ���ϰ� ��ư ���¸� ������Ʈ�ϴ� �Լ�
    void UpdateItemText(int itemCount, TMP_Text itemText, string itemName, Button itemButton)
    {
        itemText.text = $"{itemName}\nx{itemCount}";
        itemButton.interactable = itemCount > 0;
    }

    // �������� �� ���� ������ ���� ����
    void IncreaseTwoRandomItems()
    {
        int[] itemCounts = { item1Count, item2Count, item3Count, item4Count };
        int[] selectedItems = SelectTwoRandomItems();

        IncrementItem(ref itemCounts[selectedItems[0]], GetTextComponent(selectedItems[0]), GetItemName(selectedItems[0]), GetButtonComponent(selectedItems[0]));
        IncrementItem(ref itemCounts[selectedItems[1]], GetTextComponent(selectedItems[1]), GetItemName(selectedItems[1]), GetButtonComponent(selectedItems[1]));

        // ������Ʈ�� ������ �ٽ� �Ҵ�
        item1Count = itemCounts[0];
        item2Count = itemCounts[1];
        item3Count = itemCounts[2];
        item4Count = itemCounts[3];
    }

    // Ȯ���� ���� �� ���� �������� �����ϴ� �Լ�
    int[] SelectTwoRandomItems()
    {
        int[] items = new int[2];
        float randomValue = Random.value;
        items[0] = GetRandomItemIndex(randomValue);

        do
        {
            randomValue = Random.value;
            items[1] = GetRandomItemIndex(randomValue);
        } while (items[1] == items[0]);

        return items;
    }

    // Ȯ���� ���� ������ �ε����� ��ȯ�ϴ� �Լ�
    int GetRandomItemIndex(float randomValue)
    {
        if (randomValue <= commonItemProbability) return 0; // HP����
        if (randomValue <= 2 * commonItemProbability) return 1; // ������
        if (randomValue <= 3 * commonItemProbability) return 3; // ���ž�
        return 2; // ü����
    }

    // �ε����� ������� TMP_Text ������Ʈ ��ȯ
    TMP_Text GetTextComponent(int index)
    {
        switch (index)
        {
            case 0:
                return HP_Potion_Text;
            case 1:
                return Magnifying_Glass_Text;
            case 2:
                return Change_Text;
            case 3:
                return Mental_Medication_Text;
            default:
                return null;
        }
    }

    // �ε����� ������� Button ������Ʈ ��ȯ
    Button GetButtonComponent(int index)
    {
        switch (index)
        {
            case 0:
                return HP_Potion_Button;
            case 1:
                return Magnifying_Glass_Button;
            case 2:
                return Change_Button;
            case 3:
                return Mental_Medication_Button;
            default:
                return null;
        }
    }

    // �ε����� ������� ������ �̸� ��ȯ
    string GetItemName(int index)
    {
        switch (index)
        {
            case 0:
                return "HP����";
            case 1:
                return "������";
            case 2:
                return "ü����";
            case 3:
                return "���ž�";
            default:
                return "";
        }
    }
    public void OnclickButton(int buttonNumber)
    {
        switch (buttonNumber)
        {
            case 0:
                HP_Potion();
                break;
            case 1:
                Mental_Medication();
                break;
        }
    }
    public void HP_Potion()
    {
        NPC_HP.fillAmount += 50f / 100f;
    }

    public void Mental_Medication()
    {
        Player_HP.fillAmount += 35f / 100f;
    }
}




