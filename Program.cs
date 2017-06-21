namespace Manabars
{
    using System;
    using System.Linq;
using Ensage.Common.Menu;
using Ensage.Common.Objects;
    using Ensage;
    using Ensage.Common;

    using SharpDX;

    internal class Program
    {
        #region Methods
        private static float DigSize => (float)Members.Menu.Item("manaBars.Nums.Size").GetValue<Slider>().Value / 100;
        private static float ManaBarSize => (float) Members.Menu.Item("manaBars.Size").GetValue<Slider>().Value/100;
        private static void Drawing_OnDraw(EventArgs args)
        {
            if (!Game.IsInGame)
            {
                return;
            }
            var player = ObjectManager.LocalPlayer;
            if (player == null || player.Team == Team.Observer)
            {
                return;
            }

            var enemies =
                ObjectManager.GetEntities<Hero>()
                    .Where(x => x.IsVisible && x.IsAlive && x.MaximumMana > 0 && !x.IsIllusion && x.Team != player.Team)
                    .ToList();
            foreach (var enemy in enemies)
            {
                var start = HUDInfo.GetHPbarPosition(enemy) + new Vector2(0, HUDInfo.GetHpBarSizeY(enemy) - 2);
                var manaperc = enemy.Mana / enemy.MaximumMana;
                var size = new Vector2(HUDInfo.GetHPBarSizeX(), HUDInfo.GetHpBarSizeY()*ManaBarSize);
                // Draw background
                Drawing.DrawRect(start, size + new Vector2(1, 1), new Color(0, 0, 50, 150));
                // Draw manabar
                Drawing.DrawRect(start, new Vector2(size.X * manaperc, size.Y), Color.RoyalBlue);
                // Draw frame
                Drawing.DrawRect(start + new Vector2(-1, -1), size + new Vector2(3, 3), Color.Black, true);
                // Draw text
                var text = string.Format("{0} / {1}", (int)enemy.Mana, (int)enemy.MaximumMana);
                var textSize = Drawing.MeasureText(text, "Arial", new Vector2(size.Y * 2, size.X), FontFlags.AntiAlias);
                var textPos = start + new Vector2(size.X / 2 - textSize.X / 2, -textSize.Y / 2 + 2);
            }
        }

        private static void Main()
        {
            Drawing.OnDraw += Drawing_OnDraw;
        }

        #endregion
    }
}
