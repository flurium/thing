using Microsoft.EntityFrameworkCore;
using Thing.Context;
using Thing.Models;
using Thing.Repository.Interfaces;

namespace Thing.Repository
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
            _db.SaveChanges();
        }

        public async Task Edit(int Id, string Content)
        {
            var answer = await Entities.FirstOrDefaultAsync(o => o.Id == Id).ConfigureAwait(false);
            if (answer != null)
            {
                answer.Content = Content;
                _db.Entry(answer).State = EntityState.Modified;
                _db.SaveChanges();
            }
        }
    }
}