using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSwing : AttackMove
{
    public float swingSpeed = 5.0f;
    public float swingDistance = 90.0f;
    public float swingDelay = 1.0f;
    public float swingReturnSpeed = 6.0f;
    public float swingReturnDelay = 0.1f;
    public override float damage { 
        get { return 1.5f; }
        set {}
    }
    public override float knockbakPower {
        get { return 2.0f; }
        set {}
    }

    public override int pierce {
        get { return 10; }
        set {}
    }

    public override HitType hitType {
        get { return HitType.OnCollision; }
        set {}
    }

    public override float refreshRate {
        get { return 0.1f; }
        set {}
    }

    // Start is called before the first frame update
    void Start()
    {
        // StartCoroutine(Swing(null));
    }

    public override void Initialize(Vector3 position, Quaternion rotation, GameObject player)
    {
        this.user = player;
        // create empty game object
        GameObject swordContainer = new GameObject();
        // set position to player position
        swordContainer.transform.position = position;
        // set scale to player scale
        // swordContainer.transform.localScale = player.transform.localScale;
        // define sword position offset to show on player's hand
        Vector3 swordOffset = new Vector3(0.0f, 0.0f, 0.0f);
        // set sword parent to sword container
        transform.parent = swordContainer.transform;
        // set sword position to sword offset
        transform.localPosition = swordOffset;
        // set sword scale to 1
        transform.localScale = new Vector3(1, 1, 1);
    }

    override public IEnumerator Attack(Entity user, Entity target, Vector3 direction)
    {
        this.user = user.gameObject;
        // rotate sword to aim direction
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(-user.aimDirection.y, -user.aimDirection.x) * Mathf.Rad2Deg);
        // swing
        for (int i = 0; i < swingDistance/swingSpeed; i++)
        {
            transform.Rotate(0, 0, swingSpeed);
            yield return null;   
        }

        for (int i = 0; i < swingDistance/swingReturnSpeed; i++)
        {
            transform.Rotate(0, 0, -swingReturnSpeed);
            yield return null;   
        }
        // destroy parent
        Destroy(transform.parent.gameObject);
    }

    // Update is called once per frame
    public override void OnUpdate()
    {
        // set sword parent position and scale to player
        transform.parent.position = user.transform.position;
        // transform.parent.localScale = player.transform.localScale;
        // transform.position = player.transform.position;
    }

}
