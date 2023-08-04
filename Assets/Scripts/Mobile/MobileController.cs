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
    // public GameObject mobileUI;
    // public GameObject playerState;
    public GameObject mobileAttack;
    
    
    public GameObject mobileSwitchWeapon;
    public GameObject mobileInteraction;
    public GameObject weaponList;

    public GameObject weapon0;
    public GameObject weapon1;
    public GameObject weapon2;

    //public PavelWeapon saw;
    //public PavelWeapon pistol;

    public TextMeshProUGUI ammoText;

    public int currentWeaponAmmo;

    public bool isAttacking;
    public bool isInteracting;

    
    public int weaponIndex = 0;
    public string weaponName = "Unequipped";

    public float curTime;
    public float maxTime;
    public bool timerOn = false;

    public PavelWeapon[] weapons;

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
        // CheckMobileMode();
        EventManager.current.onPlayerSwitchWeapon += SwitchWeapon;
    }

    // Update is called once per frame
    void Update()
    {
        // CheckMobileMode();
        // Update Joystick
        if (Mathf.Abs(joystickLeft.Vertical) >= 0.05 || Mathf.Abs(joystickLeft.Horizontal) >= 0.05) {
            directionLeft = Vector3.up * joystickLeft.Vertical + Vector3.right * joystickLeft.Horizontal;
        }
        else
            directionLeft = Vector3.zero;

        if (Mathf.Abs(joystickRight.Vertical) >= 0.05 || Mathf.Abs(joystickRight.Horizontal) >= 0.05) {
            directionRight = Vector3.up * joystickRight.Vertical + Vector3.right * joystickRight.Horizontal;
        }

        if (joystickRight.Direction.magnitude >= 0.9) {
            isAttacking = true;
        }
        else {
            isAttacking = false;
        }
        // if (Input.touchCount > 0) {
        //     Touch touch = Input.GetTouch(0);

        //     // Update the Text on the screen depending on current position of the touch each frame
        //     Debug.Log("touch position = " + touch.position);
        // }
        // else {
        // }

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
        if (weaponIndex == 2) {
            currentWeaponAmmo = PlayerResourcesSystem.current.bullets1;
            ammoText.SetText(currentWeaponAmmo.ToString());
        }
        else if (weaponIndex == 3) {
            currentWeaponAmmo = PlayerResourcesSystem.current.bullets2;
            ammoText.SetText(currentWeaponAmmo.ToString());
        }
        else {
            ammoText.SetText("∞");
        }
    }

    public void OpenWeaponList() {
        weaponList.SetActive(true);
        foreach(PavelWeapon weapon in weapons) {
            if(weaponName == "Unequipped") {
                weapon0.SetActive(false);
            }
            else {
                weapon0.SetActive(true);
            }

            if(weapon.isAquired && weapon.weaponName == "Saw") {
                weapon1.SetActive(true);
            }
            else {
                weapon1.SetActive(false);
            }

            if(weapon.isAquired && weapon.weaponName == "ProjectileGun") {
                    weapon2.SetActive(true);
            }
            else{
                weapon2.SetActive(false);
            }
        }
    }

    // private void CheckMobileMode() {
    //     isMobileActive = PavelPlayerSettingStates.current.mobileMovement;
    //     if (!isMobileActive) {
    //         mobileUI.SetActive(false);
    //         playerState.SetActive(true);
    //     }
    //     else {
    //         mobileUI.SetActive(true);
    //         playerState.SetActive(false);
    //     }
    // }

    public void CloseWeaponList() {
        weaponList.SetActive(false);
    }

    public void ChooseWeapon0() {
        weaponIndex = 0;
        weaponName = "Unequipped";
        Sprite img = weapon0.GetComponent<Image>().sprite;
        mobileSwitchWeapon.GetComponent<Image>().sprite = img;
        EventManager.current.PlayerSwitchWeapon(MobileController.current.weaponName);
        CloseWeaponList();
    }

    public void ChooseWeapon1() {
        weaponIndex = 1;
        weaponName = "Saw";
        Sprite img = weapon1.GetComponent<Image>().sprite;
        mobileSwitchWeapon.GetComponent<Image>().sprite = img;
        EventManager.current.PlayerSwitchWeapon(MobileController.current.weaponName);
        CloseWeaponList();
    }

    public void ChooseWeapon2() {
        Debug.Log("Chooseweapon2 called");
        weaponIndex = 2;
        weaponName = "ProjectileGun";
        Sprite img = weapon2.GetComponent<Image>().sprite;
        mobileSwitchWeapon.GetComponent<Image>().sprite = img;
        EventManager.current.PlayerSwitchWeapon(MobileController.current.weaponName);
        CloseWeaponList();
    }

    public void ClickSwitchWeapon() {
        // Debug.Log(weaponList.activeSelf);
        if (weaponList.activeSelf == false) {
            OpenWeaponList();
        }  
        else {
            CloseWeaponList();
        }
        // EventManager.current.PlayerSwitchWeapon("Saw");
    }

    public void SwitchWeapon(string weaponName) {
        Debug.Log("switch weapon is called: " + weaponName);
        foreach(PavelWeapon weapon in weapons) {
            if(weapon.isAquired) {
                if(weapon.weaponName == weaponName) {
                    if(weaponName == "Saw") {
                        weaponIndex = 1;
                        Sprite img = weapon1.GetComponent<Image>().sprite;
                        mobileSwitchWeapon.GetComponent<Image>().sprite = img;
                    }
                    else if(weaponName == "ProjectileGun") {
                        weaponIndex = 2;
                        Sprite img = weapon2.GetComponent<Image>().sprite;
                        mobileSwitchWeapon.GetComponent<Image>().sprite = img;
                    }
                    // weapon.gameObject.SetActive(true);s
                }
            }
        }
    }

    public void Attack() {
        if (!timerOn) {
            Debug.Log("Attack!!!!!");
            isAttacking = true;
            curTime = maxTime;
            timerOn = true;
        }
    }

    public void StartInteracting() {
        isInteracting = true;
    }

    public void StopInteractiong() {
        isInteracting = false;
    }

    void TimerEnded() {
        // if (timerOn) {
            Debug.Log("Stop Attack!!!!!");
            isAttacking = false;
            timerOn = false;
        // }
    }


    
}
