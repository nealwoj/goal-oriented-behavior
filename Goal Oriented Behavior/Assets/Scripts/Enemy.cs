using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private GoalSystem GS;
    private float time;
    public GameObject projectile;

    public int shotCount;
    public float dampingConstant;
    public bool canShoot, timer, reloaded;

    private Text actionText;
    private Text goalText;
    private Text shotText;

    // Start is called before the first frame update
    private void Start()
    {
        GS = GetComponent<GoalSystem>();
        shotCount = 0;
        canShoot = true;
        timer = false;
        time = 0f;
        dampingConstant = 0.98f;

        actionText = GameObject.Find("Action").GetComponent<Text>();
        goalText = GameObject.Find("Goal").GetComponent<Text>();
        shotText = GameObject.Find("Ammo").GetComponent<Text>();

        //GameController.Instance.enemies.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        actionText.text = GS.GetBestAction(GS.GetBestGoal()).type.ToString();
        goalText.text = GS.GetBestGoal().type.ToString();
        shotText.text = shotCount.ToString();

        GS.enemy = gameObject.GetComponent<Enemy>();

        if (timer)
        {
            if (time > 0f)
            {
                time -= Time.deltaTime;
            }
            else
            {
                if (reloaded == false)
                {
                    reloaded = true;
                    shotCount = 0;
                }

                if (canShoot == false)
                    canShoot = true;
                
                time = 0f;
                timer = false;
            }
        }

        GS.UpdateGoalSystem();
    }

    public void Cooldown(float seconds)
    {
        time = seconds;
        timer = true;
    }

    public void OpenFire()
    {
        if (canShoot)
        {
            shotCount++;
            canShoot = false;
            Projectile proj = Instantiate(projectile, transform.position, transform.rotation).GetComponent<Projectile>();
            proj.target = GameObject.Find("Target").gameObject;
            Cooldown(3f);
        }
    }

    public void Reload()
    {
        if (reloaded)
        {
            reloaded = false;
            Cooldown(5f);
        }
    }

    public void Integrate()
    {
        Vector3 velocity = Target.position - transform.position;
        transform.position += velocity * Time.deltaTime * dampingConstant;
    }
}
