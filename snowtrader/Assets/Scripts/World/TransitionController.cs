using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionController : MonoBehaviour
{
    public Transition transition;
    public string destinationScene;
    public string destinationName;

    public class Transition
    {
        public string destinationScene;
        public Vector3 destinationLoc;

        public Transition(string destinationScene, Vector3 destinationLoc)
        {
            this.destinationScene = destinationScene;
            this.destinationLoc = destinationLoc;
        }
    }

    private void Awake()
    {
        transition = new Transition(destinationScene, PersistenceManager.transitionLocations[destinationName]);
    }
}
