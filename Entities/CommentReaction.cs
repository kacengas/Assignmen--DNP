using System.Text.Json.Serialization;
using Enums;

namespace Entities;

public class CommentReaction
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int CommentId { get; set; }
    public ReactionType Type { get; set; }
}