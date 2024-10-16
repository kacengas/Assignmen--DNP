using RepositoryContracts;

namespace CLI.UI.ManageComments;

public class CommentReactionListView
{
    private readonly ICommentRepository commentRepository;

    public CommentReactionListView(ICommentRepository commentRepository)
    {
        this.commentRepository = commentRepository;
    }

    public async Task ShowList()
    {
        Console.Clear();
        Console.WriteLine("Showing list of reactions");

        var reactions = commentRepository.getManyReactions();

        if (!reactions.Any())
        {
            Console.WriteLine("No comments available.\n");
        }
        else
        {
            foreach (var commentReaction in reactions)
            {
                Console.WriteLine($"ID: {commentReaction.Id} \n" +
                                  $"Posted by: {commentReaction.UserId} \n" +
                                  $"Posted on: {commentReaction.CommentId} \n" +
                                  $"Type: {commentReaction.Type} \n");
            }
        }

        Console.WriteLine("Press any key to go back...");
        Console.ReadKey();
    }
    
}