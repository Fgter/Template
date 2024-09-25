using System.Collections;
using UnityEngine;

public class AnimationPlayer:MonoBehaviour
{
    [SerializeField]
    bool _playOnAwake;
    [SerializeField]
    Sprite[] _sprites;
    SpriteRenderer _spriteRenderer;

    Coroutine _cor;
    int index = 0;
    bool playing;
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void SetAnimation(string animationName,bool play=true)
    {
        index = 0;
        _sprites = ResLoader.LoadSpriteAnimation(animationName);
        if (!play) return;
        if (_cor != null)
            Stop();
        if (_sprites.Length <= 0)
        {
            Debug.LogError("Animation:" + animationName + "can not find");
            return;
        }
        _cor = StartCoroutine(Play());
    }

    private void OnEnable()
    {
        if (_playOnAwake && _sprites != null)
            _cor = StartCoroutine(Play());
    }
    IEnumerator Play()
    {
        playing = true;
        WaitForSeconds seconds = new WaitForSeconds(0.1f);
        while(this!=null)
        {
            _spriteRenderer.sprite = _sprites[index];
            index = ++index % _sprites.Length;
            yield return seconds;
        }
    }

    public void Stop()
    {
        StopCoroutine(_cor);
        playing = false;
        _cor = null;
    }
}
