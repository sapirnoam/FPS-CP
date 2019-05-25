using UnityEngine;
using GameJolt.API;
using GameJolt.API.Objects;
using UnityEngine.UI;

namespace GameJolt.UI.Controllers {
	public class ScoreItem : MonoBehaviour {
        public Text Rank;
        public Text Username;
		public Text Value;

        int scoreValue = 100;
        int tableID = 0;
        public Color DefaultColour = Color.white;
		public Color HighlightColour = Color.green;
		public void Init(Score score) {
            Value.text = score.Text;
            Username.text = score.PlayerName;
            GameJolt.API.Scores.GetRank(10, 420719, (int rank) => {
                Rank.text = "#" + rank;
            });

			bool isUserScore = score.UserID != 0 && GameJoltAPI.Instance.HasUser &&
			                   GameJoltAPI.Instance.CurrentUser.ID == score.UserID;
            Rank.color = isUserScore ? HighlightColour : DefaultColour;
            Username.color = isUserScore ? HighlightColour : DefaultColour;
			Value.color = isUserScore ? HighlightColour : DefaultColour;
		}
	}
}