using System;
using System.Collections;
using System.Globalization;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Assets.Scripts.Demos.UI
{
    public class ButtonsController : MonoBehaviour
    {
        [SerializeField] private ButtonsView buttonsView;
        private float _timer;
        private CoroutineType _currentCoroutine;
        private bool _started;
        private Color _defaultColor = Color.yellow;
        private Color _startedColor = Color.red;
        private Color _leadingColor = Color.green;

        private void Awake()
        {
            buttonsView.OnClick += ChangeState;
            Stop();
            ResetTimer();
        }

        private void ChangeState()
        {
            if (_started)
            {
                Stop();
            }
            else
            {
                StartCoroutine(Coroutine1());
                StartCoroutine(Coroutine2());
                buttonsView.SetButtonText("Stop");
                PickRandom();
            }
            
            _started = !_started;
        }

        private void Stop()
        {
            buttonsView.SetText1("0");
            buttonsView.SetText2("0");
            buttonsView.SetButtonText("Start");
            buttonsView.SetIndicator1Color(_defaultColor);
            buttonsView.SetIndicator2Color(_defaultColor);
            ResetTimer();
        }

        private IEnumerator Coroutine1()
        {
            Debug.Log("First coroutine started", this);
            yield break;
        }

        private IEnumerator Coroutine2()
        {
            Debug.Log("Second coroutine started", this);
            yield break;
        }

        private void Update()
        {
            if (_started)
            {
                if (_timer > 0)
                {
                    _timer -= Time.deltaTime;
                    SelectCoroutine();
                }
                else
                {
                    SwapCoroutines();
                }
            }
        }

        private void SelectCoroutine()
        {
            switch (_currentCoroutine)
            {
                case CoroutineType.First:
                    buttonsView.SetText1(_timer.ToString(CultureInfo.InvariantCulture));
                    buttonsView.SetIndicator1Color(_leadingColor);
                    buttonsView.SetIndicator2Color(_startedColor);
                    Log();
                    break;
                case CoroutineType.Second:
                    buttonsView.SetText2(_timer.ToString(CultureInfo.InvariantCulture));
                    buttonsView.SetIndicator1Color(_startedColor);
                    buttonsView.SetIndicator2Color(_leadingColor);
                    Log();
                    break;
            }
        }

        private void SwapCoroutines()
        {
            PickRandom();
            switch (_currentCoroutine)
            {
                case CoroutineType.First:
                    buttonsView.SetText1(0.ToString());
                    buttonsView.SetText2(0.ToString());
                    buttonsView.SetIndicator1Color(_leadingColor);
                    buttonsView.SetIndicator2Color(_defaultColor);
                    ResetTimer();
                    StartCoroutine(Coroutine2());
                    Log();
                    break;
                case CoroutineType.Second:
                    buttonsView.SetText1(0.ToString());
                    buttonsView.SetText2(0.ToString());
                    buttonsView.SetIndicator1Color(_defaultColor);
                    buttonsView.SetIndicator2Color(_leadingColor);
                    ResetTimer();
                    StartCoroutine(Coroutine1());
                    Log();
                    break;
            }
        }

        private void PickRandom() => _currentCoroutine = (CoroutineType)Random.Range(0, Enum.GetNames(typeof(CoroutineType)).Length);

        private void ResetTimer() => _timer = Random.Range(10, 21);

        private void Log()
        {
            switch (_currentCoroutine)
            {
                case CoroutineType.First:
                    Debug.Log("First coroutine is leading", this);
                    break;
                case CoroutineType.Second:
                    Debug.Log("Second coroutine is leading", this);
                    break;
            }
        }

        private enum CoroutineType : byte
        {
            First = 0,
            Second = 1
        }

        private void OnDestroy() => buttonsView.OnClick -= ChangeState;
    }
}