using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemManager : MonoBehaviour
{
    public Image Player_HP;
    public Image NPC_HP;

    // 각 아이템의 수량을 저장하는 변수
    public int item1Count = 0; // HP포션
    public int item2Count = 0; // 돋보기
    public int item3Count = 0; // 체인지
    public int item4Count = 0; // 정신약

    // 각 아이템의 버튼과 수량을 표시할 텍스트
    public Button HP_Potion_Button;
    public TMP_Text HP_Potion_Text;

    public Button Magnifying_Glass_Button;
    public TMP_Text Magnifying_Glass_Text;

    public Button Change_Button;
    public TMP_Text Change_Text;

    public Button Mental_Medication_Button;
    public TMP_Text Mental_Medication_Text;

    // 아이템 확률 설정
    private float commonItemProbability = 0.3f; // HP포션, 돋보기, 정신약 확률
    private float changeItemProbability = 0.1f; // 체인지 확률

    void Start()
    {
        // 버튼에 클릭 이벤트 추가 (수량 감소)
        HP_Potion_Button.onClick.AddListener(() => DecrementItem(ref item1Count, HP_Potion_Text, "HP포션", HP_Potion_Button));
        Magnifying_Glass_Button.onClick.AddListener(() => DecrementItem(ref item2Count, Magnifying_Glass_Text, "돋보기", Magnifying_Glass_Button));
        Change_Button.onClick.AddListener(() => DecrementItem(ref item3Count, Change_Text, "체인지", Change_Button));
        Mental_Medication_Button.onClick.AddListener(() => DecrementItem(ref item4Count, Mental_Medication_Text, "정신약", Mental_Medication_Button));

        // 랜덤으로 두 개의 아이템 수량 증가
        IncreaseTwoRandomItems();

        // 초기 수량을 텍스트에 표시
        UpdateItemText(item1Count, HP_Potion_Text, "HP포션", HP_Potion_Button);
        UpdateItemText(item2Count, Magnifying_Glass_Text, "돋보기", Magnifying_Glass_Button);
        UpdateItemText(item3Count, Change_Text, "체인지", Change_Button);
        UpdateItemText(item4Count, Mental_Medication_Text, "정신약", Mental_Medication_Button);
    }

    // 아이템 수량을 증가시키고 UI를 업데이트하는 함수
    void IncrementItem(ref int itemCount, TMP_Text itemText, string itemName, Button itemButton)
    {
        itemCount++;
        UpdateItemText(itemCount, itemText, itemName, itemButton);
    }

    // 아이템 수량을 감소시키고 UI를 업데이트하는 함수
    void DecrementItem(ref int itemCount, TMP_Text itemText, string itemName, Button itemButton)
    {
        if (itemCount > 0)
        {
            itemCount--;
            UpdateItemText(itemCount, itemText, itemName, itemButton);
        }
    }

    // 아이템 수량을 텍스트에 표시하고 버튼 상태를 업데이트하는 함수
    void UpdateItemText(int itemCount, TMP_Text itemText, string itemName, Button itemButton)
    {
        itemText.text = $"{itemName}\nx{itemCount}";
        itemButton.interactable = itemCount > 0;
    }

    // 랜덤으로 두 개의 아이템 수량 증가
    void IncreaseTwoRandomItems()
    {
        int[] itemCounts = { item1Count, item2Count, item3Count, item4Count };
        int[] selectedItems = SelectTwoRandomItems();

        IncrementItem(ref itemCounts[selectedItems[0]], GetTextComponent(selectedItems[0]), GetItemName(selectedItems[0]), GetButtonComponent(selectedItems[0]));
        IncrementItem(ref itemCounts[selectedItems[1]], GetTextComponent(selectedItems[1]), GetItemName(selectedItems[1]), GetButtonComponent(selectedItems[1]));

        // 업데이트된 수량을 다시 할당
        item1Count = itemCounts[0];
        item2Count = itemCounts[1];
        item3Count = itemCounts[2];
        item4Count = itemCounts[3];
    }

    // 확률에 따라 두 개의 아이템을 선택하는 함수
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

    // 확률에 따라 아이템 인덱스를 반환하는 함수
    int GetRandomItemIndex(float randomValue)
    {
        if (randomValue <= commonItemProbability) return 0; // HP포션
        if (randomValue <= 2 * commonItemProbability) return 1; // 돋보기
        if (randomValue <= 3 * commonItemProbability) return 3; // 정신약
        return 2; // 체인지
    }

    // 인덱스를 기반으로 TMP_Text 컴포넌트 반환
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

    // 인덱스를 기반으로 Button 컴포넌트 반환
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

    // 인덱스를 기반으로 아이템 이름 반환
    string GetItemName(int index)
    {
        switch (index)
        {
            case 0:
                return "HP포션";
            case 1:
                return "돋보기";
            case 2:
                return "체인지";
            case 3:
                return "정신약";
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




