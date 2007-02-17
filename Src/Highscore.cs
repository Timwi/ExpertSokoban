using System;
using System.Collections.Generic;
using System.Text;
using RT.Util;

using Score=RT.Util.Tuple<int /* pushes */, int /* moves */>;

namespace ExpertSokoban
{
    /// <summary>
    /// Encapsulates a player's highscore in a level.
    /// </summary>
    [Serializable]
    public class Highscore
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
        /// If Score is a better score in any of the three respects, updates
        /// the relevant value with Score.
        /// </summary>
        /// <param name="Score">The Score to update the Highscore with.</param>
        public void UpdateWith(Score Score)
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

        public int CompareTo(Highscore Other)
        {
            if (BestPushScore.E1 < Other.BestPushScore.E1)
                return -1;
            if (BestPushScore.E1 > Other.BestPushScore.E1)
                return 1;
            if (BestPushScore.E2 < Other.BestPushScore.E2)
                return -1;
            if (BestPushScore.E2 > Other.BestPushScore.E2)
                return 1;
            return 0;
        }
    }
}
