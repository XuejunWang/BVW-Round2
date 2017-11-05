using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMindPalace : MonoBehaviour {

    public GameObject[] itemsBoard;
    public GameObject[] itemsGround;
    public GameObject[] itemsBoardPpl;
    private bool itemsBoardPplActive = false;
    
	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        for (int i = 0; i < itemsGround.Length; i++) 
        {
            if (itemsGround[i].GetComponent<ItemInteraction>().IsItemInteracted)
            {
                itemsBoard[i].SetActive(true);
            }
        }

        if (!itemsBoardPplActive)
        {
            for (int i = 3; i < itemsBoard.Length; i++)
            {
                if (itemsBoard[i].activeSelf)
                {
                    for (int j = 0; j < itemsBoardPpl.Length; j++)
                    {
                        itemsBoardPpl[j].SetActive(true);
                        itemsBoardPplActive = true;
                    }
                }
            }
        }
    }
}
