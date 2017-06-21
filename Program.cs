    using System;
    using Ensage.Common.Menu;
    using Ensage.Common.Objects;
    using Ensage;
    using Ensage.Common;
    using Ensage.SDK.Service.Metadata;
    using Ensage.SDK.TargetSelector;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;
    using Ensage.SDK.Service;
    using Ensage.SDK.TargetSelector.Config;
    using Ensage.SDK.TargetSelector.Metadata;
    using SharpDX;

﻿namespace Manabars
{
    [Hero(ClassId.CDOTA_Unit_Hero_EmberSpirit)]
    internal class Program
    {
        #region Methods
        private static void Drawing_OnDraw(EventArgs args)
        {
            if (!Game.IsInGame)
            {
                return;
            }
            
            var hero = unit as Hero;
            
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
                var start = HUDInfo.GetHPbarPosition(hero) + new Vector2(0, HUDInfo.GetHpBarSizeY(hero) + 1);
                var manaperc = enemy.Mana / enemy.MaximumMana;
                var size = new Vector2(HUDInfo.GetHPBarSizeX(), HUDInfo.GetHpBarSizeY() - 6);
                // Draw background
                Drawing.DrawRect(start, size + new Vector2(0, 0), new Color(150, 0, 50, 150));
                // Draw manabar
                Drawing.DrawRect(start, new Vector2(size.X * manaperc, size.Y), Color.RoyalBlue);
                // Draw frame
                Drawing.DrawRect(start + new Vector2(0, 0), size + new Vector2(0, 0), Color.Black, true);
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
