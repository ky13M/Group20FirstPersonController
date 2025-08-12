using UnityEngine;

public class FPSGun : MonoBehaviour

{
    [Header("refernces")]
    public Camera playerCamera;
    public Transform gunModel;
    public Transform adPosition;
    public Transform hiPosition;

    [Header("settings")]
    public float adSpeed = 10f;
    public float fireRate = 0.2f;
    public float weaponRange = 100f;
    public float damage = 20f;

    [Header("effects")]
    public ParticleSystem muzzleFlash;
    public GameObject hitEffect;

    private float nextFireTime;
    private bool isAiming = false;
    private float adsSpeed;
    private Vector3 vector3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleAiming(vector3);
        HandleShooting();
    }
    void HandleAiming(Vector3 vector3)
    {
        if (Input.GetMouseButton(1))//Right mouse button/ OR L2 on Controller.
            isAiming = true;
        else
            isAiming = false;
        Transform targetPos = isAiming ? adPosition : hiPosition;
        Vector3 vector3 = Vector3.Lerp(gunModel.position, targetPos.position, Time.deltaTime * adsSpeed);
        {
            
        }
      
       

     private void HandleShooting()
        {
            if ((Input.GetMouseButton(0)) && Time.time >= nextFireTime)
            {
                nextFireTime = Time.time + fireRate;
                Shoot();
            }
        }
     void Shoot()
        {
            muzzleFlash.Play();

            RaycastHit hit;
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward,out hit, weaponRange)) 
           {
                Debug.Log("Hit" + hit.collider.name);

                Target target = hit.collider.GetComponent<Target>();
                if (target != null)
                {
                    target.TakeDamage(damage);
                }
                if(hitEffect != null)
                {
                    GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(impact,2f);
                }

            }
        }
    }
}
