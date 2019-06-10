using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SubjectManager : MonoBehaviour
{

    [SerializeField]
    int m_maximumSubjects;

    [SerializeField]
    int m_currentSubjects;
    int m_believerCount;

    [SerializeField]
    float m_spawnInterval;

    private void Start()
    {
        StartCoroutine(TrySpawn());
        EventSystem.AddListener<Event_SubjectLifetimeEnded>(OnSubjectEnd);
    }

    private void OnSubjectEnd(Event_SubjectLifetimeEnded e)
    {
        if (!e.isDead)
        {
            --m_currentSubjects;
            if (!e.subject.isHeretic) --m_believerCount;
        }
    }

    IEnumerator TrySpawn()
    {
        yield return new WaitForSeconds(m_spawnInterval);
        if (m_currentSubjects < m_maximumSubjects) Spawn();
        StartCoroutine(TrySpawn());
    }

    void Spawn()
    {
        float position = Random.Range(Boundaries.Left() + 1.0f, Boundaries.Right() - 1.0f);
        bool isHeretic = (m_believerCount >= (m_maximumSubjects - 1)) ?  true : Random.Range(0, 100) >= 50;
        if (!isHeretic) ++m_believerCount;

        Subject.Create(isHeretic, position);
        ++m_currentSubjects;
        
    }
}
