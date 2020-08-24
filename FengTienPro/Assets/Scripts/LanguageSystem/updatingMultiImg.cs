using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MinYan.Lang
{
    [RequireComponent(typeof(Image))]
    public class updatingMultiImg: MonoBehaviour
    {
        public Sprite EnImg;
        public Sprite ChImg;
        public Image thisImage;

        public void Set()
        {
            thisImage.sprite = ChangeSprite(GameController.Instance.language);
        }
        public Sprite ChangeSprite(Language language)
        {
            if (ChImg == null || EnImg == null)
            {
                Debug.LogWarning("Sprite missing!");
                return null;
            }
            return language == Language.Chinese ? ChImg : EnImg;
        }
    }
}

