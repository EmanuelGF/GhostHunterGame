using Stride.Engine;
using Stride.UI;
using Stride.UI.Controls;

namespace RpgGame.StartMenu
{
    public class ExitButtonClick : StartupScript
    {

        public override void Start()
        {
            var page = Entity.Get<UIComponent>().Page;

            var button = page.RootElement.FindVisualChildOfType<Button>("ExitButton");
            button.Click += (sender, args) => GameProfiler.Game.Exit();
        }
    }
}
