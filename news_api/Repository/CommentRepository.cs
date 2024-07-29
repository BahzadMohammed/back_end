// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using news_api.Data;
// using news_api.Interfaces;
// using news_api.Models;
// using Microsoft.EntityFrameworkCore;


// namespace news_api.Repository
// {
//     public class CommentRepository : ICommentRepository
//     {
        
//         private readonly ApplicationDBContext _context;

//         public CommentRepository(ApplicationDBContext context)
//         {
//             _context = context;
//         }

//         public async Task<Comment> AddCommentAsync(Comment comment)
//         {
//             _context.Comments.Add(comment);
//             await _context.SaveChangesAsync();
//             return comment;
//         }

//         public async Task<Comment?> GetCommentAsync(int commentId)
//         {
//             return await _context.Comments.FindAsync(commentId);
//         }

//         public async Task<IEnumerable<Comment>> GetCommentsByNewsIdAsync(int newsId)
//         {
//             return await _context.Comments
//                 .Where(c => c.NewsId == newsId)
//                 .ToListAsync();
//         }

//         public async Task<Comment?> UpdateCommentAsync(Comment comment)
//         {
//             _context.Comments.Update(comment);
//             await _context.SaveChangesAsync();
//             return comment;
//         }

//         public async Task DeleteCommentAsync(int commentId)
//         {
//             var comment = await GetCommentAsync(commentId);
//             if (comment != null)
//             {
//                 _context.Comments.Remove(comment);
//                 await _context.SaveChangesAsync();
//             }
//         }
//     }
// }