using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*﻿How to Use it:
Sistema Event Driven su architettura scriptable object.
Il tutto si basa sul concetto di wrappare un metodo dietro uno scriptable object 
che in realtà è un semplice evento.
Si usa la classe event listener per wrappare un metodo dentro lo scriptable object peculiare
del nostro evento.

1) creo un nuovo scriptable object event peculiare.
2) lo nomino in modo significativo in modo che richiami l'azione che deve compiere.
3) metto lo script Listener su un oggetto in scena che mi servirà come inizializzatore e da wrapper
attraverso l'utilizzo degli unity event. possiamo mettere n listener dentro questo gameobject.
4)Creo la reference come GameEvent nello script dove lo utilizzero.
5)Dove farò il check di attivazione del mio evento chiamerò il Raise().*/
[CreateAssetMenu(menuName = "Events/GameEvent")]
public class GameEvent : ScriptableObject
{
    private List<GameEventListener> listeners = new List<GameEventListener>();
    public void Raise(GameEvent gameEventAfterThis = null)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRaised();
        }
        if (gameEventAfterThis != null)
            gameEventAfterThis.Raise();
    }
    public void RegisterListener(GameEventListener listener)
    {
        listeners.Add(listener);
    }
    public void UnregisterListener(GameEventListener listener)
    {
        listeners.Remove(listener);
    }
}

public class GameEventListener : MonoBehaviour
{
    public GameEvent Event;
    public UnityEvent Response;
    private void OnEnable()
    {
        Event.RegisterListener(this);
    }
    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }
    public void OnEventRaised()
    {
        Response.Invoke();
    }
}
[CustomEditor(typeof(GameEvent), true)]

public class GameEventEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EditorGUI.BeginDisabledGroup(serializedObject.isEditingMultipleObjects);
        if (GUILayout.Button("Raise"))
        {
            ((GameEvent)target).Raise();
        }
        EditorGUI.EndDisabledGroup();
    }
}