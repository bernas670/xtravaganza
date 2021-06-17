using UnityEngine;
using UnityEngine.Playables;

public class Timeline : MonoBehaviour
{
    private PlayableDirector timeline;
    void Start()
    {
        timeline = GetComponent<PlayableDirector>();
    }

    public void playCutScene()
    {
        timeline.Play();
    }
}
