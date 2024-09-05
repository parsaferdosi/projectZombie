using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public Transform Hat;
    public Camera Camera;
    [SerializeField] Rigidbody _rb;
    float _playerHealth = 100;
    public float playerSheild = 100;
    [SerializeField] float _walkingSpeed;
    [SerializeField] float _Runspeed;
    [SerializeField] float _jumpSpeed;
    [SerializeField] float _impactThereshold;
    Vector3 _newVolicity;
    bool _isGrounded = false;
    bool _isJumping = false;
    float vycache;
    public bool GameOver=false;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
     void Update()
    {
        // Horizontal rotation
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * 2f);   // Adjust the multiplier for different rotation speed
        _newVolicity = Vector3.up * _rb.velocity.y;
        float speed = Input.GetKey(KeyCode.LeftShift) ? _Runspeed : _walkingSpeed;
        _newVolicity.x = Input.GetAxis("Horizontal") * speed;
        _newVolicity.z = Input.GetAxis("Vertical") * speed;

        if (_isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !_isJumping)
            {
                _newVolicity.y = _jumpSpeed;
                _isJumping = true;
            }
        }
        _rb.velocity = transform.TransformDirection(_newVolicity);
        vycache = _rb.velocity.y;
    }
    void LateUpdate()
    {
        Vector3 e = Hat.eulerAngles;
        e.x -= Input.GetAxis("Mouse Y") * 2f;
        e.x=RestrictAngle(e.x,-85f,85f);
        Hat.eulerAngles = e;
        if (_playerHealth <= 0f)
        {
            GameOver = false;
            Debug.Log(GameOver);
        }
    }
    public static float RestrictAngle(float angle,float angleMin,float AngleMax)
    {
        if (angle>180)
        {
            angle -= 360;
        }
        else if(angle<-180){
            angle += 360;
        }
        if (angle > AngleMax)
        {
            angle = AngleMax;
        }
        if(angle < angleMin)
        {
            angle=angleMin;
        }
        return angle;
    }
    void OnCollisionStay(Collision col) {
       _isGrounded = true;
       _isJumping = false;
    }
    void OnCollisionExit(Collision col) { _isGrounded = false; }
    private void OnCollisionEnter(Collision col)
    {
        if (Vector3.Dot(col.GetContact(0).normal, Vector3.up) < 0.5f)
        {
            if (_rb.velocity.y < -5f)
            {
                _rb.velocity=Vector3.up * _rb.velocity.y;
                return;
            }
        }
        float acceleration = (_rb.velocity.y-vycache)/Time.fixedDeltaTime;
        float impact = _rb.mass*Mathf.Abs(acceleration);
        if (impact >= _impactThereshold)
        {
            impact -= _impactThereshold;
            Getdamage((impact) / 100);
        }
    }
    public void Getdamage(float Damage) {
        if (!_isGrounded)
        {
            _playerHealth -=Damage;
            Debug.Log("falldamage");
        }
        else if (playerSheild <= 0)
        {
            _playerHealth -=Damage;

        }
        else
        {
            playerSheild -=Damage;
            Debug.Log("shield");
            Debug.Log(playerSheild);
        }

        Debug.Log(_playerHealth);

    }

}
