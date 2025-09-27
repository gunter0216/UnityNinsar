using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using NSubstitute;
using App.Core.Cubes.Runtime.Services;
using App.Core.Cubes.Runtime.Config;

namespace App.Core.Cubes.Tests
{
    public class CubesMoveStrategyTests
    {
        private CubesMoveStrategy m_Strategy;

        [SetUp]
        public void Setup()
        {
            var matrix = new List<string>
            {
                "1234321231", 
                "2341234123", 
                "4432123432", 
                "4332214414", 
                "2332224414", 
                "4333211234",
                "4321123432",
                "4332123431",
                "4432112341"
            };
            
            var config = Substitute.For<ICubesConfigController>();
            config.GetDisplayedSize().Returns(3);
            config.GetMatrix().Returns(matrix);
            config.GetWidth().Returns(10);
            config.GetHeight().Returns(9);
            
            var startPos = Substitute.For<IStartPositionStrategy>();
            startPos.GetStartPosition().Returns(new Vector2Int(1, 1));

            m_Strategy = new CubesMoveStrategy(config, startPos);
            m_Strategy.Initialize();
        }
        
        [Test]
        public void Move_NoActions_GetGrid()
        {
            var grid = m_Strategy.GetGrid();
            var matrix = new char[,]
            {
                {'1', '2', '3'},
                {'2', '3', '4'},
                {'4', '4', '3'}
            };
            
            AssertCharMatrixEqual(matrix, grid);
        }
        
        [Test]
        public void Move_Right_GetGrid()
        {
            m_Strategy.Move(Vector2Int.right);
            var grid = m_Strategy.GetGrid();
            var matrix = new char[,]
            {
                {'2', '3', '4'},
                {'3', '4', '1'},
                {'4', '3', '2'}
            };
            
            AssertCharMatrixEqual(matrix, grid);
        }

        [Test]
        public void Move_Left_GetGrid()
        {
            m_Strategy.Move(Vector2Int.left);
            var grid = m_Strategy.GetGrid();
            var matrix = new char[,]
            {
                {'1', '1', '2'},
                {'3', '2', '3'},
                {'2', '4', '4'}
            };
            
            AssertCharMatrixEqual(matrix, grid);
        }

        [Test]
        public void Move_Top_GetGrid()
        {
            m_Strategy.Move(Vector2Int.up);
            var grid = m_Strategy.GetGrid();
            var matrix = new char[,]
            {
                {'4', '4', '3'},
                {'1', '2', '3'},
                {'2', '3', '4'}
            };
            
            AssertCharMatrixEqual(matrix, grid);
        }
        
        [Test]
        public void Move_Bottom_GetGrid()
        {
            m_Strategy.Move(Vector2Int.down);
            var grid = m_Strategy.GetGrid();
            var matrix = new char[,]
            {
                {'2', '3', '4'},
                {'4', '4', '3'},
                {'4', '3', '3'}
            };
            
            AssertCharMatrixEqual(matrix, grid);
        }
        
        [Test]
        public void Move_8Right_GetGrid()
        {
            for (int i = 0; i < 8; i++)
            {
                m_Strategy.Move(Vector2Int.right);
            }

            var grid = m_Strategy.GetGrid();
            var matrix = new char[,]
            {
                {'3', '1', '1'},
                {'2', '3', '2'},
                {'3', '2', '4'}
            };
            
            AssertCharMatrixEqual(matrix, grid);
        }
        
        [Test]
        public void Move_7Bottom_GetGrid()
        {
            for (int i = 0; i < 7; i++)
            {
                m_Strategy.Move(Vector2Int.down);
            }

            var grid = m_Strategy.GetGrid();
            var matrix = new char[,]
            {
                {'4', '3', '3'},
                {'4', '4', '3'},
                {'1', '2', '3'}
            };
            
            AssertCharMatrixEqual(matrix, grid);
        }

        private void AssertCharMatrixEqual(char[,] expected, char[,] actual)
        {
            Assert.AreEqual(expected.GetLength(0), actual.GetLength(0), "Rows count mismatch");
            Assert.AreEqual(expected.GetLength(1), actual.GetLength(1), "Columns count mismatch");
            for (int i = 0; i < expected.GetLength(0); i++)
            {
                for (int j = 0; j < expected.GetLength(1); j++)
                {
                    Assert.AreEqual(expected[i, j], actual[i, j], $"Mismatch at [{i},{j}]");
                }
            }
        }
    }
}
