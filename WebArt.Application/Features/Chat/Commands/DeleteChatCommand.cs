using MediatR;

namespace WebArt.Application.Features.Chat.Commands
{
    public class DeleteChatCommand : IRequest
    {
        public int Id { get; set; }
    }
}
