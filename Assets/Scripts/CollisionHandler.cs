using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    // Script for handling different collision scenarios
    void OnCollisionEnter(Collision other) 
    {
      switch(other.gameObject.tag) // switch depending on the tag of the object 
      {
          case "Friendly":
          Debug.Log("Start"); // print "Start" in console whenever bumping into object w/ Friendly tag
          break;

          case "Finish":
          Debug.Log("Reached Finish"); // print "Reached Finish" in console whenever bumping into object w/ Finish tag
          break;

          default:
          Debug.Log("Bumped into obstacle"); // print "Bumped into obstacle" in console whenever bumping into other objects
          break;


      }  
    }
}