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

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public List<Speech> lines;
    public float textSpeed;
    //public GameObject selectionPanels; // ������ ��ư���� �����ϴ� �г�
    public Button[] selectionButtons;  // ������ ��ư��

    private int index;
    private bool isTyping = false; // Ÿ���� ������ ���θ� ��Ÿ���� ����

    // Start is called before the first frame update

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


    void Update()
    {
        // ��ȭâ�� Ŭ���ϸ� ���� ��ȭ�� �Ѿ
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            NextLine();
        }
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
        Debug.Log(speech.text);

        for (int i = 0; i < selectionButtons.Length; i++)
        {
            selectionButtons[i].gameObject.SetActive(false); // ó���� ������ �г� �����
        }

        Debug.Log(speech.selections.selection.Count);

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
        /*switch (selectionIndex) // �б⸦ �߰��� �־��ش�.
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
        }*/
        NextLine();
    }

    public void NextLine()
    {
        if (index < lines.Count) // �̾߱Ⱑ ������ ����
        {
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
                textComponent.text = "";
                Debug.Log(lines[index].text);
                Debug.Log(lines[index].isQuestion);
                if (lines[index].isQuestion) // �����ΰ�.
                {
                    ShowSelections(lines[index]);
                }
                lines[index].ExecuteTodo();
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