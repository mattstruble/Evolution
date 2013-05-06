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
	class BlockManager
	{
		private List<Block> _blocks;
		private List<Block> _matingPool;

		private Color _targetColor;
		private Block _targetBlock;

		private Vector2 _blockSize;
		private Vector3 _tColorVector;

		private Random _rand;

		private Texture2D _assetSheet;

		private int _timeUntilUpdate;
		private int _updateTime;

		public BlockManager()
		{
			_blocks = new List<Block>();
			_matingPool = new List<Block>();
			_rand = new Random();

			_blockSize = new Vector2(50);

			_updateTime = _timeUntilUpdate = 1000;
		}

		public void loadContent(ContentManager pContent)
		{
			_assetSheet = pContent.Load<Texture2D>("blank");
		}

		public void generate(int num)
		{
			Color tmpC;

			for (int i = 0; i < num; i++)
			{
				tmpC = new Color(_rand.Next(0, 255), _rand.Next(0, 255), _rand.Next(0, 255));

				_blocks.Add(new Block(tmpC, new Vector2(_blockSize.X * i, 100), _blockSize));
			}

			_targetColor = new Color(_rand.Next(0, 255), _rand.Next(0, 255), _rand.Next(0, 255));
			_tColorVector = _targetColor.ToVector3();

			_targetBlock = new Block(_targetColor, new Vector2(200, 200), _blockSize);
		}

		public void update(GameTime gameTime)
		{
			if (_timeUntilUpdate <= 0 && _blocks.Count > 0)
			{
				generateMatingPool();
				_timeUntilUpdate = _updateTime;
			}
			else if (_timeUntilUpdate <= 0 && _matingPool.Count > 0)
			{
				crossOver();
				_timeUntilUpdate = _updateTime;
			}

			_timeUntilUpdate -= gameTime.ElapsedGameTime.Milliseconds;
		}

		public void draw(SpriteBatch pBatch)
		{
			if (_blocks.Count > 0)
			{
				for (int i = 0; i < _blocks.Count; i++)
				{
					_blocks[i].draw(pBatch, _assetSheet);
				}
			}
			else if (_matingPool.Count > 0)
			{
				for (int i = 0; i < _matingPool.Count; i++)
				{
					_matingPool[i].draw(pBatch, _assetSheet);
				}
			}

			_targetBlock.draw(pBatch, _assetSheet);
		}

		//EVOLUTIONARY FUNCTIONS BELOW

		private void generateMatingPool()
		{
			Block b1, b2;

			while (_blocks.Count > 0)
			{
				b1 = _blocks[_rand.Next(_blocks.Count)];

				do
				{
					b2 = _blocks[_rand.Next(_blocks.Count)];
				} while (b2 == b1);

				if (fitness(b1) < fitness(b2))
					_matingPool.Add(b1);
				else
					_matingPool.Add(b2);

				_blocks.Remove(b1);
				_blocks.Remove(b2);
			}
		}

		private float fitness(Block b)
		{
			float fit = 0;
			Vector3 color = b.getColorV();

			fit = Math.Abs(color.X - _tColorVector.X) + Math.Abs(color.Y - _tColorVector.Y) + Math.Abs(color.Z - _tColorVector.Z);

			return fit;
		}

		private void crossOver()
		{
			Block b1, b2;
			float[] g1; float[] g2;
			float[] tg1; float[] tg2;
			int swapPoint;

			while (_matingPool.Count > 0)
			{
				b1 = _matingPool[0];
				_matingPool.Remove(b1);

				b2 = _matingPool[0];
				_matingPool.Remove(b2);

				g1 = tg1 = b1.getGene();
				g2 = tg2 = b2.getGene();

				swapPoint = _rand.Next(tg1.Length);

				for(int i = 0; i < tg1.Length; i ++)
				{
					if(i == swapPoint)
					{
						g1[i] = tg2[i];
						g2[i] = tg1[i];
					}

					if (_rand.Next(0, 100) < 20)
					{
						float tmp = (int)(g1[i] * 100);
						tmp = _rand.Next((int)tmp - 5, (int)tmp + 5);
						tmp /= 100;
						g1[i] = tmp;
					}

					if (_rand.Next(0, 100) < 20)
					{
						float tmp = (int)(g2[i] * 100);
						tmp = _rand.Next((int)tmp - 5, (int)tmp + 5);
						tmp /= 100;
						g2[i] = tmp;
					}

				}

				_blocks.Add(new Block(convertToV3(g1)));
				_blocks.Add(new Block(convertToV3(g2)));
				_blocks.Add(b1);
				_blocks.Add(b2);
				
			}

			fixBlocks();
		}

		private void fixBlocks()
		{
			for (int i = 0; i < _blocks.Count; i++)
			{
				_blocks[i].setLoc(new Vector2(i * _blockSize.X, 100));
				_blocks[i].setSize(_blockSize);
			}
		}

		private Vector3 convertToV3(float[] arr)
		{
			return new Vector3(arr[0], arr[1], arr[2]);
		}
	}
}
