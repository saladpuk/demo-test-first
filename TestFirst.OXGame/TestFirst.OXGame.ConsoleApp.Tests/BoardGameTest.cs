using System;
using Xunit;

namespace TestFirst.OXGame.ConsoleApp.Tests
{
    public class BoardGameTest
    {
        [Fact(DisplayName = "ลง X ไปตอนที่กระดานว่าง ระบบให้ลงได้ แต่ยังไม่มีผู้ชนะ และสลับเป็นตาของ O")]
        public void PlaceXWhenBoardIsEmpty()
        {
            var boardGame = new BoardGame();
            var canPlace = boardGame.Plac("X", 0, 0);
            Assert.True(canPlace);
            Assert.Equal("O", boardGame.CurrentTurn);
        }
    }
}
