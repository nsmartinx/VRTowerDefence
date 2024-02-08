using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Photon.Pun;

public class XRGrabNetworkInteractable3X : XRGrabInteractable
{
    public PhotonView photonView;
    public PhotonView photonView1;
    public PhotonView photonView2;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    protected override void OnSelectEntered(XRBaseInteractor interactor)
    {
        photonView.RequestOwnership();
        photonView1.RequestOwnership();
        photonView2.RequestOwnership();
        base.OnSelectEntered(interactor);
    }
}
