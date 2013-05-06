using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace Evolution.Files
{
	class Block
	{
		private Color _color;

		private Random _rand;

		private Vector2 _location;
		private Vector2 _size;

		public Block()
		{
			_rand = new Random();

			_color = new Color(_rand.Next(0, 255), _rand.Next(0, 255), _rand.Next(0, 255));

			_location = new Vector2(0);
			_size = new Vector2(0);
		}

		public Block(Vector2 loc, Vector2 s)
		{
			_rand = new Random();

			_color = new Color(_rand.Next(0, 255), _rand.Next(0, 255), _rand.Next(0, 255));

			_location = loc;
			_size = s;
		}

		public Block(Color c)
		{
			_color = c;

			_location = new Vector2(0);
			_size = new Vector2(0);
		}

		public Block(Vector3 c)
		{
			_color = new Color(c);

			_location = new Vector2(0);
			_size = new Vector2(0);
		}

		public Block(float r, float g, float b)
		{
			_color = new Color(r, g, b);
		}

		public Block(Color c, Vector2 loc, Vector2 s)
		{
			_color = c;
			_location = loc;
			_size = s;
		}

		public Block(Vector3 cV, Vector2 loc, Vector2 s)
		{
			_color = new Color(cV);
			_location = loc;
			_size = s;
		}

		public Block(float r, float g, float b, Vector2 loc, Vector2 s)
		{
			_color = new Color(r, g, b);
			_location = loc;
			_size = s;
		}

		public void setColor(float r, float g, float b)
		{
			_color = new Color(r, g, b);
		}

		public void setColor(Color c)
		{
			_color = c;
		}

		public void setColor(Vector3 c)
		{
			_color = new Color(c);
		}

		public void setLoc(Vector2 l)
		{
			_location = l;
		}

		public void setSize(Vector2 s)
		{
			_size = s;
		}

		public Color getColor()
		{
			return _color;
		}

		public Vector2 getLoc()
		{
			return _location;
		}

		public Vector2 getSize()
		{
			return _size;
		}

		public Vector3 getColorV()
		{
			return _color.ToVector3();
		}

		public float[] getGene()
		{
			Vector3 tmpV3 = _color.ToVector3();

			float[] gene = new float[] { tmpV3.X, tmpV3.Y, tmpV3.Z };

			return gene;
		}

		public void draw(SpriteBatch pBatch, Texture2D assetSheet)
		{
			pBatch.Draw(assetSheet,
				_location,
				new Rectangle(0, 0, 1, 1),
				_color, 
				0f, 
				new Vector2(0),
				_size,
				SpriteEffects.None,
				0f);
		}

	}
}
