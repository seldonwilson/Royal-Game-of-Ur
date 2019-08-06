using UnityEngine;

public class Die : MonoBehaviour
{
   [SerializeField]
   private DieSide[] _diceSides = null;
   private Rigidbody _rigidbody;
   private Vector3   _initPos;
   private bool      _hasLanded;
   private bool      _hasThrown;
   private bool      _isCocked;
   private int       _diceValue;

   public int DiceValue { get { return _diceValue; }}

   private void Start()
   {
      _rigidbody = GetComponent<Rigidbody>();
      _initPos   = transform.position;
      Reset();
   }

   private void Update()
   {
      if (Input.GetKeyDown(KeyCode.Space))
      {
         RollOrReset();
      }
      if (_rigidbody.IsSleeping() && !_hasLanded && _hasThrown)
      {
         _hasLanded             = true;
         _rigidbody.useGravity  = false;
         _rigidbody.isKinematic = true;
         SideValueCheck();
      }
      else if (_hasLanded && _isCocked)
      {
         Reset();
         ReleaseDropSpin();
      }
   }

   private void ReleaseDropSpin()
   {
      _hasThrown            = true;
      _rigidbody.useGravity = true;
      _rigidbody.AddTorque(GetRandomVector3(0, 500));
   }

   private void RollOrReset()
   {
      if (!_hasThrown && !_hasLanded)
      {
         ReleaseDropSpin();
      }
      else if (_hasThrown && _hasLanded)
      {
         Reset();
      }
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
      _diceValue             = int.MinValue;
   }

   private void SideValueCheck()
   {
      _isCocked = true;

      foreach (var side in _diceSides)
      {
          if (side.OnGround)
          {
             _isCocked  = false;
             _diceValue = side.SideValue;
             Debug.Log($"{_diceValue} has been rolled");
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
