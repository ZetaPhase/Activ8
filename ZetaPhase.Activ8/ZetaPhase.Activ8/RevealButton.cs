using ExaPhaser.WebForms;
using ExaPhaser.WebForms.Controls;
using ExaPhaser.WebForms.Themes;
using SharpJS.Dom;
using SharpJS.Dom.Elements;
using SharpJS.JSLibraries.JQuery;
using System;

namespace ZetaPhase.Activ8
{
	/// <summary>
	/// A clickable rectangle that reveals and hides a Panel when clicked.
	/// </summary>
	public sealed class RevealButton : TextControl
	{
		#region Private Fields

		private string _text;
		private Panel _revealPanel;
		private bool _revealPanelVisible;

		#endregion Private Fields

		#region Public Constructors

		public RevealButton()
		{
			switch (WebApplication.CurrentTheme.Stylesheet)
			{
				case CSSFramework.Foundation6:
					InternalElement = new AnchorElement
					{
						Class = "button"
					};
					break;

				case CSSFramework.PolyUI:
					InternalElement = new AnchorElement
					{
						Class = "btn--default"
					};
					break;

				case CSSFramework.Kubism:
					InternalElement = new AnchorElement
					{
						Class = "btn"
					};
					break;

				default:
					InternalElement = new AnchorElement
					{
						Class = "button"
					};
					break;
			}
			Click += OnClick;
			PerformLayout();
		}

		#endregion Public Constructors

		#region Public Properties

		public override string Text
		{
			get { return _text; }
			set { SetText(value); }
		}

		/// <summary>
		/// The panel that is shown and hidden when the button is clicked
		/// </summary>
		/// <value>The reveal panel.</value>
		public Panel RevealPanel
		{
			get { return _revealPanel; }
			set { SetRevealPanel(value); }
		}

		#endregion Public Properties

		#region Private Methods

		private void OnClick(object sender, EventArgs e)
		{
			if (_revealPanel != null)
			{
				if (_revealPanelVisible)
					_revealPanel.FadeOut();
				else
					_revealPanel.FadeIn();
				_revealPanelVisible = !_revealPanelVisible;
			}
			Command?.Execute(new ICommandParameter(e));
		}

		private void SetRevealPanel(Panel newRevealPanel)
		{
			if (this.Controls.Contains(_revealPanel))
				this.Controls.Remove(_revealPanel);
			_revealPanel = newRevealPanel;
			_revealPanelVisible = newRevealPanel.Visible;
			this.Controls.Add(_revealPanel);
		}

		private void SetText(string value)
		{
			InternalElement.TextContent = value;
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