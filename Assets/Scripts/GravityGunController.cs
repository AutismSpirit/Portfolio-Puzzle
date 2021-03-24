using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Simple gameplay code to get a physics gun-type behaviour. Uses the IUsable interface, which lets it be picked up and fired by the player.
/// </summary>
public class GravityGunController : Weapon, IUsable
{

    public GameObject holder;

    private Camera cam;
    private Rigidbody rb;
    private FixedJoint joint;
    private Animator anim;

    [SerializeField] float scrollSpeed;

    public bool HasItem
    {
        get
        {
            return joint.connectedBody != null ? true : false;
        }
    }

    public void Awake()
    {
        cam = Camera.main;

        anim = GetComponent<Animator>();

        rb = holder.GetComponent<Rigidbody>();
        rb.detectCollisions = false;

        joint = holder.GetComponent<FixedJoint>();
    }

    public void Use()
    {
        OnFire();
    }
    public void RightClick()
    {
        OnRightClick();
    }

    public override void OnFire()
    {
        base.OnFire();

        RaycastHit hit;

        if (Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out hit, range))
        {
            Rigidbody hitRB;
            hit.transform.gameObject.TryGetComponent(out hitRB);

            if (hitRB != null)
            {
                joint.transform.position = hit.point;
                joint.connectedBody = hitRB;

                anim.SetBool("shooting", true);
            }
        }
    }

    public override void OnRightClick()
    {
        base.OnRightClick();

        if (HasItem)
        {
            joint.connectedBody = null;
            joint.transform.localPosition = Vector3.zero;

            anim.SetBool("shooting", false);
        }
    }

    public void OnItemChange()
    {
        //do item change animation
    }

    public void Update()
    {
        if (Input.mouseScrollDelta.y > 0)
        {
            joint.transform.localPosition += cam.transform.TransformDirection(Vector3.forward) * scrollSpeed * Time.deltaTime;
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            joint.transform.localPosition -= cam.transform.TransformDirection(Vector3.forward) * scrollSpeed * Time.deltaTime;
        }
    }
}
