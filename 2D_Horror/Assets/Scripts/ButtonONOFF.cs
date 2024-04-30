using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonONOFF : MonoBehaviour
{
    private Button button; // 버튼 오브젝트에 대한 참조
    private bool isActive = false; // 버튼 활성화 여부를 나타내는 변수
    private bool isTransitioning = false; // 버튼 색상이 변경 중인지 여부를 나타내는 변수

    // Start is called before the first frame update
    void Start()
    {
        // 버튼 오브젝트의 Button 컴포넌트를 가져옴
        button = GetComponent<Button>();

        // 버튼에 클릭 이벤트 리스너 추가
        button.onClick.AddListener(ToggleButtonState);
    }

    // 버튼 활성화 상태를 토글하는 함수
    // 버튼 활성화 상태를 토글하는 함수
    void ToggleButtonState()
    {
        // 버튼 색상 변경 중인 경우 다른 버튼 선택 막음
        if (isTransitioning)
        {
            return;
        }

        isActive = !isActive; // 현재 상태의 반대로 변경

        if (isActive)
        {
            // 다른 버튼들을 모두 비활성화
           
            // 현재 버튼 색상 변경
            StartCoroutine(ChangeButtonColor(Color.red));
        }
        else
        {
            // 모든 다른 버튼이 활성화되어 있는지 확인
            bool otherButtonsActive = CheckOtherButtonsActive();

            // 다른 버튼들이 모두 활성화되어 있다면 현재 버튼 비활성화
            if (otherButtonsActive)
            {
                button.interactable = false;
            }

            // 현재 버튼 색상 원래대로 변경
            StartCoroutine(ChangeButtonColor(Color.white));

            // 다른 버튼들 활성화
            ToggleOtherButtonsInteractability(true);
        }
    }


    // 다른 버튼들의 상호작용을 변경하는 함수
    void ToggleOtherButtonsInteractability(bool interactable)
    {
        // 모든 버튼을 찾음
        ButtonONOFF[] allButtons = FindObjectsOfType<ButtonONOFF>();

        // 모든 버튼의 상호작용을 변경
        foreach (ButtonONOFF otherButton in allButtons)
        {
            if (otherButton != this)
            {
                otherButton.button.interactable = interactable;
            }
        }
    }

    // 모든 다른 버튼이 활성화 상태인지 확인하는 함수
    bool CheckOtherButtonsActive()
    {
        // 모든 버튼을 찾음
        ButtonONOFF[] allButtons = FindObjectsOfType<ButtonONOFF>();

        // 모든 버튼이 활성화 상태인지 확인
        foreach (ButtonONOFF otherButton in allButtons)
        {
            if (otherButton != this && otherButton.isActive)
            {
                return true; // 다른 버튼 중 하나라도 활성화 상태라면 true 반환
            }
        }

        return false; // 모든 다른 버튼이 비활성화 상태라면 false 반환
    }

    // 버튼 색상을 변경하는 코루틴
    IEnumerator ChangeButtonColor(Color targetColor)
    {
        isTransitioning = true; // 색상 변경 중임을 표시

        // 색상 변경 애니메이션을 위한 보간
        float elapsedTime = 0f;
        float duration = 0.5f; // 변경에 걸리는 시간

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
            // 현재 버튼이 빨간색이 되었을 때 다른 버튼들의 상호작용을 비활성화
            ToggleOtherButtonsInteractability(false);
        }
        else
        {
            // 현재 버튼이 빨간색이 아닌 색으로 되었을 때 다른 버튼들의 상호작용을 활성화
            ToggleOtherButtonsInteractability(true);
        }

        isTransitioning = false; // 색상 변경 완료됨을 표시
    }
}
