using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using MonoGame.Extended.Entities;
using Magitech.Systems;
using Magitech.Components;

namespace Magitech
{
    class IItem
    {
        string id { get; set; } //The unique id of the item
        Texture2D texture { get; set; } //The texture/icon of the item
        string displayname { get; set; } //This is the name displayed to the player
        Machine reprMachine { get;set } //If the Item represents a machine in inventory, then this contains that machines info, otherwise should be null
    }
}
