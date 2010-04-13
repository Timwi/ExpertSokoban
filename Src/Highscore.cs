using System;

namespace ExpertSokoban
{
    /// <summary>Encapsulates a user's score for a specific level.</summary>
    [Serializable]
    sealed class Score
    {
        /// <summary>Number of moves.</summary>
        public int Moves;
        /// <summary>Number of pushes.</summary>
        public int Pushes;
        /// <summary>Constructor.</summary>
        public Score(int moves, int pushes) { Moves = moves; Pushes = pushes; }
        /// <summary>Constructor.</summary>
        /// <remarks>This empty constructor is necessary for XmlClassify to work.</remarks>
        public Score() { }
    }

    /// <summary>
    /// Encapsulates a player's highscore in a level.
    /// </summary>
    [Serializable]
    sealed class Highscore : IComparable<Highscore>
    {
        /// <summary>
        /// The player's best result, where "best" means "fewest pushes".
        /// </summary>
        public Score BestPushScore = null;

        /// <summary>
        /// The player's best result, where "best" means "fewest pushes".
        /// </summary>
        public Score BestMoveScore = null;

        /// <summary>
        /// The player's best result, where "best" means "fewest sum of moves + pushes".
        /// </summary>
        public Score BestSumScore = null;

        /// <summary>
        /// If <paramref name="score"/> is a better score in any of the three respects, updates
        /// the relevant value with Score.
        /// </summary>
        /// <param name="score">The Score to update the Highscore with.</param>
        public void UpdateWith(Score score)
        {
            if (BestPushScore == null || score.Pushes < BestPushScore.Pushes ||
                (score.Pushes == BestPushScore.Pushes && score.Moves < BestPushScore.Moves))
                BestPushScore = score;
            if (BestMoveScore == null || score.Moves < BestMoveScore.Moves ||
                (score.Moves == BestMoveScore.Moves && score.Pushes < BestMoveScore.Pushes))
                BestMoveScore = score;
            if (BestSumScore == null || score.Pushes + score.Moves < BestSumScore.Pushes + BestSumScore.Moves)
                BestSumScore = score;
        }

        /// <summary>
        /// Returns a string representation of this Highscore.
        /// </summary>
        public override string ToString()
        {
            return "(" +
                BestPushScore.Pushes + "/" +
                BestPushScore.Moves + ") (" +
                BestMoveScore.Pushes + "/" +
                BestMoveScore.Moves + ") (" +
                BestSumScore.Pushes + "/" +
                BestSumScore.Moves + ")";
        }

        /// <summary>Compares this <see cref="Highscore"/> to another.</summary>
        public int CompareTo(Highscore other)
        {
            if (BestPushScore.Pushes < other.BestPushScore.Pushes)
                return -1;
            if (BestPushScore.Pushes > other.BestPushScore.Pushes)
                return 1;
            if (BestPushScore.Moves < other.BestPushScore.Moves)
                return -1;
            if (BestPushScore.Moves > other.BestPushScore.Moves)
                return 1;
            return 0;
        }
    }
}
