using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorManagement.API.InputModels;
using VisitorManagement.API.ViewModels;
using VisitorManagement.Models;

namespace VisitorManagement.Service.IRepoInfo
{
   public interface IPaginationServices<T,R> where T : class //Generic typr(T -> Dto model, R ->domain model) where T : class (constrain
    {
        PaginationResultVM<T> GetPagination(List<R> source, PaginationInput paginationInput);
    }
}
