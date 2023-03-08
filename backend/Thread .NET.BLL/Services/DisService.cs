using AutoMapper;
using System.Linq;
using System.Threading.Tasks;
using Thread_.NET.BLL.Services.Abstract;
using Thread_.NET.Common.DTO.Like;
using Thread_.NET.DAL.Context;

namespace Thread_.NET.BLL.Services
{
    public sealed class DisService : BaseService
    {
        public DisService(ThreadContext context, IMapper mapper) : base(context, mapper) { }

        public async Task DisPost(NewReactionDTO reaction)
        {
            var dises = _context.PostReactions.Where(x => x.UserId == reaction.UserId && x.PostId == reaction.EntityId);

            if (dises.Any())
            {
                _context.PostReactions.RemoveRange(dises);
                await _context.SaveChangesAsync();

                return;
            }

            _context.PostReactions.Add(new DAL.Entities.PostReaction
            {
                PostId = reaction.EntityId,
                IsLike = reaction.IsLike,
                IsDis = reaction.IsDis,
                UserId = reaction.UserId
            });

            await _context.SaveChangesAsync();
        }
    }
}
