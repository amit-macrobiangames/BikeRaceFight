using UnityEngine;
using System.Collections;

public class DamageManager : MonoBehaviour
{
    public AudioClip[] HitSound;
    public GameObject Effect;
    public int HP = 10;

    private void Start()
    {

    }

    public void ApplyDamage(int damage)
    {
		print (damage);
		if(HP<0)
		return;
	
        if (HitSound.Length > 0)
        {
            AudioSource.PlayClipAtPoint(HitSound[Random.Range(0, HitSound.Length)], transform.position);
        }
        HP -= damage;
        if (HP <= 0)
        {
            Dead();
        }
    }

    private void Dead()
    {
        if (Effect){
            Instantiate(Effect, transform.position, transform.rotation);
		}
		//PlayerPrefs.SetInt("cash",PlayerPrefs.GetInt("cash",0)+ 25);
       // Destroy(this.gameObject);
    }

}
