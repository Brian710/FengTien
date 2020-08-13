using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace MinYanGame.Core
{
    public class TutoSceneManager : MonoBehaviour
    {
        public Animator FirCanv;
        public GameObject TutoTP;
        public Animator SecCanv;
        public GameObject GrabTuto;

        [Header("Tutorial Btn")]
        [SerializeField]
        private Button restart01Btn;
        [SerializeField]
        private Button restart02Btn;
        [SerializeField]
        private Button menuBtn;
        [SerializeField]
        private Button confirmBtn;

        [Header("Tutorial Img")]
        public Animator[] tutorialImgs;



        private void Start()
        {
            restart01Btn.onClick.AddListener(Set);
            restart02Btn.onClick.AddListener(Set);
            confirmBtn.onClick.AddListener(ConfirmBtn);
            Set();
        }

        public void Set()
        {
            if (GameController.Instance.gameState != GameState.TutoInit)
                GameController.Instance.gameState = GameState.TutoInit;
            FirCanv.gameObject.SetActive(true);
            TutoTP.SetActive(false);
            SecCanv.gameObject.SetActive(false);
            GrabTuto.SetActive(false);
            StartCoroutine(SetCanvVisiable(FirCanv, true));
            StartCoroutine(GotoFirstStep());
        }
        
        private void ConfirmBtn()
        {
            StartCoroutine(SetCanvVisiable(tutorialImgs[0], false));
            StartCoroutine(SetCanvVisiable(tutorialImgs[1], true));
        }

        public void GotoThirdStep()
        {
            StartCoroutine(SetCanvVisiable(SecCanv, true));
            StartCoroutine(SetCanvVisiable(tutorialImgs[2], true));
            StartCoroutine(SetCanvVisiable(tutorialImgs[3], false));
        }
        public void GotoForthStep()
        {
            StartCoroutine(SetCanvVisiable(tutorialImgs[2], false));
            StartCoroutine(SetCanvVisiable(tutorialImgs[3], true));
        }

        private IEnumerator GotoFirstStep()
        {
            yield return 3f;
            StartCoroutine(SetCanvVisiable(tutorialImgs[0], true));
            StartCoroutine(SetCanvVisiable(tutorialImgs[1], false));
        }
        private IEnumerator SetCanvVisiable(Animator transition,bool value)
        {
            transition.SetBool("End", !value);
            yield return 1f;
        }
        
    }
}
