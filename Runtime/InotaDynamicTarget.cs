using UnityEngine;
using UnityEngine.AI;
public class InotaDynamicTarget : MonoBehaviour
{
    public Vector3 _targetPosition = new Vector3 (0.0f, 0.0f, 0.0f);
    private NavMeshAgent agent;
    public float _targetDistance;
    public bool _autoMove = false;
    private InotaAutoMovement autoMovement;
    public float robotSpeed = 3;
    private Quaternion _lookRotation;
    private Vector3 _direction;
    public float rotationSpeed = 0.01f;

    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        autoMovement = this.GetComponent<InotaAutoMovement>();
        Debug.Log("For Auto Movement mode, press 'A'.\nFor disable Auto Movement mode, press 'R'.\n");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Auto Movement mode is active. Target: "+_targetPosition.x+", "+_targetPosition.y+", "+_targetPosition.z+")");
            //  A'ya basıldığı için AutoMovement scriptini disable ediyoruz.
            autoMovement.enabled = false;
            _autoMove = false;
            
            //  Bir targeta giderken yeni bir target olusturuldugunda hemen durmasi ve yeni targeta dogru donmesi icin.
            agent.destination = transform.position;
            agent.velocity = Vector3.zero;
            agent.speed = 0;

            if (_targetPosition != new Vector3 (0.0f,0.0f,0.0f))
            {
                //  Agent'i durduruyoruz ve yeni targetimiz dışarıdan verilen _targetPosition oluyor.
                //  Sonra robotun yüzünü hedefe dönmesi için yön ve dönme açısı belirliyoruz.
                agent.isStopped = true;
                agent.destination = _targetPosition;
                _direction = (_targetPosition - transform.position).normalized;
                _lookRotation = Quaternion.LookRotation(_direction);
            }
        }
        //  Mevcut konumun hedefe olan uzakligi olculuyor.
        _targetDistance = Vector3.Distance(transform.position, agent.destination);

        //  Belirledigimiz yon ve donme acisi ve hizina gore robot donuyor.
        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * rotationSpeed);

        //  Robot dondukten sonra agent'i enable edip robotun hedefe dogru hareket etmesini sagliyoruz.
        agent.isStopped = false;
        agent.speed = robotSpeed;
        
        //  E�er R butonundan bir interupt gelirse AutoMovement scriptine gidecek.
        if (Input.GetKeyDown(KeyCode.R))
        {
            //  Interpt geldigi an robotun hemen durmasi icin agent'in hizini sifirliyoruz.
            Debug.Log("Auto Movement mode is disabled. Robot moves decided targets before.");
            agent.destination = transform.position;
            agent.velocity = Vector3.zero;
            agent.speed = 0;
            agent.isStopped = true;
            
            //  AutoMovement scriptinin icine gidiyoruz. Bu durumda mouse'tan bir interupt gelmedigi surece buranin icinde kalacak.
            _autoMove = true;
            autoMovement.enabled = true;
        }
    }
}
