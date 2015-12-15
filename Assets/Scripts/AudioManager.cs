using System.Linq;
using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

    public AudioSource source;
    public AudioClip[] musics;

    private void Start() {
        source = GetComponent<AudioSource>();
    }

	// Use this for initialization
    public void PlayMusic(int i) {
        source.Stop();
        source.clip = musics[i];
        source.Play();
    }

    public void PauseMusic() {
        StartCoroutine(SlowDown());
    }

    IEnumerator SlowDown() {
        while (source.pitch > 0) {
            yield return new WaitForEndOfFrame();
            source.pitch -= Time.deltaTime;
        }
        source.Pause();
    }

    IEnumerator SpeedUp()
    {
        source.Play();
        while (source.pitch <= 1)
        {
            yield return new WaitForEndOfFrame();
            source.pitch += Time.deltaTime;
        }
    }

    public void ContinueMusic()
    {
        StartCoroutine(SpeedUp());
    }

	
	// Update is called once per frame
	void Update () {
	
	}
}
