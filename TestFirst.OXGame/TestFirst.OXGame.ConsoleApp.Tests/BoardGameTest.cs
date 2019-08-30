using System;
using Xunit;

namespace TestFirst.OXGame.ConsoleApp.Tests
{
    public class BoardGameTest
    {
        [Fact(DisplayName = "ลง X ไปตอนที่กระดานว่าง ระบบให้ลงได้ แต่ยังไม่มีผู้ชนะ และสลับเป็นตาของ O")]
        public void PlaceXWhenBoardIsEmpty()
        {
            var slots = new string[3, 3];
            verifyPlaceASymbolToEmptySpaceThenSystemMustAcceptTheRequest(slots, "X", 0, 0, "O");
        }

        [Fact(DisplayName = "ลง O ไปในช่องว่าซึ่งบนกระดานมี X 1 ตัวเท่านั้น ระบบให้ลงได้ แต่ยังไม่มีผู้ชนะ และสลับเป็นตาของ X")]
        public void PlaceOInEmptySlotWhenBoardHave_1X_0O()
        {
            var slots = new string[,]
            {
                { "X", null, null },
                { null, null, null },
                { null, null, null },
            };
            verifyPlaceASymbolToEmptySpaceThenSystemMustAcceptTheRequest(slots, "O", 0, 1, "X");
        }

        [Fact(DisplayName = "ลง X ไปในช่องว่าซึ่งบนกระดานมี X 1 ตัวและ O 1 ตัว ระบบให้ลงได้ แต่ยังไม่มีผู้ชนะ และสลับเป็นตาของ O")]
        public void PlaceXInEmptySlotWhenBoardHave_1X_1O()
        {
            var slots = new string[,]
            {
                { "X", "O", null },
                { null, null, null },
                { null, null, null },
            };
            verifyPlaceASymbolToEmptySpaceThenSystemMustAcceptTheRequest(slots, "X", 1, 0, "O");
        }

        [Fact(DisplayName = "ลง O ไปในช่องว่าซึ่งบนกระดานมี X 2 ตัวและ O 1 ตัว ระบบให้ลงได้ แต่ยังไม่มีผู้ชนะ และสลับเป็นตาของ X")]
        public void PlaceOInEmptySlotWhenBoardHave_2X_1O()
        {
            var slots = new string[,]
            {
                { "X", "O", null },
                { "X", null, null },
                { null, null, null },
            };
            verifyPlaceASymbolToEmptySpaceThenSystemMustAcceptTheRequest(slots, "O", 1, 1, "X");
        }

        [Fact(DisplayName = "ลง X ไปในช่องว่าซึ่งบนกระดานมี X 2 ตัวและ O 2 ตัว แต่ X ทั้งหมดไม่ได้เรียงกัน ระบบให้ลงได้ แต่ยังไม่มีผู้ชนะ และสลับเป็นตาของ O")]
        public void PlaceXInEmptySlotWhenBoardHave_2X_2O_ButNotConnectedTogather()
        {
            var slots = new string[,]
            {
                { "X", "O", null },
                { "X", "O", null },
                { null, null, null },
            };
            verifyPlaceASymbolToEmptySpaceThenSystemMustAcceptTheRequest(slots, "O", 0, 2, "O");
        }

        [Fact(DisplayName = "ลง O ไปในช่องว่าซึ่งบนกระดานมี X 3 ตัวและ O 2 ตัว แต่ O ทั้งหมดไม่ได้เรียงกัน ระบบให้ลงได้ แต่ยังไม่มีผู้ชนะ และสลับเป็นตาของ X")]
        public void PlaceOInEmptySlotWhenBoardHave_3X_2O_ButNotConnectedTogather()
        {
            var slots = new string[,]
            {
                { "X", "O", null },
                { "X", "O", null },
                { null, "X", null },
            };
            verifyPlaceASymbolToEmptySpaceThenSystemMustAcceptTheRequest(slots, "O", 2, 0, "X");
        }

        [Fact(DisplayName = "ลง X ไปในช่องว่าซึ่งบนกระดานมี X 2 ตัวและ O 2 ตัว และ X ทั้งหมดเรียงกัน ระบบให้ลงได้และประกาศว่า X ชนะพร้อมกับเพิ่มแต้มให้ X 1 คะแนน")]
        public void PlaceXInEmptySlotWhenBoardHave_2X_2O_WithConnectedTogather()
        {
            var slots = new string[,]
            {
                { "X", "O", null },
                { "X", "O", null },
                { null, null, null },
            };
            var boardGame = new BoardGame { Slots = slots };
            var canPlace = boardGame.Place("X", 2, 0);
            Assert.True(canPlace);
            Assert.Equal("X", boardGame.GetWinner());
            Assert.Equal(0, boardGame.OScore);
            Assert.Equal(1, boardGame.XScore);
            Assert.False(boardGame.IsDraw);
        }

        [Fact(DisplayName = "ลง O ไปในช่องว่าซึ่งบนกระดานมี X 3 ตัวและ O 2 ตัว และ O ทั้งหมดเรียงกัน ระบบให้ลงได้และประกาศว่า O ชนะ พร้อมกับเพิ่มแต้มให้ O 1 คะแนน")]
        public void PlaceOInEmptySlotWhenBoardHave_3X_2O_WithConnectedTogather()
        {
            var slots = new string[,]
            {
                { "X", "O", "X" },
                { "X", "O", null },
                { null, null, null },
            };
            var boardGame = new BoardGame { Slots = slots };
            var canPlace = boardGame.Place("O", 2, 1);
            Assert.True(canPlace);
            Assert.Equal("O", boardGame.GetWinner());
            Assert.Equal(1, boardGame.OScore);
            Assert.Equal(0, boardGame.XScore);
            Assert.False(boardGame.IsDraw);
        }

        [Fact(DisplayName = "ลง X ไปในช่องว่างซึ่งบนกระดานมี X 4 ตัวและ O 4 ตัว แต่ X ทั้งหมดไม่ได้เรียงกัน ระบบให้ลงได้ และแจ้งว่าเกมเสมอ")]
        public void PlaceXInEmptySlotWhenBoardHave_4X_4O_WithConnectedTogather()
        {
            var slots = new string[,]
            {
                { "X", "O", "X" },
                { "X", "O", "O" },
                { "O", "X", null },
            };
            var boardGame = new BoardGame { Slots = slots };
            var canPlace = boardGame.Place("X", 2, 2);
            Assert.True(canPlace);
            Assert.Null(boardGame.GetWinner());
            Assert.Equal(0, boardGame.OScore);
            Assert.Equal(0, boardGame.XScore);
            Assert.True(boardGame.IsDraw);
        }

        private void verifyPlaceASymbolToEmptySpaceThenSystemMustAcceptTheRequest(string[,] slots, string symbol, int row, int column, string expectedCurrentTurn)
        {
            var boardGame = new BoardGame { Slots = slots };
            var canPlace = boardGame.Place(symbol, row, column);
            Assert.True(canPlace);
            Assert.Equal(expectedCurrentTurn, boardGame.CurrentTurn);
            Assert.Null(boardGame.GetWinner());
            Assert.False(boardGame.IsDraw);
        }
    }
}
