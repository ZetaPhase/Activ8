using ExaPhaser.WebForms;
using ExaPhaser.WebForms.Controls;
using ExaPhaser.WebForms.Themes;
using SharpJS.Dom;
using SharpJS.Dom.Elements;
using SharpJS.JSLibraries.JQuery;
using System;

namespace ZetaPhase.Activ8
{
    public sealed class RevealButton : TextControl
    {
        #region Private Fields

        private string _text;
        private Element _buttonTriggerElement;
        private DivElement _controlContainerElement;
        private Button _dropDownBtn1;
        private Panel _revealPanel;
        private DivElement _revealArea;
        
        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// The DivElement that will be revealed and hidden when the button is clicked.
        /// This must be initialized through an object initializer.
        /// </summary>
        public DivElement RevealArea { get { return _revealArea; } set { _controlContainerElement.AppendChild(value); _revealArea = value;  } }
        public Panel RevealPanel { get { return _revealPanel; } set { this.Controls.Add(_revealPanel); } }
        bool revealAreaVisible = false;
        bool revealPanelVisible = false;

        public RevealButton()
        {
            switch (WebApplication.CurrentTheme.Stylesheet)
            {
                case CSSFramework.Foundation6:
                    _buttonTriggerElement = new AnchorElement
                    {
                        Class = "button"
                    };
                    break;

                case CSSFramework.PolyUI:
                    _buttonTriggerElement = new AnchorElement
                    {
                        Class = "btn--default"
                    };
                    break;

                case CSSFramework.Kubism:
                    _buttonTriggerElement = new AnchorElement
                    {
                        Class = "btn"
                    };
                    break;

                default:
                    _buttonTriggerElement = new AnchorElement
                    {
                        Class = "button"
                    };
                    break;
            }
            //we'll see
            _dropDownBtn1 = new Button
            {
                Text = "Button",
                Command = new DelegateCommand(()=>JSLibrary.Alert("You have clicked this amazing button")),
            };
            _revealPanel = new Panel();
            _dropDownBtn1.Controls.Add(_revealPanel);

            _controlContainerElement = new DivElement();
            var containerJQElement = new JQElement(_controlContainerElement);
            containerJQElement.Append(_buttonTriggerElement);
            InternalElement = _controlContainerElement;
            _buttonTriggerElement.Click += OnClick; //we are triggering on button click
            PerformLayout();
        }

        #endregion Public Constructors

        #region Public Properties

        public string Text
        {
            get { return _text; }
            set { SetText(value); }
        }

        #endregion Public Properties

        #region Public Events

        public event EventHandler Click;

        #endregion Public Events

        #region Private Methods

        private void OnClick(object sender, EventArgs e)
        {
            if (RevealPanel != null)
            {
                if (revealPanelVisible)
                    _revealPanel.FadeOut();
                else
                    _revealPanel.FadeIn();
                revealPanelVisible = !revealPanelVisible;
            }
            Click?.Invoke(this, e);
            Command?.Execute(new ICommandParameter(e));
        }

        private void SetText(string value)
        {
            _buttonTriggerElement.TextContent = value;
            _text = value;
        }

        #endregion Private Methods

        #region Command

        /// <summary>
        /// The command fired when the button is clicked
        /// </summary>
        /// <value>The command.</value>
        public ICommand Command { get; set; }

        #endregion Command
    }
}