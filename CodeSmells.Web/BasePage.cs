namespace CodeSmells.Web
{
    using System.Web.UI;
    using Data;

    public class BasePage : Page
    {
        protected BasePage()
            : this(new CodeSmellsData())
        {
        }

        protected BasePage(ICodeSmellsData data)
        {
            this.Data = data;
        }

        protected ICodeSmellsData Data { get; set; }
    }
}