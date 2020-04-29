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

namespace Magitech.Components
{
    interface IMachineType
    {
        string typeName { get; set; } //The unique name of the machine type
        bool isOn { get; set; }
        void Update(Machine machineobj);
    }
}
