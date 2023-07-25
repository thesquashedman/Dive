using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class MobileController : MonoBehaviour
{
    public static MobileController current;

    public bool isMobileActive;

    // Joystick
    public Joystick joystickLeft;
    public Joystick joystickRight;
    public Vector3 directionLeft;
    public Vector3 directionRight;

    // public Weapon curentWeapon;
    public GameObject mobileUI;
    public GameObject mobileAttack;
    
    public GameObject mobileSwitchWeapon;
    public GameObject mobileInteraction;
    public GameObject weaponList;

    public TextMeshProUGUI ammoText;

    public int currentWeaponAmmo;

    public bool isAttacking;
    public bool isInteracting;

    
    public int weaponIndex = 0;
    public string weaponName = "Unequipped";

    public float curTime;
    public float maxTime;
    public bool timerOn = false;

    private void Awake() {
        Debug.Log("EventManager Active.");
        //Singleton pattern
        if(current != null && current != this) {
            Destroy(this);
        } else {
            current = this;
            //Debug.Log("EventManager Active.");
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        // maxTime = 0.5f;
        // Button btn = mobileAttack.GetComponent<Button>();
		// btn.onClick.AddListener(Attack);
        isMobileActive = PavelPlayerSettingStates.current.mobileMovement;
        if (!isMobileActive)
            mobileUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Update Joystick
        if (Mathf.Abs(joystickLeft.Vertical) >= 0.05 && Mathf.Abs(joystickLeft.Horizontal) >= 0.05)
            directionLeft = Vector3.up * joystickLeft.Vertical + Vector3.right * joystickLeft.Horizontal;
        else
            directionLeft = Vector3.zero;

        if (Mathf.Abs(joystickRight.Vertical) >= 0.05 && Mathf.Abs(joystickRight.Horizontal) >= 0.05)
            directionRight = Vector3.up * joystickRight.Vertical + Vector3.right * joystickRight.Horizontal;
        
        UpdateAmmo();

        // Debug.Log(timerOn);
        if (timerOn) {
            curTime -= Time.deltaTime;
            // Debug.Log(curTime);
            if (curTime <= 0.0f)
                TimerEnded();
        }
    }

    public void UpdateAmmo() {
        currentWeaponAmmo = PlayerResourcesSystem.current.bullets1;
        ammoText.SetText(currentWeaponAmmo.ToString());
    }

    public void OpenWeaponList() {
        weaponList.SetActive(true);
    }

    public void CloseWeaponList() {
        weaponList.SetActive(false);
    }

    public void ChooseWeapon0() {
        weaponIndex = 0;
        weaponName = "Unequipped";
        CloseWeaponList();
    }

    public void ChooseWeapon1() {
        weaponIndex = 1;
        weaponName = "Saw";
        CloseWeaponList();
    }

    public void ChooseWeapon2() {
        weaponIndex = 2;
        weaponName = "ProjectileGun";
        CloseWeaponList();
    }

    public void SwitchWeapon() {
        Debug.Log(weaponList.activeSelf);
        if (weaponList.activeSelf == false) {
            OpenWeaponList();
        }  
        else {
            CloseWeaponList();
        }
        // EventManager.current.PlayerSwitchWeapon("Saw");
    }

    public void Attack() {
        if (!timerOn) {
            Debug.Log("Attack!!!!!");
            isAttacking = true;
            curTime = maxTime;
            timerOn = true;
        }
    }

    void TimerEnded() {
        // if (timerOn) {
            Debug.Log("Stop Attack!!!!!");
            isAttacking = false;
            timerOn = false;
        // }
    }


    
}
