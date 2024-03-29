using RhythmAI.Models;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.AspNetCore.Mvc;
public class ChatService : IChatService
{
    private readonly Kernel _kernel;
    public ChatService(Kernel kernel)
    {
        _kernel = kernel;
    }
    public async Task<string> SendMessage(ChatModel message)
    {

        var prompt = @"
                            You are an AI chatbot who is responsible for helping user's find new music.
                            {{$input}}";
        var chat = _kernel.CreateFunctionFromPrompt(prompt, executionSettings: new OpenAIPromptExecutionSettings { MaxTokens = 500 });
        var chatResponse = await _kernel.InvokeAsync(chat, new() { ["input"] = message.message });
        return chatResponse.GetValue<string>()!;
    }
}
