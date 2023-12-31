using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scale : MonoBehaviour
{
    public static Scale scale;
    [SerializeField] Ulti _ulti;
    private bool _statusLaser = false;
    [SerializeField] GameObject _laserGO;
    [SerializeField] CameraController _camera;
   // [SerializeField] GameObject _sphere;

    public bool isCurrentUlti = false;

    private void Start()
    {
        _laserGO.SetActive(false);
        //_sphere.SetActive(false);
    }
    private void Update()
    {
        if (!isCurrentUlti)
        {
            StopUlti();
        }
    }

    public void LaunchUlti()
    {
        isCurrentUlti = true;

        _laserGO.SetActive(true);
        //_sphere.SetActive(true);

        StartCoroutine("LaserStart");
        _statusLaser = true;
        StartCoroutine("CurrentUlti");
        
    }

    public void StopUlti()
    {
        StartCoroutine("LaserStop");
        _statusLaser = false;
        _laserGO.SetActive(false);
       // _sphere.SetActive(false);

    }
    IEnumerator LaserStart()
    {
            StartCoroutine("Extend", 0.01F);
            yield return new WaitForSeconds(1);
            StopCoroutine("Extend");
    }

    IEnumerator LaserStop()
    {
        StartCoroutine("Retract", 2.0F);
        yield return new WaitForSeconds(1f);
        StopCoroutine("Retract");
    }


    IEnumerator Extend(float someParameter)
    {
        while (true)
        {
            _camera.shakeshake = true;
            transform.localScale += new Vector3(0, 0.3f, 0);
            yield return null;
        }
    }

    IEnumerator Retract(float someParameter)
    {
        //while (true)
        //{
        //    transform.localScale -= new Vector3(0, 0.1f, 0);
        //    yield return null;
        //}
        transform.localScale = new Vector3(1,1,1);
        _ulti.flashLaser.Stop();
        _ulti.sparksLaser.Stop();

        _ulti.ultPartSysteme1.Stop();
        _ulti.ultPartSysteme2.Stop();
        _ulti.ultPartSysteme3.Stop();
        _ulti.ultPartSysteme4.Stop();
        _ulti.ultPartSysteme5.Stop();

        _ulti.ultiPostProcess.weight = 0;
        _ulti.radiusGarlic = 0;
        _ulti.garlicParticules.Stop();
        _ulti.ultiLight.SetActive(false);
        yield return null;
    }

    IEnumerator CurrentUlti()
    {
        yield return new WaitForSeconds(1.5f);
        isCurrentUlti = false;
        _camera.shakeshake = false;
    }
}
