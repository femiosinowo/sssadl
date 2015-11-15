namespace Ektron.Cms.Workarea
{
    /// <summary>
    /// <para>Provides a common baseline of functionality for all pages
    /// in the Workarea.</para>
    /// <para>Features: localization.</para>
    /// </summary>
    public class Page : System.Web.UI.Page
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Page" /> class with
        /// default values.
        /// </summary>
        public Page()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Page" /> class with
        /// the specified value for its AllowAnonymous property.
        /// </summary>
        /// <param name="allowAnonymous">Value to assign to AllowAnonymous property.</param>
        public Page(bool allowAnonymous)
            : this()
        {
            this.AllowAnonymous = allowAnonymous;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to allow anonymous users
        /// load the page.
        /// </summary>
        /// <value>
        /// <c>true</c> to not validate the user's login; otherwise, <c>false</c>.
        /// </value>
        public bool AllowAnonymous { get; set; }

        /// <summary>
        /// Sets the .NET Culture and UICulture based on the user language
        /// for any inheriting page.
        /// </summary>
        protected override void InitializeCulture()
        {
            base.InitializeCulture();

            Ektron.Cms.Localization.LocaleData data = CommonApi.Current.GetRequestedLocale(CommonApi.Current.UserLanguage);
            if (data != null)
            {
                System.Globalization.CultureInfo currentCulture = Ektron.Cms.Common.EkFunctions.GetCultureInfo(data.Culture);
                System.Globalization.CultureInfo currentUICulture =
                    data.Culture == data.UICulture ? currentCulture : Ektron.Cms.Common.EkFunctions.GetCultureInfo(data.UICulture);
                //this.Culture = currentCulture.Name;
                this.UICulture = currentUICulture.Name;
            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Load" /> event and
        /// validates the user login if AllowAnonymous is false.
        /// </summary>
        /// <param name="e">The <see cref="T:System.EventArgs" /> object that contains the event data.</param>
        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);

            if (!this.AllowAnonymous)
            {
                Utilities.ValidateUserLogin();
            }
        }
    }
}