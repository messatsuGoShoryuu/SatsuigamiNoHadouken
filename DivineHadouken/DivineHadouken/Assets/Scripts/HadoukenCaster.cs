using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HadoukenCaster : MonoBehaviour
{

    [SerializeField]
    float m_speed;
    GameObject m_hadouken;

    // Use this for initialization
    void Start()
    {
        m_hadouken = Resources.Load<GameObject>("Prefabs/Hadouken");
        Debug.Assert(m_hadouken);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector3.right * Input.GetAxis("Horizontal"));
        Vector3 position = transform.position;

        position.x = Mathf.Clamp(position.x, Boundaries.Left(), Boundaries.Right());
        transform.position = position;
    }

    void Update()
    {				
        if (Input.GetKeyDown(KeyCode.Space) && !GameState.isGameOver)
        {
            SpawnHadouken();
        }
    }

    private void SpawnHadouken()
    {
        GameObject.Instantiate(m_hadouken, transform.position, m_hadouken.transform.rotation);
    }
}
