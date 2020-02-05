using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubtitlePlayer : MonoBehaviour
{
    public string[] subtitles;
    public float sustainTime = 3f;
    public Text text;

    public static SubtitlePlayer instance = null;

    private void Start()
    {
        instance = this;
    }

    public void PlaySubtitle(int index,float after = 0f) {
        StartCoroutine(PlayFor(sustainTime, subtitles[index],after));
    }

    IEnumerator PlayFor(float seconds, string content, float after) {
        yield return new WaitForSeconds(after);
        text.text = content;
        yield return new WaitForSeconds(seconds);
        text.text = "";
    }
}
