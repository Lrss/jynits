using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FabriceventPoster : MonoBehaviour {
    public void PostEvent(string eventname)
            {
        Fabric.EventManager.Instance.PostEvent(eventname, gameObject);
    }   
}

