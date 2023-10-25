using Wheelzy.Solutions;

namespace Wheelzy.Tests
{
    [TestClass]
    public class ChessKnightMovesTests
    {
        [TestMethod]
        public void GetKnightMoves_CenterPosition_Returns8PossibleMoves()
        {
            // Arrange
            var expectedMoves = new List<Tuple<int, int>>
            {
                Tuple.Create(2, 5),
                Tuple.Create(2, 3),
                Tuple.Create(3, 6),
                Tuple.Create(3, 2),
                Tuple.Create(5, 6),
                Tuple.Create(5, 2),
                Tuple.Create(6, 5),
                Tuple.Create(6, 3)
            };

            // Act
            var actualMoves = ChessKnightMoves.GetKnightMoves(4, 4);

            // Assert
            CollectionAssert.AreEquivalent(expectedMoves, actualMoves);
        }

        [TestMethod]
        public void GetKnightMoves_CornerPosition_Returns2PossibleMoves()
        {
            // Arrange
            var expectedMoves = new List<Tuple<int, int>>
            {
                Tuple.Create(1, 2),
                Tuple.Create(2, 1)
            };

            // Act
            var actualMoves = ChessKnightMoves.GetKnightMoves(0, 0);

            // Assert
            CollectionAssert.AreEquivalent(expectedMoves, actualMoves);
        }

        [TestMethod]
        public void GetKnightMoves_WrongInput_ReturnsNoPossibleMoves()
        {
            // Act
            var actualMoves = ChessKnightMoves.GetKnightMoves(10, -15);

            // Assert
            CollectionAssert.AreEquivalent(new List<Tuple<int, int>>(), actualMoves);
        }

        [TestMethod]
        public void GetKnightMoves_EdgePosition_Returns4PossibleMoves()
        {
            // Arrange
            var expectedMoves = new List<Tuple<int, int>>
            {
                Tuple.Create(5, 6),
                Tuple.Create(6, 5)
            };

            // Act
            var actualMoves = ChessKnightMoves.GetKnightMoves(7, 7);

            // Assert
            CollectionAssert.AreEquivalent(expectedMoves, actualMoves);
        }

        [TestMethod]
        public void GetKnightMoves_OnEdgePosition_Returns2PossibleMoves()
        {
            // Arrange
            var expectedMoves = new List<Tuple<int, int>>
            {
                Tuple.Create(1, 5),
                Tuple.Create(2, 6)
            };

            // Act
            var actualMoves = ChessKnightMoves.GetKnightMoves(0, 7);

            // Assert
            CollectionAssert.AreEquivalent(expectedMoves, actualMoves);
        }

        [TestMethod]
        public void GetKnightMoves_NearEdgePosition_Returns6PossibleMoves()
        {
            // Arrange
            var expectedMoves = new List<Tuple<int, int>>
            {
                Tuple.Create(1, 6),
                Tuple.Create(2, 5),
                Tuple.Create(4, 5),
                Tuple.Create(5, 6)
            };

            // Act
            var actualMoves = ChessKnightMoves.GetKnightMoves(3, 7);

            // Assert
            CollectionAssert.AreEquivalent(expectedMoves, actualMoves);
        }
    }
}