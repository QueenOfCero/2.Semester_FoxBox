using UnityEngine;
using System.Collections;
     
    public class Animal : MonoBehaviour
{

    public enum State
    {
        Idling,
        Walking,
        Running,
        IsMidAir,
        Swimming,
        Climbing,
        Holding,
        BeingHeld,
        Riding
    }

    public State state;

    IEnumerator IdlingState()
    {
        Debug.Log("Idle: Enter");
        while (state == State.Idling)
        {
            yield return 0;
        }
        Debug.Log("Idle: Exit");
        NextState();
    }

    IEnumerator WalkingState()
    {
        Debug.Log("Walk: Enter");
        while (state == State.Walking)
        {
            yield return 0;
        }
        Debug.Log("Walk: Exit");
        NextState();
    }

    IEnumerator RunningState()
    {
        Debug.Log("Run: Enter");
        while (state == State.Running)
        {
            yield return 0;
        }
        Debug.Log("Run: Exit");
        NextState();
    }

    IEnumerator IsMidAirState()
    {
        Debug.Log("IsMidAir: Enter");
        while (state == State.IsMidAir)
        {
            yield return 0;
        }
        Debug.Log("IsMidAir: Exit");
        NextState();
    }

    IEnumerator SwimmingState()
    {
        Debug.Log("Swim: Enter");
        while (state == State.Swimming)
        {
            yield return 0;
        }
        Debug.Log("Swim: Exit");
        NextState();
    }


    IEnumerator ClimbingState()
    {
        Debug.Log("Climb: Enter");
        while (state == State.Climbing)
        {
            yield return 0;
        }
        Debug.Log("Climb: Exit");
        NextState();
    }

    IEnumerator HoldState()
    {
        Debug.Log("Hold: Enter");
        while (state == State.Holding)
        {
            yield return 0;
        }
        Debug.Log("Hold: Exit");
        NextState();
    }

    IEnumerator BeingHeldState()
    {
        Debug.Log("BeingHeld: Enter");
        while (state == State.BeingHeld)
        {
            yield return 0;
        }
        Debug.Log("BeingHeld: Exit");
        NextState();
    }

    IEnumerator RidingState()
    {
        Debug.Log("Riding: Enter");
        while (state == State.Riding)
        {
            yield return 0;
        }
        Debug.Log("Riding: Exit");
        NextState();
    }

    void Start()
    {
        NextState();
    }

    void NextState()
    {
        string methodName = state.ToString() + "State";
        System.Reflection.MethodInfo info =
            GetType().GetMethod(methodName,
                                System.Reflection.BindingFlags.NonPublic |
                                System.Reflection.BindingFlags.Instance);
        StartCoroutine((IEnumerator)info.Invoke(this, null));
    }

}

