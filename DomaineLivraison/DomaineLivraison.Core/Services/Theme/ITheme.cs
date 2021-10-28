using System.Drawing;

namespace DomaineLivraison.Core.Services.Theme
{
    public interface ITheme
    {
        void SetStatusBarColor(Color color, bool darkStatusBarTint);
    }
}
