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


namespace Magitech.Components
{
    class Machine
    {
        string id;
        string displayName;
        Texture2D onTexture; //Texture for when the machine is on
        Texture2D offTexture; //Texture for when the machine is off
    }
}
