using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering.PostProcessing;
using UnityEngine;

public class Profile : MonoBehaviour
{
    public TextMeshProUGUI Pname;
    public TextMeshProUGUI Page;
    public TextMeshProUGUI Psex;
    public TextMeshProUGUI Pjob;
    public TextMeshProUGUI Pspecial;

    public void setname(string name)
    {
        StartCoroutine(TypeLine(Pname, name,0.3f));
    }
    public void setage(string age)
    {
        StartCoroutine(TypeLine(Page, age, 0.35f));
    }
    public void setsex(string sex)
    {
        StartCoroutine(TypeLine(Psex, sex, 0.37f));
    }

    public void setjob(string job)
    {
        StartCoroutine(TypeLine(Pjob, job, 0.2f));
    }

    public void setspecial(string special)
    {
        StartCoroutine(TypeLine(Pspecial, special, 0.2f));
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
