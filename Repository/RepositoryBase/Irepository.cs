using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.RepositoryBase
{
    interface Irepository<T> where T : class
    {

        Task<List<T>> GetAll();
        Task<T> GetbyId(int id);
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task<T> Delete(int id);


    }
}
