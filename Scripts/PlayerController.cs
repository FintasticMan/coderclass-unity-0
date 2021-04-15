using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour {
	public float speed = 0;
	public float jumpForce = 0;
	
	public TextMeshProUGUI countText;
	
	public GameObject winTextObject;
	
	public GameObject northWallObject;
	
	private Rigidbody rb;
	
	private int count;
	private float movementX;
	private float movementY;
	
	private bool isGrounded;
	
	// Start is called before the first frame update
	void Start() {
		rb = GetComponent<Rigidbody>();
		
		count = 0;
		SetCountText();
		
		winTextObject.SetActive(false);
	}
	
	void Update() {
		if (Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded) {
			rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
		}
	}

	void SetCountText() {
		countText.text = "Count: " + count.ToString();
		
		if (count >= 13) {
// 			winTextObject.SetActive(true);
			northWallObject.SetActive(false);
		}
	}
	
	void OnMove(InputValue movementValue) {
		Vector2 movementVector = movementValue.Get<Vector2>();
		
		movementX = movementVector.x;
		movementY = movementVector.y;
	}
	
	void FixedUpdate() {
		Vector3 movement = new Vector3(movementX, 0.0f, movementY);
		
		rb.AddForce(movement * speed);
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag("PickUp")) {
			other.gameObject.SetActive(false);
			count++;
			
			SetCountText();
		}
		else if (other.gameObject.CompareTag("DeathPlane")) {
			transform.position = new Vector3(0.0f, 0.5f, 0.0f);
		}
	}
	
	void OnCollisionEnter(Collision other) {
		if (other.gameObject.CompareTag("Ground")) {
			isGrounded = true;
		}
	}
	
	void OnCollisionExit(Collision other) {
		if (other.gameObject.CompareTag("Ground")) {
			isGrounded = false;
		}
	}
}
