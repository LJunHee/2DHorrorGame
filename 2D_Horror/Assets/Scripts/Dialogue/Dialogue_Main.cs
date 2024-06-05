using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement; // 씬 관리를 위해 추가

public class Dialogue_Main : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public GameObject Black_Panel; // Black_Panel을 참조
    public string sceneNameToActivatePanel = "Prologue"; // Black_Panel을 활성화할 씬의 이름
    public string[] dialogue_text;

    public float textSpeed;

    private int index;
    private bool isTyping;
    private bool blackPanelActive = false; // Black_Panel 상태를 추적

    void Start()
    {
        textComponent.text = string.Empty;
        StartDialogue();
    }

/*    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            NextLine();
        }
    }*/

    public void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        textComponent.text = string.Empty;

        string currentLine = dialogue_text[index].Replace("\\n", "\n");

        foreach (char c in currentLine)
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        isTyping = false;
    }

    public void NextLine()
    {
        if (index < dialogue_text.Length - 1)
        {
            if (isTyping)
            {
                StopAllCoroutines();
                textComponent.text = dialogue_text[index].Replace("\\n", "\n");
                isTyping = false;
            }
            else
            {
                if ((index == 2 || index == 8 || index == 13 || index == 19 || index == 25 || index == 29) && SceneManager.GetActiveScene().name == sceneNameToActivatePanel)
                {
                    if (Black_Panel != null)
                    {
                        if (!blackPanelActive)
                        {
                            Black_Panel.SetActive(true);
                            blackPanelActive = true;
                            return; // Black_Panel을 활성화하고 대화 진행을 일시 정지
                        }
                        else
                        {
                            Black_Panel.SetActive(false);
                            blackPanelActive = false;
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Black_Panel is not assigned.");
                    }
                }

                index++;
                textComponent.text = string.Empty;
                StartCoroutine(TypeLine());
            }
        }
        else
        {
            gameObject.SetActive(false);
            if (SceneManager.GetActiveScene().name == sceneNameToActivatePanel)
            {
                if (Black_Panel != null)
                {
                    Black_Panel.SetActive(true); // 특정 씬에서만 Black_Panel 활성화
                }
                else
                {
                    Debug.LogWarning("Black_Panel is not assigned.");
                }
            }
        }
    }
}