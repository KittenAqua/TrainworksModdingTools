using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Trainworks.Utilities
{/// <summary>
 /// An interface used to represent settings
 /// </summary>
    public interface ISettings { }

    /// <summary>
    /// A generic interface used to represent settings to apply
    /// </summary>
    public interface ISettings<T> : ISettings
    {
        void ApplySettings(ref T @object);
    }

    public class GameObjectImportSettings : ISettings<GameObject>
    {
        /// <summary>
        /// A Function that is called after Settings are applied.
        /// Use this to add your own GameObject logic.
        /// </summary>
        public Func<GameObject, GameObject> Func { get; set; }

        public void ApplySettings(ref GameObject @object)
        {
            if (Func != null)
            {
                @object = Func(@object);
            }
        }
    }

    public class Texture2DImportSettings : ISettings<Texture2D>
    {
        /// <summary>
        /// A Function that is called after settings are applied.
        /// Use this to add your own texture logic.
        /// </summary>
        public Func<Texture2D, Texture2D> Func { get; set; }

        /// <summary>
        /// The Filter that should be when scaling the texture
        /// </summary>
        public FilterMode Filter { get; set; }
        /// <summary>
        /// The WrapMode of the Texture on the horizontal axis
        /// </summary>
        public TextureWrapMode WrapModeU { get; set; }
        /// <summary>
        /// The WrapMode of the Texture on the Vertical axis
        /// </summary>
        public TextureWrapMode WrapModeV { get; set; }
        /// <summary>
        /// The Scale of the Object
        /// </summary>
        public Vector2 Scale { get; set; }

        public Texture2DImportSettings()
        {
            Filter = FilterMode.Bilinear;
            WrapModeU = TextureWrapMode.Clamp;
            WrapModeV = TextureWrapMode.Clamp;
        }

        public void ApplySettings(ref Texture2D @object)
        {
            @object.filterMode = Filter;
            @object.wrapModeU = WrapModeU;
            @object.wrapModeV = WrapModeV;
            if (@object.isReadable)
            {
                @object.Resize((int)(@object.width * Scale.x), (int)(@object.height * Scale.y));
            }
            if (Func != null)
            {
                @object = Func(@object);
            }
        }
    }

    /// <summary>
    /// Import Settings for Sprites
    /// </summary>
    public class SpriteImportSettings : ISettings<Sprite>
    {
        /// <summary>
        /// A Function that is called after settings are applied.
        /// Use this to add your own sprite logic.
        /// </summary>
        public Func<Sprite, Sprite> Func { get; set; }
        /// <summary>
        /// Rectangular section of the texture to use for the sprite relative to the textures width and height, keep values between 0 and 1
        /// </summary>
        public Rect Rectangle { get; set; }
        /// <summary>
        /// Sprite's pivot point relative to its graphic rectangle
        /// </summary>
        public Vector2 Pivot { get; set; }
        /// <summary>
        /// The number of pixels in the sprite that correspond to one unit in world space.
        /// </summary>
        public float PixelPerUnit { get; set; }
        /// <summary>
        /// Amount by which the sprite mesh should be expanded outwards.
        /// </summary>
        public uint Extrude { get; set; }
        /// <summary>
        /// Controls the type of mesh generated for the sprite.
        /// </summary>
        public SpriteMeshType MeshType { get; set; }
        /// <summary>
        /// The border sizes of the sprite (X=left, Y=bottom, Z=right, W=top).
        /// </summary>
        public Vector4 Border { get; set; }
        /// <summary>
        /// Generates a default physics shape for the sprite.
        /// </summary>
        public bool GenerateFallbackPhysicsShape { get; set; }

        public SpriteImportSettings()
        {
            Rectangle = new Rect(0, 0, 1, 1);
            Pivot = new Vector2(0.5f, 0.5f);
            PixelPerUnit = 100f;
            Extrude = 0;
            MeshType = SpriteMeshType.Tight;
            Border = Vector4.zero;
            GenerateFallbackPhysicsShape = true;
        }

        public void ApplySettings(ref Sprite @object)
        {
            @object = Sprite.Create(
                @object.texture,
                new Rect(
                    @object.texture.width * Rectangle.x,
                    @object.texture.height * Rectangle.y,
                    @object.texture.width * Rectangle.width,
                    @object.texture.height * Rectangle.height),
                Pivot,
                PixelPerUnit,
                Extrude,
                MeshType,
                Border,
                GenerateFallbackPhysicsShape);

            if (Func != null)
            {
                @object = Func(@object);
            }
        }
    }
}
