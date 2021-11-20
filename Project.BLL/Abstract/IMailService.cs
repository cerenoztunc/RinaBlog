using Project.ENTITIES.DTOs;
using Project.SHARED.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.Abstract
{
    public interface IMailService
    {
        IResult Send(EmailSendDto emailSendDto); //kullanıcının verdiği email'e şifre değiştirme, bilgilendirme vb. mailleri için
        IResult SendContactEmail(EmailSendDto emailSendDto); //iletişim formunda belirtilen bilgilere geri dönüş için mailler
    }
}
