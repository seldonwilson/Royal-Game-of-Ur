using UnityEngine;

public class DieSide : MonoBehaviour
{
      // Represents opposite face of the sphere collider.
   [SerializeField]
   private int  _sideValue = int.MinValue;
   private bool _onGround;

   public bool OnGround  { get { return _onGround;  }}
   public int  SideValue { get { return _sideValue; }}


   private void OnTriggerEnter(Collider other)
   {
      if (other.tag == "Ground")
      {
         _onGround = true;
      }
   }

   private void OnTriggerExit(Collider other)
   {
      if (other.tag == "Ground")
      {
         _onGround = false;
      }
   }
}
