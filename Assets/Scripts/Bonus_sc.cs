using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus_sc : MonoBehaviour
{
    [SerializeField]
    private float speed = 3.0f;

    [SerializeField]
    private int bonusId;

    [SerializeField]
    private AudioClip audioClip;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (transform.position.y < -5.8f) {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        //Bonus yakalandı
        if (other.tag == "Player") {

            //Player scriptini elde et
            Player_sc player = other.transform.GetComponent<Player_sc>();
            //Bonus yakalama ses efektini çal
            AudioSource.PlayClipAtPoint(audioClip, transform.position);
            //Player scriptinin içindeki bonus aktifleştirme fonksiyonunu çağır
            if (player != null) {

                switch(bonusId)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.SpeedBonusActive();
                        break;
                    case 2:
                        player.ShieldBonusActive();
                        break;
                    default:
                        Debug.Log("Hatalı Bonus ID'si");
                        break;
                }
                
                
            }
            Destroy(this.gameObject);
        }
    }
}
