using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement; // 씬 관리를 위해 추가

public class Dialogue_Prologue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public GameObject[] activeObj;
    public string sceneNameToActivatePanel = "Prologue"; // activeObj[0]을 활성화할 씬의 이름
    public float textSpeed;
    public string[] dialog_text;



    private int index;
    private bool isTyping;
    private bool isActive = false; // activeObj[0] 상태를 추적


    void Start()
    {
        textComponent.text = string.Empty;
        StartDialogue();
    }

    public void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());

    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        textComponent.text = string.Empty;

        
        string currentLine = dialog_text[index];

        foreach (char c in currentLine)
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        isTyping = false;
    }

    public void NextLine()
    {
        if (index < dialog_text.Length - 1)
        {
            if (isTyping)
            {
                StopAllCoroutines();
                textComponent.text = dialog_text[index];
                isTyping = false;

            }
            else
            {
                Debug.Log(index);
                switch (index)
                {
                    case 2:
                    case 8:
                    case 13:
                    case 19:
                    case 25:
                        if (activeObj[0] != null)
                        {
                            if (!isActive)
                            {
                                activeObj[0].SetActive(true);
                                isActive = true;
                                return; // activeObj[0]을 활성화하고 대화 진행을 일시 정지
                            }
                            else
                            {
                                activeObj[0].SetActive(false);
                                isActive = false;
                            }
                        }
                        else
                        {
                            Debug.LogWarning("activeObj[0] is not assigned.");
                        }
                        break;

                    case 14:
                        if (activeObj[1] != null)
                        {
                            if (!isActive)
                            {
                                activeObj[1].SetActive(true);
                                isActive = true;
                                return; // activeObj[1]을 활성화하고 대화 진행을 일시 정지
                            }
                            else
                            {
                                activeObj[1].SetActive(false);
                                isActive = false;
                                
                            }
                        }
                        else
                        {
                            Debug.LogWarning("activeObj[1] is not assigned.");
                        }
                        break;

                    case 32:
                        Debug.Log("finish");
                        SceneManager.LoadScene("Stage1");
                        return;


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
                if (activeObj[0] != null)
                {
                    activeObj[0].SetActive(true); // 특정 씬에서만 activeObj[0] 활성화
                }
                else
                {
                    Debug.LogWarning("activeObj[0] is not assigned.");
                }
            }
        }
    }
}