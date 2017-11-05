using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveTrigger : MonoBehaviour {

    public Sprite activeModeSprite;
    private bool hasSceneAnalyzed = false;
    public GameObject hudInterface;

    public GameObject[] trainingItems;
    public GameObject[] playZoneItems;
    public Sprite[] sceneAnalyzing;
    public int m_loops = 4;
    public float m_loopTime = 0.2f;

    [SerializeField] private ProgressScript progressScript;
    private AudioSource audioSource;
    private AudioClip audioClip;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
        //sprite = interfaceMonitor.GetComponent<SpriteRenderer>().sprite;
        for (int j = 0; j < playZoneItems.Length; j++)
        {
            playZoneItems[j].GetComponent<ItemInteraction>().IsInteractable = false;
            playZoneItems[j].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Active")
        {
            if (!hasSceneAnalyzed)
            {
                StartCoroutine(playSceneAnalyzing());
                hasSceneAnalyzed = true;
            }
            for (int i = 0; i < trainingItems.Length; i++)
            {
                trainingItems[i].GetComponent<ItemInteraction>().IsInteractable = false;
                trainingItems[i].SetActive(false);
                Destroy(trainingItems[i]);
            }
        }
    }

    IEnumerator playSceneAnalyzing()
    {
        audioClip = Resources.Load<AudioClip>("scanning");
        audioSource.clip = audioClip;
        audioSource.Play();
        audioSource.loop = true;
        progressScript.trainingMode = false;
        for (int i = 0; i < sceneAnalyzing.Length * m_loops; i++)
        {
            hudInterface.GetComponent<SpriteRenderer>().sprite = sceneAnalyzing[i % sceneAnalyzing.Length];
            yield return new WaitForSeconds(m_loopTime);
        }
        audioSource.Stop();
        hudInterface.GetComponent<SpriteRenderer>().sprite = activeModeSprite;
        audioClip = Resources.Load<AudioClip>("scan_complete");

        audioSource.PlayOneShot(audioClip);
        

        for (int j = 0; j < playZoneItems.Length; j++)
        {
            playZoneItems[j].SetActive(true);
            playZoneItems[j].GetComponent<ItemInteraction>().IsInteractable = true;
        }

        yield return new WaitForSeconds(1.2f);
        audioClip = Resources.Load<AudioClip>("lab_bgm2");
        audioSource.clip = audioClip;
        audioSource.Play();
        audioSource.loop = true;
    }
}
