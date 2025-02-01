using System.Collections;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    [System.Serializable]
    public class BallData
    {
        public GameObject ball; 
        public float delay;    
    }

    public BallData[] balls;   
    public float speed = 5f;   

    private Rigidbody[] rigidbodies;
    private bool[] isGrounded; 

    void Start()
    {
        int ballCount = balls.Length;
        rigidbodies = new Rigidbody[ballCount];
        isGrounded = new bool[ballCount];

        // Configurar cada bola
        for (int i = 0; i < ballCount; i++)
        {
            BallData ballData = balls[i];
            rigidbodies[i] = ballData.ball.GetComponent<Rigidbody>();

            // Desativar a gravidade inicialmente
            rigidbodies[i].useGravity = false;

            // Iniciar o atraso para ativar a gravidade
            StartCoroutine(ActivateGravityAfterDelay(i, ballData.delay));
        }
    }

    void Update()
    {
        // Gerenciar o movimento de cada bola
        for (int i = 0; i < balls.Length; i++)
        {
            if (isGrounded[i]) // Só move a bola se estiver no chão
            {
                float moveHorizontal = Input.GetAxis("Horizontal");
                float moveVertical = Input.GetAxis("Vertical");
                Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
                rigidbodies[i].AddForce(movement * speed);
            }
        }
    }

    // Coroutine para ativar a gravidade após o atraso
    IEnumerator ActivateGravityAfterDelay(int index, float delay)
    {
        yield return new WaitForSeconds(delay);
        rigidbodies[index].useGravity = true;
    }

    // Detecta se uma bola está no chão
    void OnCollisionStay(Collision collision)
    {
        for (int i = 0; i < balls.Length; i++)
        {
            if (collision.gameObject == balls[i].ball && collision.gameObject.CompareTag("chao"))
            {
                isGrounded[i] = true;
            }
        }
    }

    // Detecta quando uma bola sai do chão
    void OnCollisionExit(Collision collision)
    {
        for (int i = 0; i < balls.Length; i++)
        {
            if (collision.gameObject == balls[i].ball && collision.gameObject.CompareTag("chao"))
            {
                isGrounded[i] = false;
            }
        }
    }
}
