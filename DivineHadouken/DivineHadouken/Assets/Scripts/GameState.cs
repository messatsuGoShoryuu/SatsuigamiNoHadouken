using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_GameOver : BaseEventData
{

}

public class Event_LivesUpdated : BaseEventData
{

}

public class Event_PointsUpdated : BaseEventData
{

}

public class GameState
{
    private static GameState s_instance;
    private static GameState instance { get { if (s_instance == null) s_instance = new GameState(); return s_instance; } }
    private static bool s_initialized = false;

    public static void Init()
    {
        if (!s_initialized)
        {
            GameState gamestate = instance;
            EventSystem.AddListener<Event_SubjectLifetimeEnded>(OnSubjectDied);
            s_initialized = true;
        }
    }

    private static void OnSubjectDied(Event_SubjectLifetimeEnded e)
    {
        if (e.isDead)
        {
            if (e.subject.isHeretic) points = points + 1;
            else lives = lives - 1;
        }
    }

    private static int s_lives = 3;
    private static int s_points;
    public static int lives
    {
        protected set
        {
            s_lives = value;
            EventSystem.Dispatch(new Event_LivesUpdated());
            if (s_lives <= 0)
            {
                s_lives = 0;
                isGameOver = true;
                EventSystem.Dispatch(new Event_GameOver());
            }
        }
        get
        {
            return s_lives;
        }
    }
    public static int points
    {
        protected set
        {
            s_points = value;
            EventSystem.Dispatch(new Event_PointsUpdated());
        }
        get
        {
            return s_points;
        }
    }
    public static bool isGameOver { protected set; get; }

    public GameState()
    {
        Reset();
    }

    public static void Reset()
    {
        lives = 3;
        points = 0;
        isGameOver = false;
    }
}
