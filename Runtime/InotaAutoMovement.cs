using System;
using UnityEngine;
using UnityEngine.AI;

public class InotaAutoMovement : MonoBehaviour
{
    [SerializeField] private Transform[] targets; // robotun gitmesi gereken hedefleri temsil eder. Devriye görevleri için kullanılabilir.
    private int counter = 0;
    private int targetNumber = 0;
    private int showDestinationPoint = 0;
    public bool isArrived = false;
    public float _targetDistance;
    public float robotSpeed = 0.5f;

    private Quaternion _lookRotation;
    private Vector3 _direction;
    public float RotationSpeed= 0.01f;

    //private float delay = 0.01f;
    //private float time = 0.01f;
    private NavMeshAgent agent;
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        _direction = (targets[counter].position - transform.position).normalized;
        _lookRotation = Quaternion.LookRotation(_direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);
    }

    // Update is called once per frame
    void Update()
    {

        agent.destination = targets[counter].position;  //agent için varış noktası hedef olarak ayarlanır.
        _targetDistance = Vector3.Distance(transform.position, agent.destination);  //Şu anki konum ve hedef arası uzaklık hesabı yapılır.
        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);
        
        if (_targetDistance <= 0.001)  // Dist 0.001 thresholdundan düşükse hedefe ulaşmış demektir. Yeni hedef belirlenebilir.
        {
            isArrived = true;  // Hedefe ulaşıldı.
            agent.destination = transform.position;
            agent.velocity = Vector3.zero;
            agent.speed = 0;
            agent.isStopped = true;

            // Eğer başka hedef varsa yeni hedef set edilir ve yön o tarafa çevrilir.
            if (counter != targets.Length - 1)
            {
                Debug.Log("Target Distance:" + _targetDistance);

                targetNumber = counter + 1;
                Debug.Log("Target:" + targetNumber);

                counter++;
                _direction = (targets[counter].position - transform.position).normalized;
                _lookRotation = Quaternion.LookRotation(_direction);
                Debug.Log("Turning towards new target");
            }
            else
            {
                showDestinationPoint += 1;
                if (showDestinationPoint == 1)
                {
                    Debug.Log("Target Distance:" + _targetDistance);
                    if (counter==targets.Length-1){Debug.Log("INOTA reached all targets!");}
                }
            }
        }
        else
        {
            isArrived = false;
            agent.isStopped = false;   
            if (Math.Abs(_lookRotation[3] - transform.rotation[3]) < 0.02 && Math.Abs(_lookRotation[1] - transform.rotation[1]) < 0.02)
            {
                // _lookRotation ve transform.rotation birbirine eşit olduğunda artık robot tekrar hareket edebilir. 
                // (Kayma kontrolü bu kod parçası ile yapıldı.)
                agent.speed = robotSpeed;
            }
        }
    }
}
