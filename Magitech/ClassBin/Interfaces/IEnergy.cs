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


namespace Magitech
{
    interface  IEnergy
    {
        string id { get; set; } //The unique id for this type of energy
        Color reprColor { get;set } //The color used to represent this energy type
    }
}
