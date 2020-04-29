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
    class MachineBasicMiner : IMachineType
    {
        public string typeName { get; set; }
        public bool isOn { get; set; }

        public void Update(Machine machineobj)
        {
            throw new NotImplementedException();
        }
    }
}
