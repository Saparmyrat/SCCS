﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IRepository<TModel, T>
        where TModel : class
        where T : struct
    {
        Task<IEnumerable<TModel>> GetAllAsync();

        Task<TModel> GetByIdAsync(T id);

        Task SaveAsync();

        Task CreateAsync(TModel item);

        Task UpdateAsync(TModel item);

        Task DeleteAsync(TModel item);
    }
}
