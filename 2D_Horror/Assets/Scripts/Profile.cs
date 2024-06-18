using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Profile : MonoBehaviour
{
    public TextMeshProUGUI Pname;
    public TextMeshProUGUI Page;
    public TextMeshProUGUI Psex;

    public void setname(string name)
    {
        Pname.text = name;
    }
    public void setage(string age)
    {
        Page.text = age;
    }
    public void setsex(string sex)
    {
        Psex.text = sex;
    }
}
