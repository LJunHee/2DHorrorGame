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
    //public GameObject selectionPanels; // 선택지 버튼들을 포함하는 패널
    public Button[] selectionButtons;  // 선택지 버튼들

    private int index;
    private bool isTyping = false; // 타이핑 중인지 여부를 나타내는 변수

    // Start is called before the first frame update

    public void Start()
    {

        textComponent.text = "";
        foreach (var button in selectionButtons)
        {
            button.gameObject.SetActive(false); // 처음에 선택지 패널 숨기기
        }
        index = 0;
        NextLine();
    }


    void Update()
    {
        // 대화창을 클릭하면 다음 대화로 넘어감
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            NextLine();
        }
    }



    IEnumerator TypeLine() // 한글자씩 글을 나타낸다.
    {
        isTyping = true; // 타이핑이 시작됨을 나타냄

        foreach (char c in lines[index].text)
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        isTyping = false; // 타이핑이 끝났음을 나타냄
        index++;
    }

    void ShowSelections(Speech speech)
    {
        Debug.Log(speech.text);

        for (int i = 0; i < selectionButtons.Length; i++)
        {
            selectionButtons[i].gameObject.SetActive(false); // 처음에 선택지 패널 숨기기
        }

        Debug.Log(speech.selections.selection.Count);

        for (int i = 0; i < speech.selections.selection.Count; i++) // 선택지의 갯수만큼 버튼을 활성화 한다.
        {
            selectionButtons[i].gameObject.SetActive(true);
            selectionButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = lines[index].selections.selection[i];

            int selectionIndex = i; // 버튼 클릭을 위한 인덱스 저장
            selectionButtons[i].onClick.RemoveAllListeners();
            selectionButtons[i].onClick.AddListener(() => OnSelectionClicked(selectionIndex));
        }
    }

    void OnSelectionClicked(int selectionIndex) // 선택지 버튼의 기능이다.
    {
        /*switch (selectionIndex) // 분기를 추가로 넣어준다.
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
        if (index < lines.Count) // 이야기가 끝나지 않음
        {
            if (isTyping)
            {
                // 타이핑 중에 클릭되면 타이핑을 멈추고 전체 텍스트를 표시
                StopAllCoroutines();
                textComponent.text = lines[index].text;
                isTyping = false; // 타이핑이 끝났음을 나타냄
                index++;
            }
            else
            {
                textComponent.text = "";
                Debug.Log(lines[index].text);
                Debug.Log(lines[index].isQuestion);
                if (lines[index].isQuestion) // 질문인가.
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
    public bool isQuestion; // 질문인가?
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
    public List<string> selection; // 선택지

    public List<Speech> anserDialogue1; // 첫번째를 선택했을 시 나오는 대사들
    public List<Speech> anserDialogue2;
    public List<Speech> anserDialogue3;
}




/*
 질문이면 선택지를 선택하기 전까지 넘어가면 안됨
 ㄴ> 선택지가 나와야함
 ㄴ> 선택을 하면 선택한 대사가 대사창에 나와야함
     ㄴ> GetSelectAnser로 대답한 string을 Speech로 만들어서
     ㄴ> lines에 바로 다음 대사가 되도록 넣어준다.
 ㄴ> 선택을 하면 선택지가 사라져야함
 ㄴ> 선택을 하면 선택한 후의 대사가 lines에 들어가야함
 선택지를 선택하면 해당 선택지에 맞는 대사를 

 */