using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Threading;
using System;
using UnityEngine.Events;
using Unity.VisualScripting;

public class Answer : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public List<Speech> lines;
    public float textSpeed;
    public Button[] selectionButtons;  // ������ ��ư��

    private int index;
    private bool isTyping = false; // Ÿ���� ������ ���θ� ��Ÿ���� ����

    public void Start()
    {
        textComponent.text = "";
        foreach (var button in selectionButtons)
        {
            button.gameObject.SetActive(false); // ó���� ������ �г� �����
        }
        index = 0;
        NextLine();
    }

    public void CheckIsQuestion()
    {
        if (isTyping)
        {
            Debug.Log("Ÿ��������");
            return;
        }

        if (lines[index - 1].isQuestion)
        {
            Debug.Log("�������� ��ư�� ��Ȱ��ȭ��");
            return;
        }

        NextLine();
    }


    IEnumerator TypeLine() // �ѱ��ھ� ���� ��Ÿ����.
    {
        isTyping = true; // Ÿ������ ���۵��� ��Ÿ��

        foreach (char c in lines[index].text)
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        isTyping = false; // Ÿ������ �������� ��Ÿ��
        index++;
    }

    void ShowSelections(Speech speech)
    {
        ButtonOnOff(false);

        for (int i = 0; i < speech.selections.selection.Count; i++) // �������� ������ŭ ��ư�� Ȱ��ȭ �Ѵ�.
        {
            selectionButtons[i].gameObject.SetActive(true);
            selectionButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = lines[index].selections.selection[i];

            int selectionIndex = i; // ��ư Ŭ���� ���� �ε��� ����
            selectionButtons[i].onClick.RemoveAllListeners();
            selectionButtons[i].onClick.AddListener(() => OnSelectionClicked(selectionIndex));
        }
    }

    void OnSelectionClicked(int selectionIndex) // ������ ��ư�� ����̴�.
    {
        switch (selectionIndex) // �б⸦ �߰��� �־��ش�.
        {
            case 0:
                lines.InsertRange(index, lines[index - 1].selections.anserDialogue1);
                break;
            case 1:
                lines.InsertRange(index, lines[index - 1].selections.anserDialogue2);
                break;
            case 2:
                lines.InsertRange(index, lines[index - 1].selections.anserDialogue3);
                break;
        }

        NextLine();
    }


    public void NextLine()
    {
        if (index >= lines.Count)
        {
            gameObject.SetActive(false);
            return;
        }


        if (isTyping)
        {
            // Ÿ���� �߿� Ŭ���Ǹ� Ÿ������ ���߰� ��ü �ؽ�Ʈ�� ǥ��
            StopAllCoroutines();
            textComponent.text = lines[index].text;
            isTyping = false; // Ÿ������ �������� ��Ÿ��
            index++;
        }
        else
        {
            ButtonOnOff(false);
            textComponent.text = "";
            if (lines[index].isQuestion) // �����ΰ�.
            {
                ShowSelections(lines[index]);
            }
            lines[index].ExecuteTodo();
            StartCoroutine(TypeLine());
        }

    }

    public void ButtonOnOff(bool b)
    {
        for (int i = 0; i < selectionButtons.Length; i++)
        {
            selectionButtons[i].gameObject.SetActive(b); // ó���� ������ �г� �����
        }
    }
}


[System.Serializable]
public class Speech
{
    public string text;
    public bool isQuestion; // �����ΰ�?
    public UnityEvent todo;
    public selections selections;

    public Speech GetSelectAnser(string seletedAnser)
    {
        Speech anser = new Speech();
        anser.text = seletedAnser;
        return anser;
    }
    public void ExecuteTodo()
    {
        if (todo != null)
        {
            todo.Invoke();
        }
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