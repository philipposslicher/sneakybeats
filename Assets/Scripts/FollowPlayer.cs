using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

// Attach this to the camera

  public Transform player;
  
  public float smoothTime = 0.05f;
  
  Vector3 velocity = Vector3.zero;
  
  void Start () {
  
  }
  
  void FixedUpdate () {
  
    Vector3 targetPos = player.position;
    
    targetPos.z = player.position.z;
    
    transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
  
  }



}
