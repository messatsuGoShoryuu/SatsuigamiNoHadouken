using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Event_SubjectLifetimeEnded : BaseEventData
{
    public Event_SubjectLifetimeEnded(Subject subject, bool isDead)
    {
        this.subject = subject;
        this.isDead = isDead;
    }

    public Subject subject;
    public bool isDead;
}
public class Subject : MonoBehaviour
{
    [SerializeField]
    float m_lifeTime = 5.0f;

    [SerializeField]
    float m_afterDeathLifetime = 3.0f;

    [SerializeField]
    float m_walkSpeed = 2.0f;

    Animator m_animator;
    Rigidbody2D m_rigidBody;
    bool m_isFacingRight = true;
    float m_walkDestination;
    bool m_isWalking = false;

    public bool isHeretic { get; protected set; }

    private static int s_id = 0;

    private void Awake()
    {
        EventSystem.AddListener<Event_HadoukenHit>(OnHadoukenHit);
    }

    public static Subject Create(bool isHeretic, float position)
    {
        GameObject go = GameObject.Instantiate<GameObject>(
            Resources.Load<GameObject>("Prefabs/Subject"),
             new Vector3(position, -4.0f, 0.0f),
              Quaternion.identity);

        Subject subject = go.GetComponent<Subject>();
        Debug.Assert(subject);
        subject.isHeretic = isHeretic;
        subject.gameObject.name = subject.isHeretic ? ("Heretic " + s_id) : ("Believer " + s_id);
        ++s_id;

        return subject;
    }

    private void Start()
    {
        m_animator = GetComponent<Animator>();
        m_rigidBody = GetComponent<Rigidbody2D>();
        Bubble.Create(this);
        StartCoroutine(Lifetime(m_lifeTime));
        StartCoroutine(WalkDice());
    }

    private void OnHadoukenHit(Event_HadoukenHit e)
    {
        if (e.target == this.gameObject)
            Die();
    }

    private void OnDestroy()
    {
        EventSystem.RemoveListener<Event_HadoukenHit>(OnHadoukenHit);
        EventSystem.Dispatch(new Event_SubjectLifetimeEnded(this, false));
    }

    private void Die()
    {
        Stop();
        EventSystem.RemoveListener<Event_HadoukenHit>(OnHadoukenHit);
        StopAllCoroutines();
        StartCoroutine(Lifetime(m_afterDeathLifetime));
        m_animator.Play("SubjectDie");
        m_rigidBody.isKinematic = true;
        GetComponent<Collider2D>().enabled = false;
        EventSystem.Dispatch(new Event_SubjectLifetimeEnded(this, true));
    }

    IEnumerator Lifetime(float lifetime)
    {
        yield return new WaitForSeconds(lifetime);
        GameObject.Destroy(this.gameObject);
    }

    private void Walk(float point)
    {
        if (!IsAtDestination(point))
        {
            float diff = point - transform.position.x;
            if (diff < 0 && m_isFacingRight || diff > 0 && !m_isFacingRight)
            {
                Flip(!m_isFacingRight);
                ApplyVelocity();
            }
            else  ApplyVelocity();
        }
    }

    void Stop()
    {
        m_animator.Play("SubjectIdle");
        m_rigidBody.velocity = Vector3.zero;
        m_isWalking = false;
    }

    bool IsAtDestination(float point)
    {
        if(Math.Abs(transform.position.x - point) > 0.05f) return false;
        if (m_isFacingRight && transform.position.x >= point) return CompleteWalk();
        if (!m_isFacingRight && transform.position.x <= point) return CompleteWalk();
        return false;
    }

    bool CompleteWalk()
    {
        Stop();
        StartCoroutine(WalkDice());
        return true;
    }

    private void ApplyVelocity()
    {
        Vector3 velocity = m_rigidBody.velocity;
        velocity.x = m_walkSpeed * (m_isFacingRight ? 1.0f : -1.0f) * m_rigidBody.transform.right.x;
        m_rigidBody.velocity = velocity;
    }

    IEnumerator WalkDice()
    {
        yield return new WaitForSeconds(Random.Range(2.0f, 4.0f));
        m_walkDestination = Random.Range(Boundaries.InnerLeft(), Boundaries.InnerRight());
        m_isWalking = true;
        m_animator.Play("SubjectWalk");
        Debug.Log(gameObject.name + " WalkDice");
    }

    private void FixedUpdate()
    {
        if (m_isWalking)
            Walk(m_walkDestination);
    }

    void Flip(bool faceRight)
    {
        Vector3 scale = transform.localScale;
        scale.x = faceRight ? 1 : -1;
        transform.localScale = scale;
        m_isFacingRight = faceRight;
    }
}
