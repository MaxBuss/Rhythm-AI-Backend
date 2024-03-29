
using RhythmAI.Models;

public interface IChatService
{
    public Task<string> SendMessage(ChatModel message);
}