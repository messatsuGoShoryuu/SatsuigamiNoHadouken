using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour
{
	BoxCollider2D m_collider;
    static Boundaries s_instance;
    public static Boundaries instance
    {
        get
        {
            if (s_instance == null) s_instance = GameObject.FindObjectOfType<Boundaries>();
            return s_instance;
        }
    }

	private void Start() {
		m_collider = GetComponent<BoxCollider2D>();
	}

	public static float Left()
	{
		return instance.m_collider.bounds.min.x;
	}

	public static float Right()
	{
		return instance.m_collider.bounds.max.x;
	}
    
	public static float InnerLeft()
	{
		return Left() + 1;
	}

	public static float InnerRight()
	{
		return Right() - 1;
	}
}
