using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace EncoreSample.Models
{
	public class Audio
	{
		[Key]
		public int AudioId { get; set; }
		[ForeignKey("User")]
		public string? UserId { get; set; }
		public string Song { get; set; } = String.Empty;
		[DisplayName("Artist")]
		public string OriginalArtist { get; set; } = String.Empty;
		[DisplayName("Submission Date")]
		public DateTime SubmittedOn { get; set; } = DateTime.Now;

		[DisplayName("Audio Path")]
		public string AudioPath { get; set; } = String.Empty;

		public int NumberOfLikes { get; set; } = 0;
		public int NumberOfDislikes { get; set; } = 0;

		public int Vote { get { return NumberOfLikes - NumberOfDislikes; } }

        public User? User { get; set; }

        public List<Vote>? Votes { get; set; }
		public List<Comment>? Comments { get; set; }

		
	}
}
