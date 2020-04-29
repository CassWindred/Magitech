﻿using Microsoft.Xna.Framework;
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
    class HexBlock //This represents a block of hexes, machines will sit on these allowing them to cross multiple hexes
    {
        HexBase[] hexes;
    }
}
