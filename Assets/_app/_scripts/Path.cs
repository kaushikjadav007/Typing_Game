using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Path : MonoBehaviour, I_Walkable
{

    public TextMeshPro m_text;
    [Space]
    public string m_my_v;

    private MeshRenderer m_rend;

    private void Start()
    {
       
    }

    public void _InitialSetup(string m_my_char)
    {
        m_my_v = m_my_char;
        m_rend = GetComponent<MeshRenderer>();

        m_text.text = m_my_char;

    }


    public void _Completed()
    {
        m_text.color = Color.green;
    }
}