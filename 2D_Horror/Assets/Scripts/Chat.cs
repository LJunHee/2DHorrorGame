using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Chat : MonoBehaviour
{
    public TextMeshProUGUI CStart;
    public TextMeshProUGUI C1;
    public TextMeshProUGUI C2;
    public TextMeshProUGUI C3;

    public void setstart(string start)
    {
        StartCoroutine(TypeLine(CStart, start,0.2f));
    }
    public void set1(string c1)
    {
        StartCoroutine(TypeLine(C1, c1, 0.0f));
    }
    public void set2(string c2)
    {
        StartCoroutine(TypeLine(C2, c2, 0.2f));
    }

    public void set3(string c3)
    {
        C3.text = c3;
    }

    public IEnumerator TypeLine(TextMeshProUGUI text, string info, float waitsc) // 한글자씩 글을 나타낸다.
    {
        text.text = "";
        foreach (char c in info)
        {
            text.text += c;
            yield return new WaitForSeconds(waitsc);
        }
    }
}
