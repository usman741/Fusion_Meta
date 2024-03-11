using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerControllerFusion : NetworkBehaviour
{

    public int charindex ;
    public GameObject[] CharacterPrefabs;
    public GameObject PlayerCanvas;
    [Networked]
    public int charindexNetworked { get; set; }

    private void Awake()
    {
       
        //LookAt Score to Camera
        ConstraintSource mysource = new ConstraintSource();
        mysource.sourceTransform = Camera.main.transform;
        mysource.weight = 1.0f;
        PlayerCanvas.GetComponent<LookAtConstraint>().constraintActive = true;
        PlayerCanvas.GetComponent<LookAtConstraint>().AddSource(mysource);
        PlayerCanvas.SetActive(false);
    }
    IEnumerator  Start()
    {
        if (Object.HasStateAuthority)
        {

            //Get the character selected info from main scene 
            charindex = GameObject.FindAnyObjectByType<CharacterSelection>().Char_Index;
            ///
            GameObject chartemp = Instantiate(CharacterPrefabs[charindex], this.transform);// = 
            chartemp.name = CharacterPrefabs[charindex].name;
            GetComponent<Animator>().avatar = chartemp.GetComponent<Animator>().avatar;
            charindexNetworked = charindex;
        }
        else
        {
            yield return new WaitForSeconds(1);
            GameObject chartemp = Instantiate(CharacterPrefabs[charindexNetworked], this.transform);// = 
            chartemp.name = CharacterPrefabs[charindexNetworked].name;
            GetComponent<Animator>().avatar = chartemp.GetComponent<Animator>().avatar;

        }
        PlayerCanvas.SetActive(true);

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
