using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using Magitech.Components;
using System.Diagnostics;

namespace Magitech.Systems
{
    class DynamicRenderSystem : EntityDrawSystem
    {

        GraphicsDevice _graphicsdevice;
        SpriteBatch _spriteBatch;
        OrthographicCamera _camera;

        ComponentMapper<FreePosition> _freePositionMapper;
        ComponentMapper<Sprite> _spriteMapper;

        public DynamicRenderSystem(GraphicsDevice graphicsDevice, OrthographicCamera camera) : base(Aspect.All(typeof(FreePosition), typeof(Sprite)))
        {
            _graphicsdevice = graphicsDevice;
            _spriteBatch = new SpriteBatch(graphicsDevice);
            _camera = camera;
        }


        public override void Draw(GameTime gameTime)
        {
            var transformMatrix = _camera.GetViewMatrix();

            _spriteBatch.Begin(transformMatrix: transformMatrix);


			foreach (var entityID in ActiveEntities)
            {
                Vector2 position = _freePositionMapper.Get(entityID).position;
                Sprite sprite = _spriteMapper.Get(entityID);
                _spriteBatch.Draw(sprite.texture, position, Color.White);
                Debug.WriteLine("Rendering Sprite: " + sprite.textureName);
                Entity entityobj = GetEntity(entityID);
                if (entityobj.Has<Player>())
                {
                    Debug.WriteLine("Rendering player at " + position.ToString());
                }
            }

            _spriteBatch.End();
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _freePositionMapper = mapperService.GetMapper<FreePosition>();
            _spriteMapper = mapperService.GetMapper<Sprite>();
        }
    }
}
