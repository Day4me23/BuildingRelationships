using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviourPun
{
    #region variables
    public KeyCode dropKey = KeyCode.Q;
    public KeyCode dorKey = KeyCode.S;
    public KeyCode interactKey = KeyCode.E;
    [Space]
    public KeyCode fire0 = KeyCode.Mouse0;
    public KeyCode fire1 = KeyCode.Mouse1;
    [Space]
    Rigidbody2D rb;
    Vector3 move;
    PhotonView view;
    [Space]
    public Vector3 spawn;
    public int playerNumber = 0;
    float delayPickup = 0;
    #endregion
    #region Main
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        view = GetComponent<PhotonView>();
    }
    void Update()
    {
        if (!view.IsMine)
            return;

        Move();

        if (delayPickup < 20)
            delayPickup += Time.deltaTime;

        if (item != null && Input.GetKeyDown(dropKey))
            DropItem();

        if (transform.position.y < -5) // respawn on fall into void
        {
            transform.position = new Vector3(0, 1, 0);
            rb.velocity = new Vector2(0, 0);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Debug.Log("Player overlap");
        }
        if (collision.transform.CompareTag("Object"))
        {
            Debug.Log(transform.name);
            if (Input.GetKey(KeyCode.S) && delayPickup >= .2f)
            {
                PickUpItem(collision.transform);
                delayPickup = 0;
            }
        }
        if (collision.transform.CompareTag("Door"))
        {
            Door door = collision.transform.GetComponent<Door>();
            if (Input.GetKey(KeyCode.S) && door.open && !door.locked && delayPickup >= 5)
            {
                DontDestroyOnLoad(this.gameObject);
                Level temp = door.levelPath;
                temp.locked = false;
                PhotonNetwork.LoadLevel(temp.level);
                delayPickup = 0;
            }
        }
        if (collision.transform.CompareTag("Portal"))
        {
            collision.GetComponent<Portal>().Teleport(this.gameObject);
        }
        if (collision.transform.CompareTag("Deconstructor"))
        {
            Debug.Log("collision " + collision.transform.name);
            if (Input.GetKey(KeyCode.S) && item != null)
                collision.GetComponent<Deconstructor>().Deconstruct(this);
        }
    }
    #endregion
    #region Item
    public GameObject Object;
    public Item item;

    void PickUpItem(Transform pickUp)
    {
        Debug.Log("pickup");
        if (item != null)
            DropItem();
        
        item = pickUp.GetComponent<Object>().item;
        Destroy(pickUp.gameObject);
    }
    void DropItem()
    {
        GameObject temp = PhotonNetwork.Instantiate("Object", transform.position + new Vector3(.01f, -.15f, 0), Quaternion.identity);
        Instantiate(Object, transform.position + new Vector3 (.01f, -.15f,0), Quaternion.identity);
        temp.GetComponent<Object>().SetupObject(item);
        item = null;
    }

    #endregion
    #region Movement
    bool groundCheck;
    public float speed;
    public float jumpForce;

    [SerializeField] LayerMask groundLayer;
    [SerializeField] float groundcheckRange;

    public void SetPos(Vector2 pos)
    {
        Debug.Log("Setting pos");
        transform.position = (Vector3)pos;
    }
    void Move()
    {
        move = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0); // walk input

        #region Springboots
        float jumpBonus;
        if (GetComponentInChildren<SpringBoots>() != null)
            jumpBonus = 1.8f;
        else jumpBonus = 0;
        #endregion

        float jumpForce = 5 * GetComponentInParent<Rigidbody2D>().gravityScale;
        if (Input.GetKeyDown(KeyCode.W) && groundCheck) // jump input
            rb.AddForce(new Vector2(0,  jumpForce + jumpBonus), ForceMode2D.Impulse); // jump

        GroundCheck();
    }
    private void FixedUpdate()
    {
        transform.position += move * Time.deltaTime * speed; // walk
        FaceThisWay(move.x);
    }
    void GroundCheck()
    {
        Debug.DrawRay(transform.position, Vector2.down * groundcheckRange, Color.green);

        if (Physics2D.Raycast(transform.position, Vector2.down, groundcheckRange, groundLayer))
            groundCheck = true;

        else groundCheck = false;

    }
    public void FaceThisWay(float x)
    {
        if ((transform.localScale.x > 0 && x < 0) || (transform.localScale.x < 0 && x > 0))
        {
            Vector3 temp = transform.localScale;
            temp.x *= -1;
            transform.localScale = temp;
        }
    }
    #endregion
}