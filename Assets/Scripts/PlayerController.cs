using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public Transform viewPoint;
    public float mouseSensitivity = 1f;
    private float verticalRotStore;
    private Vector2 mouseInput;


    public float moveSpeed = 5f;
    private Vector3 moveDir, movement;

    public CharacterController charCon;

    public Camera cam;

    public float jumpforce = 12f, gravityMod = 2.5f;

    public Transform groundCheckPoint;
    private bool isGrounded;
    public LayerMask groundLayers;

    public Transform dropPoint;


    public Gun activeGun;

    Gun gun;
    public List<Gun> allGuns = new List<Gun>();
    public List<Gun> unlockableGuns = new List<Gun>();

    public int currentGun;

    RaycastHit hit;



    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        cam = Camera.main;
        gun = FindObjectOfType<Gun>();
       

    }

    
    void Update()
    {
        PlayerLookAround();

        PlayerMovement();

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchGuns();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }

    }

    void PlayerLookAround()
    {
        mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensitivity;

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseInput.x, transform.rotation.eulerAngles.z);

        verticalRotStore += mouseInput.y;
        verticalRotStore = Mathf.Clamp(verticalRotStore, -60f, 60f);

        viewPoint.rotation = Quaternion.Euler(-verticalRotStore, viewPoint.rotation.eulerAngles.y, viewPoint.rotation.eulerAngles.z);
    }


    void PlayerMovement()
    {
        moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));

        float yVel = movement.y;

        movement = ((transform.forward * moveDir.z) + (transform.right * moveDir.x)).normalized * moveSpeed;

        movement.y = yVel;

        if (charCon.isGrounded)
        {
            movement.y = 0f;
        }

        isGrounded = Physics.Raycast(groundCheckPoint.position, Vector3.down, 0.25f, groundLayers);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            movement.y = jumpforce;
        }

        movement.y += Physics.gravity.y * Time.deltaTime * gravityMod;

        charCon.Move(movement  * Time.deltaTime);
    }

    void Shoot()
    {
        if (activeGun.currentAmmo > 0)
        {


            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 50f))
            {
                gun.firePoint.LookAt(hit.point);
            }
            else
            {
                gun.firePoint.LookAt(Camera.main.transform.position + (Camera.main.transform.forward * 30f));
            }
            activeGun.currentAmmo--;
            Instantiate(activeGun.bullet , gun.firePoint.position, gun.firePoint.rotation);

        }
    }

    public void SwitchGuns()
    {
        activeGun.gameObject.SetActive(false);
        currentGun++;
        if (currentGun >= allGuns.Count)
        {
            currentGun = 0;
        }
        activeGun = allGuns[currentGun];
        activeGun.gameObject.SetActive(true);

    }

   
    public void AddGun(string gunToAdd)
    {
        bool gunUnlocked = false;

        if(unlockableGuns.Count > 0)
        {
            for(int i = 0; i < unlockableGuns.Count; i++)
            {
                if(unlockableGuns[i].gunName == gunToAdd)
                {

                    gunUnlocked = true;

                    allGuns.Add(unlockableGuns[i]);

                    unlockableGuns.RemoveAt(i);

                    i = unlockableGuns.Count;
                }
            }
        }

        if (gunUnlocked)
        {
            currentGun = allGuns.Count - 2;
            SwitchGuns();
        }
    }

    public void DropGun()
    {
        //activeGun.transform.parent = null;
        //activeGun.transform.position = dropPoint.position;
       
        activeGun.gameObject.SetActive(false);
        unlockableGuns.Add(activeGun);
        allGuns.Remove(activeGun);
        


    }



    void Reload()
    {
        if (activeGun.currentAmmo < activeGun.Mag)
        {
            activeGun.currentAmmo = activeGun.Mag;
        }
    }

    

    private void LateUpdate()
    {
        cam.transform.position = viewPoint.position;
        cam.transform.rotation = viewPoint.rotation;
    }
}