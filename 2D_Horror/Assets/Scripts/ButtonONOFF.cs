using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonONOFF : MonoBehaviour
{
    private Button button; // ��ư ������Ʈ�� ���� ����
    private bool isActive = false; // ��ư Ȱ��ȭ ���θ� ��Ÿ���� ����
    private bool isTransitioning = false; // ��ư ������ ���� ������ ���θ� ��Ÿ���� ����

    // Start is called before the first frame update
    void Start()
    {
        // ��ư ������Ʈ�� Button ������Ʈ�� ������
        button = GetComponent<Button>();

        // ��ư�� Ŭ�� �̺�Ʈ ������ �߰�
        button.onClick.AddListener(ToggleButtonState);
    }

    // ��ư Ȱ��ȭ ���¸� ����ϴ� �Լ�
    // ��ư Ȱ��ȭ ���¸� ����ϴ� �Լ�
    void ToggleButtonState()
    {
        // ��ư ���� ���� ���� ��� �ٸ� ��ư ���� ����
        if (isTransitioning)
        {
            return;
        }

        isActive = !isActive; // ���� ������ �ݴ�� ����

        if (isActive)
        {
            // �ٸ� ��ư���� ��� ��Ȱ��ȭ
           
            // ���� ��ư ���� ����
            StartCoroutine(ChangeButtonColor(Color.red));
        }
        else
        {
            // ��� �ٸ� ��ư�� Ȱ��ȭ�Ǿ� �ִ��� Ȯ��
            bool otherButtonsActive = CheckOtherButtonsActive();

            // �ٸ� ��ư���� ��� Ȱ��ȭ�Ǿ� �ִٸ� ���� ��ư ��Ȱ��ȭ
            if (otherButtonsActive)
            {
                button.interactable = false;
            }

            // ���� ��ư ���� ������� ����
            StartCoroutine(ChangeButtonColor(Color.white));

            // �ٸ� ��ư�� Ȱ��ȭ
            ToggleOtherButtonsInteractability(true);
        }
    }


    // �ٸ� ��ư���� ��ȣ�ۿ��� �����ϴ� �Լ�
    void ToggleOtherButtonsInteractability(bool interactable)
    {
        // ��� ��ư�� ã��
        ButtonONOFF[] allButtons = FindObjectsOfType<ButtonONOFF>();

        // ��� ��ư�� ��ȣ�ۿ��� ����
        foreach (ButtonONOFF otherButton in allButtons)
        {
            if (otherButton != this)
            {
                otherButton.button.interactable = interactable;
            }
        }
    }

    // ��� �ٸ� ��ư�� Ȱ��ȭ �������� Ȯ���ϴ� �Լ�
    bool CheckOtherButtonsActive()
    {
        // ��� ��ư�� ã��
        ButtonONOFF[] allButtons = FindObjectsOfType<ButtonONOFF>();

        // ��� ��ư�� Ȱ��ȭ �������� Ȯ��
        foreach (ButtonONOFF otherButton in allButtons)
        {
            if (otherButton != this && otherButton.isActive)
            {
                return true; // �ٸ� ��ư �� �ϳ��� Ȱ��ȭ ���¶�� true ��ȯ
            }
        }

        return false; // ��� �ٸ� ��ư�� ��Ȱ��ȭ ���¶�� false ��ȯ
    }

    // ��ư ������ �����ϴ� �ڷ�ƾ
    IEnumerator ChangeButtonColor(Color targetColor)
    {
        isTransitioning = true; // ���� ���� ������ ǥ��

        // ���� ���� �ִϸ��̼��� ���� ����
        float elapsedTime = 0f;
        float duration = 0.5f; // ���濡 �ɸ��� �ð�

        Color startColor = button.image.color;

        while (elapsedTime < duration)
        {
            button.image.color = Color.Lerp(startColor, targetColor, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        button.image.color = targetColor;

        if (targetColor == Color.red)
        {
            // ���� ��ư�� �������� �Ǿ��� �� �ٸ� ��ư���� ��ȣ�ۿ��� ��Ȱ��ȭ
            ToggleOtherButtonsInteractability(false);
        }
        else
        {
            // ���� ��ư�� �������� �ƴ� ������ �Ǿ��� �� �ٸ� ��ư���� ��ȣ�ۿ��� Ȱ��ȭ
            ToggleOtherButtonsInteractability(true);
        }

        isTransitioning = false; // ���� ���� �Ϸ���� ǥ��
    }
}
