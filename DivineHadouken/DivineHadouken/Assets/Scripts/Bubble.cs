using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bubble : MonoBehaviour
{

    Subject m_owner;

    [SerializeField]
    Text m_text;

    // Use this for initialization
    void Start()
    {
        EventSystem.AddListener<Event_SubjectLifetimeEnded>(OnSubjectEnd);
    }

    public static Bubble Create(Subject owner)
    {
        Canvas canvas = GameObject.FindObjectOfType<Canvas>();

        GameObject go = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Bubble"), canvas.transform);

        Bubble bubble = go.GetComponent<Bubble>();
        Debug.Assert(bubble);
        bubble.SetOwner(owner);

        return bubble;
    }

    private void SetOwner(Subject subject)
    {
        m_owner = subject;
        m_text.text = m_owner.isHeretic ? HereticStrings.GetRandomHereticString() :
        HereticStrings.GetRandomBelieverString();
    }

    private void OnSubjectEnd(Event_SubjectLifetimeEnded e)
    {
        if (e.subject != null && e.subject == m_owner)
        {
            EventSystem.RemoveListener<Event_SubjectLifetimeEnded>(OnSubjectEnd);
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        EventSystem.RemoveListener<Event_SubjectLifetimeEnded>(OnSubjectEnd);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, m_owner.transform.position);
    }
}
