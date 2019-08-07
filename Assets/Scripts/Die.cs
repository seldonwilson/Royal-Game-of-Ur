using UnityEngine;

public class Die : MonoBehaviour
{
   [SerializeField]
   private DieFace[] _faces = null;

   private Rigidbody _rigidbody;
   private Vector3   _initPos;
   private bool      _hasLanded;
   private bool      _hasThrown;
   private bool      _isCocked;
   private int       _value;

   public int Value { get { return _value; }}


   private void Start()
   {
      _rigidbody = GetComponent<Rigidbody>();
      _initPos   = transform.position;
      Reset();
   }


   private void Update()
   {
         // Consider factoring out the input to be more generic…
         // Possibly it will only RollOrReset upon receiving a particular event?
      if (Input.GetKeyDown(KeyCode.Space))
      {
            // If dice are in their starting state, drop them
         if (!_hasThrown && !_hasLanded)
         {
            ReleaseDropSpin();
         }
         else if (_hasThrown && _hasLanded)
         {
            Reset();
         }
      }
         // Die has landed, if cocked, reset and 
      if (_rigidbody.IsSleeping() && !_hasLanded && _hasThrown)
      {
         _hasLanded             = true;
         _rigidbody.useGravity  = false; // Cannot be moved by gravity
         _rigidbody.isKinematic = true;  // Cannot be moved by other physics objects
         ValueCheck();

         if (_isCocked)
         {
            Reset();
            ReleaseDropSpin();
         }
      }
   }


   private void ReleaseDropSpin()
   {
      _hasThrown            = true;
      _rigidbody.useGravity = true;
      _rigidbody.AddTorque(GetRandomVector3(0, 2000));
   }


   private void Reset()
   {
      transform.position     = _initPos;
      transform.rotation     = GetRandomRotation();
      _hasLanded             = false;
      _hasThrown             = false;
      _rigidbody.useGravity  = false;
      _rigidbody.isKinematic = false;
      _isCocked              = false;
      _value                 = int.MinValue;
   }


   private void ValueCheck()
   {
      _isCocked = true;

      foreach (var face in _faces)
      {
          if (face.OnGround)
          {
             _isCocked  = false;
             _value     = face.Value;
             Debug.Log($"{_value} has been rolled");
          }
      }
   }


   private Quaternion GetRandomRotation()
   {
      Quaternion randomRotation = Quaternion.Euler(
            x: Random.Range(0, 360),
            y: Random.Range(0, 360),
            z: Random.Range(0, 360)
      );

      return randomRotation;
   }


   private Vector3 GetRandomVector3(int low, int high)
   {
      Vector3 randomVector3 = new Vector3(
         x: Random.Range(low, high),
         y: Random.Range(low, high),
         z: Random.Range(low, high)
      );

      return randomVector3;
   }
}
