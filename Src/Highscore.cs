using System;
using System.Collections.Generic;
using System.Text;
using RT.Util;

namespace ExpertSokoban
{
    [Serializable]
    public struct Score
    {
        public int Pushes;
        public int Moves;
        public Score(int pushes, int moves) { Pushes = pushes; Moves = moves; }
    }

    /// <summary>
    /// Encapsulates a player's highscore in a level.
    /// </summary>
    [Serializable]
    public class Highscore : IComparable<Highscore>
    {
        /// <summary>
        /// The player's best result, where "best" means "fewest pushes".
        /// </summary>
        public Score BestPushScore = new Score(int.MaxValue, int.MaxValue);

        /// <summary>
        /// The player's best result, where "best" means "fewest pushes".
        /// </summary>
        public Score BestMoveScore = new Score(int.MaxValue, int.MaxValue);

        /// <summary>
        /// The player's best result, where "best" means "fewest sum of moves + pushes".
        /// </summary>
        public Score BestSumScore = new Score(int.MaxValue, int.MaxValue);

        /// <summary>
        /// If <paramref name="score"/> is a better score in any of the three respects, updates
        /// the relevant value with Score.
        /// </summary>
        /// <param name="Score">The Score to update the Highscore with.</param>
        public void UpdateWith(Score score)
        {
            if (score.Pushes < BestPushScore.Pushes ||
                (score.Pushes == BestPushScore.Pushes && score.Moves < BestPushScore.Moves))
                BestPushScore = score;
            if (score.Moves < BestMoveScore.Moves ||
                (score.Moves == BestMoveScore.Moves && score.Pushes < BestMoveScore.Pushes))
                BestMoveScore = score;
            if (score.Pushes + score.Moves < BestSumScore.Pushes + BestSumScore.Moves)
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
