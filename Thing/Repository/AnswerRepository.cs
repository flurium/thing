using Microsoft.EntityFrameworkCore;
using Dal.Context;
using Dal.Models;
using Dal.Repository.Interfaces;

namespace Dal.Repository
{
    public class AnswerRepository : BaseRepository<Answer>, IAnswerRepository
    {
        public AnswerRepository(ThingDbContext context) : base(context)
        {
        }

        public async Task Delete(int Id)
        {
            var answer = await Entities.FirstOrDefaultAsync(o => o.Id == Id).ConfigureAwait(false);
            if (answer != null) Entities.Remove(answer);
            await _db.SaveChangesAsync();
        }

        public async Task Edit(int Id, string Content)
        {
            var answer = await Entities.FirstOrDefaultAsync(o => o.Id == Id).ConfigureAwait(false);
            if (answer != null)
            {
                answer.Content = Content;
                await _db.SaveChangesAsync();
            }
        }
    }
}