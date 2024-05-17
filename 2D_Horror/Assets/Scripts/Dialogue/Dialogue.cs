using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;

    private int index;
    private bool isTyping; // 타이핑 중인지 여부를 나타내는 변수

    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        // 대화창을 클릭하면 다음 대화로 넘어감
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
        isTyping = true; // 타이핑이 시작됨을 나타냄

        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        isTyping = false; // 타이핑이 끝났음을 나타냄
    }

    public void NextLine()
    {
        if (index < lines.Length - 1)
        {
            if (isTyping)
            {
                // 타이핑 중에 클릭되면 타이핑을 멈추고 전체 텍스트를 표시
                StopAllCoroutines();
                textComponent.text = lines[index];
                isTyping = false; // 타이핑이 끝났음을 나타냄
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
