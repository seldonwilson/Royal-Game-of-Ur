using UnityEngine;

public class DieFace : MonoBehaviour
{
      // Represents value of die when this face is down
   [SerializeField]
   private int _value = int.MinValue;

   private bool _onGround;

   public bool OnGround  { get { return _onGround;  }}
   public int  Value     { get { return _value;     }}


   private void OnTriggerEnter(Collider other)
   {
      if (other.tag == "DieLanding")
      {
         _onGround = true;
      }
   }

   private void OnTriggerExit(Collider other)
   {
      if (other.tag == "DieLanding")
      {
         _onGround = false;
      }
   }
}
