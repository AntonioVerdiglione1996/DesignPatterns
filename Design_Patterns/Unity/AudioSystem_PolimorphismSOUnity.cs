using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;
public abstract class AudioEvent : ScriptableObject
{
    public abstract void Play(AudioSource source);
}

[CreateAssetMenu(fileName = "SimpleAudioEvent", menuName = "AudioEvents/Simple")]
public class SimpleAudioEvent : AudioEvent
{
    public AudioClip[] clips;
    [Range(0f, 1f)]
    public float Volume = 0.5f;
    [Range(0f, 2f)]
    public float Pitch = 1f;
    public bool Loop;

    public override void Play(AudioSource source)
    {
        if (clips.Length == 0)
            return;
        source.clip = clips[Random.Range(0, clips.Length)];

        source.loop = Loop;
        source.playOnAwake = false;
        source.volume = Volume;
        source.pitch = Pitch;

        source.Play();
    }
}

[CustomEditor(typeof(AudioEvent), true)]
public class AudioEventEditor : Editor
{

    [SerializeField]
    private AudioSource _previewer;

    public void OnEnable()
    {
        //dont show my audiosource previewer in hierarchy
        _previewer = EditorUtility.CreateGameObjectWithHideFlags("Audio preview", HideFlags.HideAndDontSave, typeof(AudioSource)).GetComponent<AudioSource>();
    }

    public void OnDisable()
    {
        //use this only in editor code
        DestroyImmediate(_previewer.gameObject);
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EditorGUI.BeginDisabledGroup(serializedObject.isEditingMultipleObjects);
        if (GUILayout.Button("Preview"))
        {
            ((AudioEvent)target).Play(_previewer);
        }
        EditorGUI.EndDisabledGroup();
    }
}
