using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private int count;
    public TextMeshProUGUI countText;
    //public GameObject winTextObject;  //이렇게 해도 되고, TextMeshProUGUI도 .gameobject를 통해서 SetActive를 사용할 수 있다.
    public TextMeshProUGUI winTextObject;
    Vector3 movement;
    [SerializeField] private float speed;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winTextObject.gameObject.SetActive(false);
    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movement = new Vector3(movementVector.x, 0.0f, movementVector.y).normalized;
    }

    private void FixedUpdate()
    {
        rb.AddForce(movement * speed);
    }

    //Trigger는 겹쳐질 때 작동한다. OnTriggerEnter, OnTriggerExit, OnTriggerStay
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp"))
        {
            count++;
            other.gameObject.SetActive(false);
            SetCountText();
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count;
        if (count >= 12)
        {
            winTextObject.gameObject.SetActive(true);
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
        }
    }

    //Collision은 충돌할 때 작동한다. OnCollisionEnter, OnCollisionExit, OnCollisionStay
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            winTextObject.gameObject.SetActive(true);
            //winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
            winTextObject.text = "You Lose!";
        }
    }
}
