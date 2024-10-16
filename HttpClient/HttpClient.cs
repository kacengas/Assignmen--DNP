namespace HttpClient;

static readonly HttPClient client = new HttPClient();

static async Task Main()
{
    await CreatePost();
    await GetSinglePost();
    await DeletePost();
    await CreateComment();
    await GetSingleComment();
    await DeleteComment();
    await UpdateComment();
}
{
    
}