using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
   public void Init(string name, Vector3 pos)
   {
      gameObject.name = name;
      transform.position = pos;
   }
}
