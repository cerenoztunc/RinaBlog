using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.Abstract
{
    public interface IUnitOfWork:IAsyncDisposable
    {
        IArticleRepository Articles { get; }
        ICategoryRepository Categories { get; }
        ICommentRepository Comments { get; }
        // _ unitOfWork.Categories.AddAsync();
        Task<int> SaveAsync(); //Veritabanına burdan başka yere veriyi kaydetme işlemi yapmadık..

        //Örneğin;
        // _unitOfWork.Categories.AddAsync(category);
        // _unitOfWork.Users.AddAsync(user);
        // _unitOfWork.SaveAsync();  yaptığımızda önce kategori gönderdik, sonra user gönderdik en son ikisini birden veritabanına kaydettik. Buradaki unitOfWork class'ı sayesinde çatı bir yerden yönetirken repositorylerimizi veritabanına kayıt olan verilerin eksik ve yanlış bir şekilde gitmesine biraz daha engel oluyoruz. Herhangi bir hata varsa hiçbiri kaydolmayarak veritabanında değişikliği engelliyor ve veritabanına eksik veri gönderilmiyor..
    }
}
