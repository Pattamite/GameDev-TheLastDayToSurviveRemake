using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifle : MonoBehaviour {
    public int magCapacity = 30;
    public int magPocket = 12;
    public float reloadTime = 3;
    public float firerate = 700;
    public Object bullet;
    public AudioClip emptySound;
    public AudioClip fireSound;
    public AudioClip reloadSound;
    public ParticleController bulletShellParticle;
    public ParticleController magParticle;

    public int currentAmmo { get; private set; }
    public int pocketAmmo { get; private set; }
    public bool isReloading { get; private set; }
    private float timeBetweenShot;
    private float lastReloadTime;
    private float lastShotTime;
    private bool isReady = true;

    void Start () {
        if (!PlayerPrefs.HasKey(PlayerPrefKey.IS_AUDIO_ENABLE)) PlayerPrefs.SetInt(PlayerPrefKey.IS_AUDIO_ENABLE, 1);
        currentAmmo = magCapacity + 1;
        pocketAmmo = magCapacity * magPocket;
        timeBetweenShot = 60f / firerate;
        isReloading = false;
    }
	
	void Update () {
        CheckReload();
    }

    public void PullTrigger () {
        if (!isReloading && (currentAmmo <= 0) && isReady) {
            if(PlayerPrefs.GetInt(PlayerPrefKey.IS_AUDIO_ENABLE) > 0) AudioSource.PlayClipAtPoint(emptySound, transform.position);
            isReady = false;
        }
        else if (!isReloading && (currentAmmo > 0) && ((Time.time - lastShotTime) >= timeBetweenShot)) {
            Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, transform.eulerAngles.z));
            bulletShellParticle.EmitCount(1);
            currentAmmo--;
            lastShotTime = Time.time;
            if (PlayerPrefs.GetInt(PlayerPrefKey.IS_AUDIO_ENABLE) > 0) AudioSource.PlayClipAtPoint(fireSound, transform.position, 0.4f);
        }
    }

    public void ReleaseTrigger () {
        isReady = true;
    }

    public void Reload () {
        if (!isReloading && (pocketAmmo > 0) && (currentAmmo < (magCapacity + 1))) {
            isReloading = true;
            lastReloadTime = Time.time;
            if (PlayerPrefs.GetInt(PlayerPrefKey.IS_AUDIO_ENABLE) > 0) AudioSource.PlayClipAtPoint(reloadSound, transform.position);
            magParticle.EmitCount(1);
        }
    }

    public void CancleReload () {
        isReloading = false;
    }

    public float ReloadProgress () {
        return ((Time.time - lastReloadTime) / reloadTime);
    }

    public bool GrabOneMag () {
        if (pocketAmmo < (magCapacity * magPocket)) {
            pocketAmmo += magCapacity;
            if (pocketAmmo > (magCapacity * magPocket)) {
                pocketAmmo = magCapacity * magPocket;
            }
            return true;
        }
        else {
            return false;
        }
    }

    public bool GrabFullMag () {
        if (pocketAmmo < (magCapacity * magPocket)) {
            pocketAmmo = magCapacity * magPocket;
            return true;
        }
        else {
            return false;
        }
    }

    private void CheckReload () {
        if (isReloading && ((Time.time - lastReloadTime) >= reloadTime)) {
            if (currentAmmo <= 0) {
                EmptyReload();
            }
            else {
                TacticalReload();
            }

            isReloading = false;
        }
    }

    private void EmptyReload () {
        if (pocketAmmo > (magCapacity - currentAmmo)) {
            pocketAmmo -= (magCapacity - currentAmmo);
            currentAmmo = magCapacity;
        }
        else {
            currentAmmo = pocketAmmo;
            pocketAmmo = 0;
        }
    }

    private void TacticalReload () {
        if (pocketAmmo > (magCapacity + 1 - currentAmmo)) {
            pocketAmmo -= (magCapacity + 1 - currentAmmo);
            currentAmmo = magCapacity + 1;
        }
        else {
            currentAmmo += pocketAmmo;
            pocketAmmo = 0;
        }
    }
}
