using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameMode : MonoBehaviour
{
    [SerializeField]
    private float _firstWaveStart = 5.0f;

    [SerializeField]
    private float _waveStartFrequency = 15.0f;

    [SerializeField]
    private float _waveEndFrequency = 7.0f;

    [SerializeField]
    private float _waveFrequencyIncrement = 0.5f;

    [SerializeField] GameObject _ghostRed;
    [SerializeField] GameObject _ghostBlue;
    [SerializeField] GameObject _ghostGreen;
    [SerializeField] GameObject _ghostYellow;

    private float _currentFrequency = 0.0f;

    private void Awake()
    {
        _currentFrequency = _waveStartFrequency;

        Invoke(STARTNEWWAVE_METHOD, _firstWaveStart);
        MineSpawnManager.Instance.SpawnMine();
    }

    const string STARTNEWWAVE_METHOD = "StartNewWave";

    void StartNewWave()
    {
        SpawnManager.Instance.SpawnWave();

        _currentFrequency = Mathf.Clamp(_currentFrequency - _waveFrequencyIncrement, _waveEndFrequency, _currentFrequency);

        Invoke(STARTNEWWAVE_METHOD, _currentFrequency);
    }

    public void ReloadScene()
    {
        StartCoroutine(reloadSceneCoroutine());
    }

    private IEnumerator reloadSceneCoroutine()
    {
        yield return new WaitForSeconds(5.0f);

        List<GameObject> players = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));

        foreach(GameObject player in players)
        {
            var rb = player.GetComponent<Rigidbody>();
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;



            switch (player.layer)
            {
                case 6:
                    Instantiate(_ghostRed, player.transform.position, player.transform.rotation);
                    rb.position = GameObject.Find("P1").transform.position;
                    rb.rotation = GameObject.Find("P1").transform.rotation;
                    break;
                case 7:
                    Instantiate(_ghostBlue, player.transform.position, player.transform.rotation);
                    rb.position = GameObject.Find("P2").transform.position;
                    rb.rotation = GameObject.Find("P2").transform.rotation;
                    break;
                case 8:
                    Instantiate(_ghostGreen, player.transform.position, player.transform.rotation);
                    rb.position = GameObject.Find("P3").transform.position;
                    rb.rotation = GameObject.Find("P3").transform.rotation;
                    break;
                case 9:
                    Instantiate(_ghostYellow, player.transform.position, player.transform.rotation);
                    rb.position = GameObject.Find("P4").transform.position;
                    rb.rotation = GameObject.Find("P4").transform.rotation;
                    break;
            }

        }

        //FindFirstObjectByType<PlayerSelectedSpriteManager>().ShowEndScreen();

        yield return new WaitForSeconds(3.0f);

        foreach (GameObject player in players)
        {
            player.GetComponent<CurlingStoneController>().IsStoppable = false;
            player.GetComponent<PlayerInput>().ActivateInput();
            player.GetComponent<Rigidbody>().linearVelocity = Vector3.forward;
        }

        MineSpawnManager.Instance.SpawnMine();
    }
}
