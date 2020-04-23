using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using Magitech.Components;
using Microsoft.Xna.Framework.Input;

namespace Magitech.Systems
{
    class PlayerMovementSystem : EntityUpdateSystem


    {
        Keybinds _keybinds;
        ComponentMapper<FreePosition> _freePositionComponentMapper;
        ComponentMapper<Player> _playerComponentMapper;
        ComponentMapper<Velocity> _velocityComponentMapper;

        public PlayerMovementSystem(Keybinds keybinds) : base(Aspect.All(typeof(Player), typeof(FreePosition), typeof(Velocity)))
        {
            _keybinds = keybinds;
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var entity in ActiveEntities) {
                Player player = _playerComponentMapper.Get(entity);
                Velocity velocity = _velocityComponentMapper.Get(entity);
                FreePosition freeposition = _freePositionComponentMapper.Get(entity);

                KeyboardState kstate = Keyboard.GetState();

                //Player Movement Logic
                if (kstate.IsKeyDown(_keybinds.PlayerMoveUp))
                    freeposition.position.Y -= velocity.speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (kstate.IsKeyDown(_keybinds.PlayerMoveDown))
                    freeposition.position.Y += velocity.speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (kstate.IsKeyDown(_keybinds.PlayerMoveLeft))
                    freeposition.position.X -= velocity.speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (kstate.IsKeyDown(_keybinds.PlayerMoveRight))
                    freeposition.position.X += velocity.speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            }
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _freePositionComponentMapper = mapperService.GetMapper<FreePosition>();
            _playerComponentMapper = mapperService.GetMapper<Player>();
            _velocityComponentMapper = mapperService.GetMapper<Velocity>();
        }
    }
}
