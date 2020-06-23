using PonguGame.lib;
using PonguGame.resources;
using SFML.Graphics;
using SFML.System;

namespace PonguGame.model
{
    public class ScoreBoard : SceneNode
    {
        private int _playerScore;
        private int _opponentScore;
        private bool _playerScoreChanged;
        private bool _opponentScoreChanged;

        private Text _playerScoreDisplay;
        private Text _opponentScoreDisplay;

        private readonly RenderWindow windowRef;

        public ScoreBoard(ref RenderWindow window) : base(Layer.Background)
        {
            _playerScore = 0;
            _opponentScore = 0;

            windowRef = window;
            
            _playerScoreDisplay = new Text($"Player: {_playerScore.ToString()}", ResourceRegistry.GetFont(Fonts.Score), 21);
            _opponentScoreDisplay = new Text($"Opponent: {_opponentScore.ToString()}", ResourceRegistry.GetFont(Fonts.Score), 21);

            _playerScoreChanged = true;
            _opponentScoreChanged = true;
            
            SetScorePositions();
        }

        private void SetScorePositions()
        {
            var playerCenterChar =
                _playerScoreDisplay.FindCharacterPos((uint)(_playerScoreDisplay.DisplayedString.Length - 1) / 2);
            var opponentCenterChar =
                _opponentScoreDisplay.FindCharacterPos((uint) (_opponentScoreDisplay.DisplayedString.Length - 1) / 2);
            
            _playerScoreDisplay.Origin = new Vector2f(playerCenterChar.X / 2.0f, playerCenterChar.Y / 2.0f);
            _opponentScoreDisplay.Origin = new Vector2f(opponentCenterChar.X / 2.0f, opponentCenterChar.Y / 2.0f);
            
            _playerScoreDisplay.Position = new Vector2f(windowRef.Size.X / 5.0f, windowRef.Size.Y / 10.0f);
            _opponentScoreDisplay.Position = new Vector2f((windowRef.Size.X / 5.0f) * 4f, windowRef.Size.Y / 10.0f);
        }

        public void ScorePoint(bool forPlayer)
        {
            if (forPlayer)
            {
                _playerScore++;
                _playerScoreChanged = true;
            }
            else
            {
                _opponentScore++;
                _opponentScoreChanged = true;
            }
        }

        public override void UpdateCurrent(Time deltaTime)
        {
            if (_playerScoreChanged)
            {
                _playerScoreDisplay.DisplayedString = $"Player: {_playerScore.ToString()}";
                _playerScoreChanged = false;
            }

            if (_opponentScoreChanged)
            {
                _opponentScoreDisplay.DisplayedString = $"Opponent: {_opponentScore.ToString()}";
                _opponentScoreChanged = false;
            }
        }

        public override void DrawCurrent(RenderTarget target, RenderStates states)
        {
            _playerScoreDisplay.Draw(target, states);
            _opponentScoreDisplay.Draw(target, states);
        }
    }
}