using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
namespace ZTools
{
    [RequireComponent(typeof(Image), typeof(Button))]
    public class Tab : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("给按钮设置标签")]
        public string tabName;
        [SerializeField]
        [Header("是否选中")]
        private bool isOn = false;
        [SerializeField]
        [Header("是否可以多选")]
        private bool multipleChoice = false;
        [Header("改变图片显示状态")]
        [Header("---------------鼠标移入状态设置----------------")]
        public bool isHoverImgActive;
        public GameObject hoverImg;
        [Header("改变图片显示颜色")]
        public bool isHoverImgColor;
        [Header("改变文字显示颜色")]
        public bool isHoverTxtColor;
        public Text tabTxt;
        public Text isOnTabTxt;
        [SerializeField]
        [Header("改变图片显示状态")]
        [Header("---------------选中时状态设置----------------")]
        private bool isOnImgActive = false;
        [SerializeField]
        [Header("改变文字显示颜色")]
        private bool isOnTxtColor = false;
        [SerializeField]
        [Header("改变图片显示颜色")]
        private bool isOnImgColor = false;
        [Header("父级控制器，可手动配置")]
        [Header("---------------------------------------------")]
        public TabController tabcontroller = null;
        public Image tabImg;
        public Image tabChangeColorImg;
        public Image hoverImage;
        public Color changeTxtColor;
        public Color hoverTxtColor;
        public Color imgColor;
        public Color hoverImageColor;

        public List<GameObject> showPanel;

        private Button btnTab;
        public TabController tabController
        {
            set
            {
                tabcontroller = value;
                tabController.AddTab(this);
            }
            get { return tabcontroller; }
        }
        private Color origTxtColor;
        private Color origIsOnTxtColor;
        private Color origImgColor;
        private Color origHoverImgColor;
        /// <summary>
        /// 点击事件，选中状态再次点击有效
        /// </summary>
        public event Action<Tab> TabClick;
        /// <summary>
        /// 点击事件，选中状态再次点击无效
        /// </summary>
        public event Action<Tab> TabClickFirstEffect;
        private bool isInit = false;
        private int hash;

        public bool IsOn
        {
            set
            {
                if (isOn != value && value)
                    if (TabClickFirstEffect != null)
                        TabClickFirstEffect.Invoke(this);
                ChangeType(value);
                if (isOn)
                    if (TabClick != null)
                        TabClick.Invoke(this);
            }
            get { return isOn; }
        }
        public bool IsOnWithOutEvent
        {
            set
            {
                ChangeType(value);
            }
        }

        void Awake()
        {
            btnTab = this.GetComponent<Button>();
            btnTab.transition = Selectable.Transition.None;
            hash = this.GetHashCode();
            if (!isInit)
            {
                InitData();
            }
            if (isOn)
            {
                isOn = false;
                IsOn = true;
            }
            btnTab.onClick.AddListener(() =>
            {
                IsOn = (!multipleChoice) ? true : !IsOn;
            });
        }
        /// <summary>
        /// 保存当前状态
        /// </summary>
        void InitData()
        {
            isInit = true;
            if (isHoverTxtColor)
            {
                if (null != tabTxt)
                {
                    origTxtColor = tabTxt.color;
                }
                else
                {
                    Debug.LogError("tabTxt is null");
                }
            }
            if (isOnTxtColor)
            {
                if (null != isOnTabTxt)
                {
                    origIsOnTxtColor = isOnTabTxt.color;
                }
                else
                {
                    Debug.LogError("isOnTabTxt is null");
                }
            }
            if (isOnImgColor)
            {
                if (null != tabChangeColorImg)
                {
                    origImgColor = tabChangeColorImg.color;
                }
                else
                {
                    Debug.LogError("tabChangeColorImg is null");
                }
            }
            if (isHoverImgColor)
            {
                if (null != hoverImage)
                {
                    origHoverImgColor = hoverImage.color;
                }
                else
                {
                    Debug.LogError("hoverImage is null");
                }
            }
            if (!multipleChoice)
            {
                if (null == tabController)
                {
                    tabController = (this.transform.parent.GetComponent<TabController>() == null) ? this.transform.parent.gameObject.AddComponent<TabController>() : this.transform.parent.GetComponent<TabController>();
                }
                else
                {
                    tabController.AddTab(this);
                }
            }
        }
        /// <summary>
        /// 改变选择的状态
        /// </summary>
        /// <param name="value"></param>
        void ChangeType(bool value)
        {
            if (isOn == value) return;
            isOn = value;
            if (isHoverImgActive)
            {
                hoverImg.gameObject.SetActive(false);
            }
            if (isHoverImgColor)
            {
                hoverImage.color = origHoverImgColor;
            }
            if (isHoverTxtColor)
            {
                tabTxt.color = origTxtColor;
            }
            if (isOnTxtColor)
            {
                isOnTabTxt.color = (value) ? changeTxtColor : origIsOnTxtColor;
            }
            if (isOnImgActive)
            {
                tabImg.gameObject.SetActive(value);
            }
            if (isOnImgColor)
            {
                tabChangeColorImg.color = (value) ? imgColor : origImgColor;
            }
            if (showPanel.Count > 0)
            {
                showPanel.ForEach(item =>
                {
                    if (item != null)
                        item.SetActive(value);
                });
            }
            if (value && !multipleChoice)
            {
                tabController.ChoiceTab(hash);
            }
        }
        /// <summary>
        /// 监听改变的状态
        /// </summary>
        /// <param name="txtName"></param>
        public void ChangeTab(int tabHash)
        {
            if (tabHash != hash)
                IsOn = false;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (isHoverImgActive && !isOn)
            {
                hoverImg.SetActive(true);
            }
            if (isHoverTxtColor && !isOn)
            {
                tabTxt.color = hoverTxtColor;
            }
            if (isHoverImgColor && !isOn)
            {
                hoverImage.color = hoverImageColor;
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (isHoverImgActive && !isOn)
            {
                hoverImg.SetActive(false);
            }
            if (isHoverTxtColor && !isOn)
            {
                tabTxt.color = origTxtColor;
            }
            if (isHoverImgColor && !isOn)
            {
                hoverImage.color = origHoverImgColor;
            }
        }
        private void OnDestroy()
        {
            if (tabController != null)
            {
                tabController.RemoveTab(this);
            }
        }
    }
}
