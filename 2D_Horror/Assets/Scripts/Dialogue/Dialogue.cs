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
    private bool isTyping; // Ÿ���� ������ ���θ� ��Ÿ���� ����

    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
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

        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        isTyping = false; // Ÿ������ �������� ��Ÿ��
    }

    public void NextLine()
    {
        if (index < lines.Length - 1)
        {
            if (isTyping)
            {
                // Ÿ���� �߿� Ŭ���Ǹ� Ÿ������ ���߰� ��ü �ؽ�Ʈ�� ǥ��
                StopAllCoroutines();
                textComponent.text = lines[index];
                isTyping = false; // Ÿ������ �������� ��Ÿ��
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
