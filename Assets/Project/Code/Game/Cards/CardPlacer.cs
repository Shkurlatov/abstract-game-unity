using System.Collections.Generic;
using UnityEngine;

namespace Game.Cards
{
    public class CardPlacer
    {
        private readonly float _spacingFactor = 0.07f;
        private readonly List<Card> _cards;
        private readonly int _rows;
        private readonly int _columns;

        public CardPlacer(List<Card> cards, GameMode gameMode)
        {
            _cards = cards;
            _rows = gameMode.Rows;
            _columns = gameMode.Columns;
        }

        public void PlaceCards()
        {
            float boardSize = CalculateBoardSize();
            float spacing = CalculateSpacing(boardSize);
            float cardSize = CalculateCardSize(boardSize, spacing);

            ResizeCards(cardSize);

            Vector2 startPosition = CalculateStartPosition(cardSize, spacing);
            PlaceAllCards(cardSize, spacing, startPosition);
        }

        private float CalculateBoardSize()
        {
            float screenWidth = Screen.width;
            float screenHeight = Screen.height;
            return Mathf.Min(screenWidth, screenHeight);
        }

        private float CalculateSpacing(float squareSize)
        {
            float totalSpacing = squareSize * _spacingFactor;
            return totalSpacing / (_columns + 1);
        }

        private float CalculateCardSize(float squareSize, float spacing)
        {
            float availableSize = squareSize - (squareSize * _spacingFactor);
            return availableSize / Mathf.Max(_columns, _rows);
        }

        private void ResizeCards(float cardSize)
        {
            foreach (Card card in _cards)
            {
                card.SetSize(cardSize);
            }
        }

        private Vector2 CalculateStartPosition(float cardSize, float spacing)
        {
            float startX = -((_columns - 1) * (cardSize + spacing)) / 2f;
            float startY = (_rows - 1) * (cardSize + spacing) / 2f;
            return new Vector2(startX, startY);
        }

        private void PlaceAllCards(float cardSize, float spacing, Vector2 startPosition)
        {
            bool isOdd = _rows * _columns % 2 != 0;
            int centralRowIndex = _rows / 2;
            int centralColumnIndex = _columns / 2;

            int cardIndex = 0;
            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _columns; j++)
                {
                    if (isOdd && i == centralRowIndex && j == centralColumnIndex)
                    {
                        continue;
                    }

                    if (cardIndex < _cards.Count)
                    {
                        float posX = startPosition.x + j * (cardSize + spacing);
                        float posY = startPosition.y - i * (cardSize + spacing);
                        _cards[cardIndex].SetPosition(new Vector2(posX, posY));
                        cardIndex++;
                    }
                }
            }
        }
    }
}
