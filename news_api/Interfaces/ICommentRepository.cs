// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using news_api.Models;

// namespace news_api.Interfaces
// {
//     public interface ICommentRepository
//     {
//         Task<Comment> AddCommentAsync(Comment comment);
//         Task<Comment?> GetCommentAsync(int commentId);
//         Task<IEnumerable<Comment>> GetCommentsByNewsIdAsync(int newsId);
//         Task<Comment?> UpdateCommentAsync(Comment comment);
//         Task DeleteCommentAsync(int commentId);
//     }
// }