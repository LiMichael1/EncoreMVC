using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Composition.Convention;

namespace EncoreSample.Models
{
	public class Vote
	{
		[Key]
		public int VoteId { get; set; }
		public int VoteType { get; set; }

		[Required]
		[ForeignKey("Audio")]
		public int AudioId { get; set; }

		[Required]
		[ForeignKey("User")]
		public string UserId { get; set; }

		public Audio Audio { get; set; }

		public User User { get; set; }
	}
}
