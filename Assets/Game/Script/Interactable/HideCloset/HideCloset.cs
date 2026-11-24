using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class HideCloset : MonoBehaviour, Iinteractable
{
    [SerializeField]
    private string _name;

    [SerializeField]
    private Transform _hidePostion;

    [SerializeField]
    private Transform _unhidePostion;

    [SerializeField]
    private float _duration;

    [SerializeField]
    private Door _door;

    private PlayerCharacter _hidingPlayer;
    private Coroutine _hideCorutine;
    private Coroutine _unhideCorutine;

    public string Name => _name;

    public void Interact(PlayerCharacter character)
    {
        if (_hidePostion != null && _unhidePostion != null && _door != null)
        {
            _hidingPlayer = character;
            if (_hideCorutine != null)
            {
                StopCoroutine(_hideCorutine);
            }
            _hideCorutine = StartCoroutine(Hide());
        }
    }

    public IEnumerator Hide()
    {
        _hidingPlayer.SethisHiding(true);
        _hidingPlayer.Camera.SetCameraInputEnable(false);
        _hidingPlayer.Movement.SetEnabled(false);
        _hidingPlayer.InteractDetector.SetEnabled(false);
        _hidingPlayer.Camera.ResetCameraRotation();

        _door.Open();
        yield return new WaitWhile(() => _door._IsAnimating);

        float time = 0f;
        Vector3 startPosition = _hidingPlayer.transform.position;
        float startRotation = _hidingPlayer.Camera.PanAxis;
        while (time < _duration)
        {
            time = time + Time.deltaTime;
            // animasi Posisi
            _hidingPlayer.transform.position = Vector3.Lerp(startPosition, _hidePostion.position, time / _duration);
            // animasi rotation
            float PanAxis = Mathf.Lerp(startRotation, _hidePostion.eulerAngles.y, time / _duration);
            _hidingPlayer.Camera.SetPanAxisValue(PanAxis);
            yield return null;
        }

        _hidingPlayer.transform.position = _hidePostion.position;
        _hidingPlayer.transform.rotation = _hidePostion.rotation;

        _door.Close();

        yield return new WaitWhile(() => _door._IsAnimating);
        _hidingPlayer.Input.OnInteractInput.AddListener(StopHiding);
    }

    public void StopHiding()
    {
        if (_unhideCorutine != null)
        {
            StopCoroutine(_unhideCorutine);
        }
        StartCoroutine(Unhide());
    }

    public IEnumerator Unhide()
    {
        _hidingPlayer.Input.OnInteractInput.RemoveListener(StopHiding);
        _door.Open();
        yield return new WaitWhile(() => _door._IsAnimating);

        float time = 0f;
        Vector3 startPosition = _hidingPlayer.transform.position;
        while (time < _duration)
        {
            time = time + Time.deltaTime;
            // animasi Posisi
             _hidingPlayer.transform.position = Vector3.Lerp(startPosition, _unhidePostion.position, time / _duration);
            // animasi rotation
            float PanAxis = Mathf.Lerp(_hidingPlayer.Camera.PanAxis, _unhidePostion.rotation.y, time / _duration);
            _hidingPlayer.Camera.SetPanAxisValue(PanAxis);
            yield return null;
        }

        _hidingPlayer.transform.position = _unhidePostion.position;
        _hidingPlayer.transform.rotation = _unhidePostion.rotation;

        _door.Close();
        _hidingPlayer.Camera.SetCameraInputEnable(true);
        _hidingPlayer.Movement.SetEnabled(true);
        _hidingPlayer.InteractDetector.SetEnabled(true);
        _hidingPlayer.SethisHiding(false);
        _hidingPlayer = null;

        yield return new WaitWhile(() => _door._IsAnimating);
        
    }
}
