using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Space]
    public Transform m_target;
    [Space]
    [Space]
    public float m_distance = 3.0f;
    public float m_height = 3.0f;
    public float m_damping = 5.0f;
    public float m_rotationdamping = 10.0f;
    [Space]
    public bool m_smoothrotation = true;
    public bool m_followbehind = true;

    public Vector3 diffRotation;
    [Space]
    public Vector3 m_offset;
    private Vector3 m_wantedPosition;


    private IEnumerator m_temp;

    // Update is called once per frame
    void Update()
    {

        if (m_followbehind)
        {

            m_wantedPosition = m_target.TransformPoint(0, m_height, -m_distance);
        }
        else
        {
            m_wantedPosition = m_target.TransformPoint(0, m_height, m_distance);
        }

        m_wantedPosition += m_offset;

        transform.position = Vector3.Lerp(transform.position, m_wantedPosition, Time.deltaTime * m_damping);


        if (m_smoothrotation)
        {
            Quaternion wantedRotation = Quaternion.LookRotation(m_target.position - transform.position, m_target.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, wantedRotation, Time.deltaTime * m_rotationdamping);
        }
        else
        {
            transform.LookAt(m_target);
            transform.eulerAngles = transform.eulerAngles - diffRotation;
        }
    }

    public void _GameStateChangeEvent(string m_state)
    {
        // Debug.Log(m_state);

        switch (m_state)
        {
            case _DataStore.m_game_start:

                if (m_temp!=null)
                {
                    StopCoroutine(m_temp);
                }

                m_temp = _AnimateOnStart();
                StartCoroutine(m_temp);
                break;

            case _DataStore.m_senance_complete:

                m_temp = _AnimateOnEnd();
                StartCoroutine(m_temp);
                break;
            case _DataStore.m_sentance_change:

                if (m_temp != null)
                {
                    StopCoroutine(m_temp);
                }

                m_temp = _AnimateOnStart();
                StartCoroutine(m_temp);
                break;

            case _DataStore.m_game_complete:

                m_temp = _AnimateOnEnd();
                StartCoroutine(m_temp);
                break;
        }
    }

    IEnumerator _AnimateOnStart()
    {
        while (m_offset.x <=0 || m_offset.z <=0)
        {

            m_offset.x -=Time.deltaTime;
            m_offset.z -= Time.deltaTime;

            yield return null;
        }

        m_offset.x = 0f;
        m_offset.z = 0f;
        Debug.Log("Done");  
    }


    IEnumerator _AnimateOnEnd()
    {
        while (m_offset.x <= 5 || m_offset.z <= 10)
        {
            m_offset.x += Time.deltaTime;
            m_offset.z += Time.deltaTime;

            if (m_offset.x >5)
            {
                m_offset.x = 5.5f;
            }

            if (m_offset.z >10)
            {
                m_offset.z = 10.2f;
            }
            yield return null;
        }

        m_offset.x = 3f;
        m_offset.z = 10f;

        Debug.Log("Done");
    }

}
