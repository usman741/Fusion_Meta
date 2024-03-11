using Fusion;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : NetworkBehaviour
{
    // Start is called before the first frame update

    public Vector2 MyMove;
    public StarterAssetsInputs Inputs;
    void Start()
    {
        
    }
    public override void FixedUpdateNetwork()
    {
     /*   if (GetInput(out NetworkInputData networkInputdata))
        {
            MyMove = networkInputdata.move;
            Inputs.move = networkInputdata.move;
        }*/
       // Inputs.move= MyMove;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
