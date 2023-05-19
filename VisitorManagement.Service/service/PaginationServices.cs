using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorManagement.API.InputModels;
using VisitorManagement.API.ViewModels;
using VisitorManagement.Service.IRepoInfo;

namespace VisitorManagement.Service.service
{
    public class PaginationServices<T, R> : IPaginationServices<T, R> where T : class
    {
        private readonly IMapper _mapper;
        public PaginationServices(IMapper mapper)
        {
            _mapper = mapper;
        }
        public PaginationResultVM<T> GetPagination(List<R> source, PaginationInput paginationInput)
        {
            var currentPage = paginationInput.PageNumber;
            var totalNoOfRecords = source.Count;
            var pageSize = paginationInput.PageSize;
            var result = source.Skip((paginationInput.PageNumber - 1) * (paginationInput.PageSize)).Take(paginationInput.PageSize).ToList();
            var items = _mapper.Map<List<T>>(result);
            var totalPages = (int)Math.Ceiling(totalNoOfRecords / (double)pageSize);
             PaginationResultVM<T> paginationResultVM = new PaginationResultVM<T>(currentPage, totalNoOfRecords, pageSize, totalPages, items);
            return paginationResultVM;

        }
    }
}
