using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody rigid;
    private AudioSource audioSource;
    [SerializeField] private float mainThrustPower = 1000f;
    [SerializeField] private float rotateThrustPower = 75f;

    [SerializeField] private ParticleSystem mainThruster;
    [SerializeField] private ParticleSystem sideThruster1;
    [SerializeField] private ParticleSystem sideThruster2;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotate();
    }
    private void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }
    
    private void ProcessRotate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }
    }

    private void StartThrusting()
    {
        rigid.AddRelativeForce(Vector3.up * mainThrustPower * Time.deltaTime);
        if (!audioSource.isPlaying)
            audioSource.Play();
        if (!mainThruster.isPlaying)
            mainThruster.Play();
    }

    private void StopThrusting()
    {
        audioSource.Stop();
        mainThruster.Stop();
    }

    private void RotateLeft()
    {
        Rotate(rotateThrustPower);
        if (!sideThruster1.isPlaying)
            sideThruster1.Play();
    }
    private void RotateRight()
    {
        Rotate(-rotateThrustPower);
        if (!sideThruster2.isPlaying)
            sideThruster2.Play();
    }   
    private void StopRotating()
    {
        sideThruster1.Stop();
        sideThruster2.Stop();
    }

    private void Rotate(float force)
    {
        rigid.freezeRotation = true;
        transform.Rotate(Vector3.forward * force * Time.deltaTime);
        rigid.freezeRotation = false;
    }
}
