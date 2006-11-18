using System;
using System.Collections.Generic;
using System.Text;
using RT.Util;

namespace ExpertSokoban
{
    /// <summary>
    /// This struct encapsulates a player's highscore in a level.
    /// </summary>
    [Serializable]
    public struct Highscore
    {
        /// <summary>
        /// The player's best result, where "best" means "fewest pushes".
        /// </summary>
        public Tuple<int /* pushes */, int /* moves */> BestPushScore;

        /// <summary>
        /// The player's best result, where "best" means "fewest pushes".
        /// </summary>
        public Tuple<int /* pushes */, int /* moves */> BestMoveScore;

        /// <summary>
        /// The player's best result, where "best" means "fewest sum of moves + pushes".
        /// </summary>
        public Tuple<int /* pushes */, int /* moves */> BestSumScore;

        /// <summary>
        /// If Score is a better score in any of the three respects, updates
        /// the relevant value with Score.
        /// </summary>
        /// <param name="Score">The Score to update the Highscore with.</param>
        public void UpdateWith(Tuple<int, int> Score)
        {
            if (Score.E1 < BestPushScore.E1 ||
                (Score.E1 == BestPushScore.E1 && Score.E2 < BestPushScore.E2))
                BestPushScore = Score;
            if (Score.E2 < BestMoveScore.E2 ||
                (Score.E2 == BestMoveScore.E2 && Score.E1 < BestMoveScore.E1))
                BestMoveScore = Score;
            if (Score.E1 + Score.E2 < BestSumScore.E1 + BestSumScore.E2)
                BestSumScore = Score;
        }

        /// <summary>
        /// Initialises a Highscore struct with a given Score.
        /// </summary>
        /// <param name="Score">The Score to initialise the Highscore with.</param>
        public Highscore(Tuple<int, int> Score)
        {
            BestPushScore = Score;
            BestMoveScore = Score;
            BestSumScore = Score;
        }

        /// <summary>
        /// Returns a string representation of this Highscore.
        /// </summary>
        public override string ToString()
        {
            return "(" +
                BestPushScore.E1 + "/" +
                BestPushScore.E2 + ") (" +
                BestMoveScore.E1 + "/" +
                BestMoveScore.E2 + ") (" +
                BestSumScore.E1 + "/" +
                BestSumScore.E2 + ")";
        }
    }
}
