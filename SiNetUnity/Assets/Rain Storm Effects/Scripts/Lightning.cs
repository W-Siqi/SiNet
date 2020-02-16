using UnityEngine;
using System.Collections;

public class Lightning : MonoBehaviour{
	
	public float lightningOffMin = 5.0f;
	public float lightningOffMax = 30.0f;
	public float lightningOnMin = 0.05f;
	public float lightningOnMax = 0.1f;
	public float soundDelayMin = 0.25f;
	public float soundDelayMax = 2.0f;
	public GameObject lightning;
	public AudioClip[] Thunder;
	
	void OnEnable()
	{
		StartCoroutine(lighter());
	}
	
	IEnumerator lighter(){
		while (true)
		{
			yield return new WaitForSeconds(Random.Range(lightningOffMin, lightningOffMax));
			lightning.SetActive(true);
			yield return new WaitForSeconds(Random.Range(soundDelayMin, soundDelayMax));
			GetComponent<AudioSource>().PlayOneShot(Thunder[Random.Range(0, Thunder.Length)]);
			yield return new WaitForSeconds(Random.Range(lightningOnMin, lightningOnMax));
			lightning.SetActive(false);
		}
	}
}