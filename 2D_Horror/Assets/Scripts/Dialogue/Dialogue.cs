using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Threading;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public List<Speech> lines;
    public float textSpeed;
    //public GameObject selectionPanels; // ������ ��ư���� �����ϴ� �г�
    public Button[] selectionButtons;  // ������ ��ư��

    private int index;
    private bool isTyping; // Ÿ���� ������ ���θ� ��Ÿ���� ����

    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
        foreach(var button in selectionButtons)
        {
            button.gameObject.SetActive(false); // ó���� ������ �г� �����
        }
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        // ��ȭâ�� Ŭ���ϸ� ���� ��ȭ�� �Ѿ
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            NextLine();
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        isTyping = true; // Ÿ������ ���۵��� ��Ÿ��

        foreach (char c in lines[index].text)
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        isTyping = false; // Ÿ������ �������� ��Ÿ��

        // ���� ���� �������� Ȯ���ϰ� �������� ������
        if (lines[index].isQuestion)
        {
            ShowSelections(lines[index]);
        }
    }

    void ShowSelections(Speech speech)
    {
        for(int i = 0; i < speech.selections.selection.Count; i++)
        {
            selectionButtons[i].gameObject.SetActive(true); // ó���� ������ �г� �����
        }
        for (int i = 0; i < selectionButtons.Length; i++)
        {
            if (i < lines[index].selections.selection.Count)
            {
                selectionButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = lines[index].selections.selection[i];
                selectionButtons[i].gameObject.SetActive(true);
                int selectionIndex = i; // ��ư Ŭ���� ���� �ε��� ����
                selectionButtons[i].onClick.RemoveAllListeners();
                selectionButtons[i].onClick.AddListener(() => OnSelectionClicked(selectionIndex));
            }
            else
            {
                selectionButtons[i].gameObject.SetActive(false);
            }
        }
    }

    void OnSelectionClicked(int selectionIndex)
    {
        foreach (var button in selectionButtons)
        {
            button.gameObject.SetActive(false); // ó���� ������ �г� �����
        }
        switch (selectionIndex)
        {
            case 0:
                lines.InsertRange(index + 1, lines[index].selections.anserDialogue1);
                break;
            case 1:
                lines.InsertRange(index + 1, lines[index].selections.anserDialogue2);
                break;
            case 2:
                lines.InsertRange(index + 1, lines[index].selections.anserDialogue3);
                break;
        }
        index++;
        textComponent.text = string.Empty;
        StartCoroutine(TypeLine());
    }

    private void AddInlist(List<Speech> speeches)
    {

    }

    public void NextLine()
    {
        if (index < lines.Count)
        {
            if (isTyping)
            {
                // Ÿ���� �߿� Ŭ���Ǹ� Ÿ������ ���߰� ��ü �ؽ�Ʈ�� ǥ��
                StopAllCoroutines();
                textComponent.text = lines[index].text;
                isTyping = false; // Ÿ������ �������� ��Ÿ��

                if (lines[index].isQuestion)
                {
                    ShowSelections(lines[index]);
                }
            }
            else
            {
                index++;
                textComponent.text = string.Empty;
                StartCoroutine(TypeLine());
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}

[System.Serializable]
public class Speech
{
    public string text;
    public bool isQuestion; // �����ΰ�?
    public selections selections;

    public Speech GetSelectAnser(string seletedAnser)
    {
        Speech anser = new Speech();
        anser.text = seletedAnser;
        return anser;
    }
}

[System.Serializable]
public struct selections
{
    public List<string> selection; // ������

    public List<Speech> anserDialogue1; // ù��°�� �������� �� ������ ����
    public List<Speech> anserDialogue2;
    public List<Speech> anserDialogue3;
}




/*
 �����̸� �������� �����ϱ� ������ �Ѿ�� �ȵ�
 ��> �������� ���;���
 ��> ������ �ϸ� ������ ��簡 ���â�� ���;���
     ��> GetSelectAnser�� ����� string�� Speech�� ����
     ��> lines�� �ٷ� ���� ��簡 �ǵ��� �־��ش�.
 ��> ������ �ϸ� �������� ���������
 ��> ������ �ϸ� ������ ���� ��簡 lines�� ������
 �������� �����ϸ� �ش� �������� �´� ��縦 

 */