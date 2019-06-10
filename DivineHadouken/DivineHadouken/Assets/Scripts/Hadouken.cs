using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_HadoukenHit : BaseEventData
{
    public Event_HadoukenHit(GameObject target)
    {
        this.target = target;
    }

    public GameObject target;
}
public class Hadouken : MonoBehaviour
{

    [SerializeField]
    float m_speed;

    Animator m_animator;
    Rigidbody2D m_rigidBody;
    // Use this for initialization
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_rigidBody = GetComponent<Rigidbody2D>();
        m_rigidBody.velocity = new Vector2(0.0f, -m_speed);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        m_rigidBody.isKinematic = true;
        m_rigidBody.velocity = Vector3.zero;
        GetComponent<Collider2D>().enabled = false;
        EventSystem.Dispatch(new Event_HadoukenHit(other.gameObject));
        m_animator.Play("HadoukenHit");
    }

    public void OnHitAnimationEnd()
    {
        GameObject.Destroy(this.gameObject);
    }

}
